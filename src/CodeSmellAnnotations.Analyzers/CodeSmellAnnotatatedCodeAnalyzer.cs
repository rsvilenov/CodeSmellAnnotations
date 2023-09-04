using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Analyzers.Rules;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace CodeSmellAnnotations.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class CodeSmellAnnotatatedCodeAnalyzer : DiagnosticAnalyzer
    {
        private static readonly IRule[] _rules = new IRule[]
        {
            new CodeSmellAttributeRule(),
            new DuplicatedCodeAttributeRule(),
            new SolidViolationAttributeRule()
        };

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => 
            ImmutableArray.CreateRange(_rules.SelectMany(r => r.Descriptors));

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.RegisterSyntaxNodeAction(AnalyzeCodeSmellAttributes, 
                SyntaxKind.PropertyDeclaration, 
                SyntaxKind.ClassDeclaration,
                SyntaxKind.MethodDeclaration,
                SyntaxKind.InterfaceDeclaration,
                SyntaxKind.StructDeclaration,
                SyntaxKind.ConstructorDeclaration,
                SyntaxKind.EnumDeclaration,
                SyntaxKind.EnumMemberDeclaration,
                SyntaxKind.GetAccessorDeclaration,
                SyntaxKind.SetAccessorDeclaration,
                SyntaxKind.AddAccessorDeclaration,
                SyntaxKind.RemoveAccessorDeclaration,
                SyntaxKind.FieldDeclaration);
        }

        private static void AnalyzeCodeSmellAttributes(SyntaxNodeAnalysisContext context)
        {
            var location = GetLocation(context.Node);
            if (location == null) return;

            var attributesSyntaxList = GetAttributes(context.Node);
            if (!attributesSyntaxList.Any()) return;

            foreach (var rule in _rules)
            {
                var annotationAttributeType = context.SemanticModel.Compilation.GetTypeByMetadataName(rule.TriggeringAttributeType.FullName);
                var attributeSyntaxMatch = attributesSyntaxList
                    .FirstOrDefault(at => context.SemanticModel.GetSymbolInfo(at).Symbol?.ContainingType.CompareTo(annotationAttributeType) ?? false);
                
                if (attributeSyntaxMatch == null) continue;
                if (context.Compilation.GetDiagnostics().Any(diagnostic => diagnostic.Id == "CS0592")) return;

                var arguments = GetAttributeArguments(attributeSyntaxMatch, context.SemanticModel);
                var diagnosis = rule.GetDiagnosis(arguments);

                context.ReportDiagnostic(
                    Diagnostic.Create(diagnosis.Descriptor,
                        location,
                        diagnosis.DiagnosticMessageArguments));
            }
        }

        private static IEnumerable<AttributeArgument> GetAttributeArguments(AttributeSyntax attributeSyntax, SemanticModel semanticModel)
        {
            var arguments = attributeSyntax.ArgumentList?.Arguments;
            if (arguments == null) yield break;
            
            int index = 0;
            foreach (AttributeArgumentSyntax attributeArgumentSyntax in arguments)
            {
                var argumentValue = semanticModel.GetConstantValue(attributeArgumentSyntax.Expression).Value;
                var argumentName = attributeArgumentSyntax.GetArgumentName();
                yield return new AttributeArgument
                {
                    Name = argumentName,
                    Value = argumentValue,
                    Index = index++
                };
            }
        }

        private static IEnumerable<AttributeSyntax> GetAttributes(SyntaxNode node)
        {
            return node switch
            {
                MemberDeclarationSyntax memberDeclarationSyntax => memberDeclarationSyntax.AttributeLists.SelectMany(al => al.Attributes),
                AccessorDeclarationSyntax accessorDeclarationSyntax => accessorDeclarationSyntax.AttributeLists.SelectMany(al => al.Attributes),
                _ => Enumerable.Empty<AttributeSyntax>(),
            };
        }

        private static Location GetLocation(SyntaxNode node)
        {
            return node switch
            {
                BaseTypeDeclarationSyntax typeDeclarationSyntax => typeDeclarationSyntax.Identifier.GetLocation(),
                PropertyDeclarationSyntax propertyDeclarationSyntax => propertyDeclarationSyntax.Identifier.GetLocation(),
                FieldDeclarationSyntax fieldDeclarationSyntax => fieldDeclarationSyntax.Declaration.GetLocation(),
                ConstructorDeclarationSyntax constructorDeclarationSyntax => constructorDeclarationSyntax.Identifier.GetLocation(),
                AccessorDeclarationSyntax accessorDeclarationSyntax => accessorDeclarationSyntax.Body?.GetLocation() ?? accessorDeclarationSyntax.GetLocation(),
                MethodDeclarationSyntax methodDeclarationSyntax => methodDeclarationSyntax.Identifier.GetLocation(),
                _ => null,
            };
        }
    }
}
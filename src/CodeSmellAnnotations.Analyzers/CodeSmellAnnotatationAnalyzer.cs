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
    public sealed class CodeSmellAnnotatationAnalyzer : DiagnosticAnalyzer
    {

        private static readonly IAttributeRuleContainer[] _rules = new IAttributeRuleContainer[]
        {
            new CodeSmellAttributeRuleContainer(),
            new DuplicateOfAttributeRuleContainer(),
            new SolidViolationAttributeRuleContainer()
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
            var existingDiagnostics = context.Compilation.GetDeclarationDiagnostics();
            if (HasAttributeErrorLevelDiagnostic(existingDiagnostics)) return;

            var location = GetLocation(context.Node);
            if (location == null) return;

            var attributesSyntaxList = GetAttributes(context.Node);
            if (!attributesSyntaxList.Any()) return;

            foreach (var rule in _rules)
            {
                var annotationAttributeType = context.SemanticModel.Compilation.GetTypeByMetadataName(rule.TriggeringAttributeType.FullName);
                var attributeSyntaxMatches = attributesSyntaxList
                    .Where(at => context.SemanticModel.GetSymbolInfo(at).Symbol?.ContainingType.CompareTo(annotationAttributeType) ?? false);

                if (!attributeSyntaxMatches.Any()) continue;

                foreach (var attributeSyntaxMatch in attributeSyntaxMatches)
                {
                    var arguments = attributeSyntaxMatch.GetAttributeArguments(context.SemanticModel);
                    var diagnosis = rule.GetDiagnosis(arguments);

                    context.ReportDiagnostic(
                        Diagnostic.Create(diagnosis.Descriptor,
                            location,
                            diagnosis.DiagnosticMessageArguments));
                }
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

        private static bool HasAttributeErrorLevelDiagnostic(ImmutableArray<Diagnostic> diagnostics)
        {
            const string InvalidAttributeUsageDiagnosticId = "CS0592";
            const string AttributeDoesNotHaveSuchConstructorDiagnosticId = "CS1729";

            return diagnostics
                .Any(diagnostic => diagnostic.Id == InvalidAttributeUsageDiagnosticId 
                    || diagnostic.Id == AttributeDoesNotHaveSuchConstructorDiagnosticId);
        }
    }
}
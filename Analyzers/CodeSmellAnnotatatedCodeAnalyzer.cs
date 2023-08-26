using CodeSmellAnnotations.Analyzers.Rules;
using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace CodeSmellAnnotations.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class CodeSmellAnnotatatedCodeAnalyzer : DiagnosticAnalyzer
    {
        private static ImmutableArray<IRule> Rules = ImmutableArray.Create<IRule>(
            new CodeSmellAttributeRule(),
            new DuplicateCodeAttributeRule());
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => 
            ImmutableArray.CreateRange(Rules.Select(r => r.Descriptor));

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.RegisterSyntaxNodeAction(AnalyzeNeedsRefactoringAttribute, 
                SyntaxKind.PropertyDeclaration, 
                SyntaxKind.ClassDeclaration,
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

        private static IEnumerable<AttributeSyntax> GetAttributes(SyntaxNode node)
        {
            if (node is MemberDeclarationSyntax memberDeclarationSyntax)
            {
                return memberDeclarationSyntax
                .AttributeLists
                .SelectMany(al => al.Attributes);
            }
            else if (node is AccessorDeclarationSyntax accessorDeclarationSyntax)
            {
                return accessorDeclarationSyntax
                .AttributeLists
                .SelectMany(al => al.Attributes);
            }

            return Enumerable.Empty<AttributeSyntax>();
        }

        private static Location GetLocation(SyntaxNode node)
        {
            Location location = null;
            if (node is BaseTypeDeclarationSyntax typeDeclarationSyntax)
            {
                location = typeDeclarationSyntax.Identifier.GetLocation();
            }
            else if (node is PropertyDeclarationSyntax propertyDeclarationSyntax)
            {
                location = propertyDeclarationSyntax.Identifier.GetLocation();
            }
            else if (node is FieldDeclarationSyntax fieldDeclarationSyntax)
            {
                location = fieldDeclarationSyntax.Declaration.GetLocation();
            }
            else if (node is ConstructorDeclarationSyntax constructorDeclarationSyntax)
            {
                location = constructorDeclarationSyntax.Identifier.GetLocation();
            }
            else if (node is AccessorDeclarationSyntax accessorDeclarationSyntax)
            {
                location = accessorDeclarationSyntax.Body.GetLocation();
            }

            return location;
        }

        private static void AnalyzeNeedsRefactoringAttribute(SyntaxNodeAnalysisContext context)
        {
            
            var location = GetLocation(context.Node);
            if (location == null)
            {
                return;
            }

            // Identifier is in BaseTypeDeclarationSyntax
            // AttributeList is in MemberDeclarationSyntax
            // BaseTypeDeclaration is also MemberDeclaration

            var attributesSyntaxList = GetAttributes(context.Node);


            foreach (var rule in Rules)
            {
                var attributeSyntaxMatch = attributesSyntaxList
                    .FirstOrDefault(at => context.SemanticModel.GetSymbolInfo(at).Symbol?.ContainingType?.ToDisplayString() == rule.TriggeringAttributeName);
                if (attributeSyntaxMatch != null)
                {
                    context.ReportDiagnostic(
                        Diagnostic.Create(rule.Descriptor,
                            location,
                            rule.GetDiagnosticMessageArguments(attributeSyntaxMatch)));
                }
            }
        }
    }
}
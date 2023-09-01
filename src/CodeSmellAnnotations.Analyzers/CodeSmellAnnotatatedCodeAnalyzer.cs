﻿using CodeSmellAnnotations.Analyzers.Extensions;
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
            new DuplicateCodeAttributeRule(),
            new PrimitiveObsessionAttributeRule(),
            new LeakyAbstractionAttributeRule(),
            new SolidViolationAttributeRule()
        };

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => 
            ImmutableArray.CreateRange(_rules.Select(r => r.Descriptor));

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


                context.ReportDiagnostic(
                    Diagnostic.Create(rule.Descriptor,
                        location,
                        rule.GetDiagnosticMessageArguments(attributeSyntaxMatch)));
                
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
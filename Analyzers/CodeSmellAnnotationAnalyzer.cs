using CodeSmellAnnotations.Analyzers.Rules;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace CodeSmellAnnotations.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class CodeSmellAnnotationAnalyzer : DiagnosticAnalyzer
    {
        private static ImmutableArray<IRule> Rules = ImmutableArray.Create<IRule>(new CodeSmellAttributeRule());
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => 
            ImmutableArray.CreateRange(Rules.Select(r => r.Descriptor));

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.RegisterSyntaxNodeAction(AnalyzeNeedsRefactoringAttribute, SyntaxKind.Attribute);
        }

        private static void AnalyzeNeedsRefactoringAttribute(SyntaxNodeAnalysisContext context)
        {
            if (!(context.Node is AttributeSyntax attributeSyntax))
            {
                return;
            }

            return; 
            /* var containingType = context
                .SemanticModel
                .GetSymbolInfo(attributeSyntax)
                .Symbol
                .ContainingType;
            if (containingType == null)
            {
                return;
            }

            var typeStr = containingType.ToDisplayString();
            foreach (var rule in Rules)
            {
                if (typeStr == rule.TriggeringAttributeName)
                {
                    context.ReportDiagnostic(Diagnostic.Create(rule.Descriptor, attributeSyntax.GetLocation(), rule.GetDiagnosticMessageArguments(attributeSyntax)));
                }
            } */
        }
    }
}
using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
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
    public sealed class CodeSmellAttributeArgumentAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "SMLE001";

        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId,
            "Reason is mandatory",
            "When 'General' kind is used, a reason should be provided.",
            "Build", 
            DiagnosticSeverity.Error, 
            isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.RegisterSyntaxNodeAction(AnalyzeNeedsRefactoringAttribute, SyntaxKind.Attribute);
        }

        private static void AnalyzeNeedsRefactoringAttribute(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is not AttributeSyntax attributeSyntax) return;

            var attributeNamedSymbol = context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol?.ContainingType;
            if (attributeNamedSymbol == null) return;

            var annotationAttributeTypeNamedSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName(typeof(CodeSmellAttribute).FullName);
            if (!attributeNamedSymbol.CompareTo(annotationAttributeTypeNamedSymbol)) return;
            
            var attributeArguments = attributeSyntax.GetAttributeArguments(context.SemanticModel);
            if (!attributeArguments.Any()) return;

            var kindEnumValue = attributeArguments.ElementAt(0).GetEnumValue<Kind>();
            if (kindEnumValue != Kind.General) return;
                
            var reason = attributeArguments.FirstOrDefault(aa => aa.Name == "Reason")?.Value as string;
            if (!string.IsNullOrEmpty(reason)) return;

            context.ReportDiagnostic(Diagnostic.Create(Rule, attributeSyntax.GetLocation()));
        }
    }
}

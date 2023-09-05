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
    public sealed class AttributeArgumentAnalyzer : DiagnosticAnalyzer
    {

        internal static DiagnosticDescriptor RuleSMLE001 = new DiagnosticDescriptor(
            "SMLE001",
            "Reason is mandatory",
            "When 'General' kind is used, a reason should be provided.",
            "Build",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        internal static DiagnosticDescriptor RuleSMLE002 = new DiagnosticDescriptor(
            "SMLE002",
            "Duplicated code is mandatory",
            "You must specify what code is duplicated.",
            "Build",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                var builder = ImmutableArray.CreateBuilder<DiagnosticDescriptor>();
                builder.Add(RuleSMLE001);
                builder.Add(RuleSMLE002);
                return builder.ToImmutable();
            }
        }
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

            var codeSmellAttributeTypeNamedSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName(typeof(CodeSmellAttribute).FullName);
            if (attributeNamedSymbol.CompareTo(codeSmellAttributeTypeNamedSymbol))
            {
                DiagnoseSMLE001(context, attributeSyntax);
                return;
            }

            var duplicatesAttributeTypeNamedSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName(typeof(DuplicateOfAttribute).FullName);
            if (attributeNamedSymbol.CompareTo(duplicatesAttributeTypeNamedSymbol))
            {
                DiagnoseSMLE002(context, attributeSyntax);
            }
        }

        private static void DiagnoseSMLE001(SyntaxNodeAnalysisContext context, AttributeSyntax attributeSyntax)
        {
            var attributeArguments = attributeSyntax.GetAttributeArguments(context.SemanticModel);
            if (!attributeArguments.Any()) return;

            var kindEnumValue = attributeArguments.ElementAt(0).GetEnumValue<Kind>();
            if (kindEnumValue != Kind.General) return;

            var reason = attributeArguments.FirstOrDefault(aa => aa.Name == "Reason")?.Value as string;
            if (!string.IsNullOrEmpty(reason)) return;

            context.ReportDiagnostic(Diagnostic.Create(RuleSMLE001, attributeSyntax.GetLocation()));
        }

        private static void DiagnoseSMLE002(SyntaxNodeAnalysisContext context, AttributeSyntax attributeSyntax)
        {
            var attributeArguments = attributeSyntax.GetAttributeArguments(context.SemanticModel);
            if (!attributeArguments.Any()) return;

            var duplicates = attributeArguments.ElementAt(0).Value as string;
            if (!string.IsNullOrEmpty(duplicates)) return;

            context.ReportDiagnostic(Diagnostic.Create(RuleSMLE002, attributeSyntax.GetLocation()));
        }
    }
}

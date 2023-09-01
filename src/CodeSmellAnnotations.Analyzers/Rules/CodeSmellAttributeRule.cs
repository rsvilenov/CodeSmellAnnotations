using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal class CodeSmellAttributeRule : IRule
    {
        // In order for the Roslyn's Release tracking analyzer to work,
        // the descriptor should be a field and the full "new" expression
        // should be used (the short one - new("SML00X",...) will break the analysis)
        private static readonly DiagnosticDescriptor _descriptor 
            = new DiagnosticDescriptor("SML001", 
                "Code smell", 
                "Code smell{0}", 
                "CodeSmell",
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true,
                description: "General code smell annotation",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

        public DiagnosticDescriptor Descriptor => _descriptor;

        public Type TriggeringAttributeType => typeof(CodeSmellAttribute);

        public string[] GetDiagnosticMessageArguments(AttributeSyntax attributeSyntax)
        {
            var message = attributeSyntax.GetStringArgumentValue();
            return new[] { string.IsNullOrEmpty(message) ? null : $": {message}" };
        }
    }
}

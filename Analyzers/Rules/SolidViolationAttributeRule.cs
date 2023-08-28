using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal class SolidViolationAttributeRule : IRule
    {
        public DiagnosticDescriptor Descriptor 
            => new DiagnosticDescriptor("CSM005", 
                "SOLID violation",
                "Violates {0} SOLID principle {1}", 
                "CodeSmell", 
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true);

        public Type TriggeringAttributeType => typeof(SolidViolationAttribute);

        public string[] GetDiagnosticMessageArguments(AttributeSyntax attributeSyntax)
        {
            var violates = attributeSyntax.GetStringArgumentValue();
            var reason = attributeSyntax.GetStringArgumentValue("Reason");
            return new[] { violates, string.IsNullOrEmpty(reason) ? null : $": {reason}" };
        }
    }
}

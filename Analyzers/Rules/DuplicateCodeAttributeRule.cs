using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal class DuplicateCodeAttributeRule : IRule
    {
        public DiagnosticDescriptor Descriptor 
            => new DiagnosticDescriptor("SML002", 
                "Duplicate code", 
                "Duplcate code.{0}{1}", 
                "CodeSmell", 
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true);

        public Type TriggeringAttributeType => typeof(DuplicateCodeAttribute);

        public string[] GetDiagnosticMessageArguments(AttributeSyntax attributeSyntax)
        {
            var reason = attributeSyntax.GetStringArgumentValue("Reason");
            var duplicates = attributeSyntax.GetStringArgumentValue("Duplicates");

            return new[] 
            { 
                string.IsNullOrEmpty(duplicates) ? null : $" Duplicates {duplicates}.", 
                string.IsNullOrEmpty(duplicates) ? null : $" Reason: {reason}", 
            };
        }
    }
}

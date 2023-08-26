using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Text;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal class DuplicateCodeAttributeRule : IRule
    {
        public DiagnosticDescriptor Descriptor 
            => new DiagnosticDescriptor("CSM002", 
                "Duplicate code!", 
                "Duplcate code{0}", 
                "CodeSmell", 
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true);

        public Type TriggeringAttributeType => typeof(DuplicateCodeAttribute);

        public string[] GetDiagnosticMessageArguments(AttributeSyntax attributeSyntax)
        {
            var message = attributeSyntax.GetStringArgumentValue("Message");
            var duplicates = attributeSyntax.GetStringArgumentValue("Duplicates");

            var diagnosticMessageArgument = string.IsNullOrEmpty(duplicates) && string.IsNullOrEmpty(message) ? null : $": {duplicates} {message}";
            return new[] { diagnosticMessageArgument };
        }
    }
}

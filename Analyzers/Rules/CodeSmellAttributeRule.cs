using CodeSmellAnnotations.Analyzers.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal class CodeSmellAttributeRule : IRule
    {
        public DiagnosticDescriptor Descriptor 
            => new DiagnosticDescriptor("CSM001", 
                "Code smell!", 
                "Code smell{0}", 
                "CodeSmell", 
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true);

        public string TriggeringAttributeName => "CodeSmellAnnotations.Attributes.CodeSmellAttribute";

        public string[] GetDiagnosticMessageArguments(AttributeSyntax attributeSyntax)
        {
            var message = attributeSyntax.GetStringArgumentValue();
            var diagnosticMessageArgument = string.IsNullOrEmpty(message) ? null : $": {message}";
            return new[] { diagnosticMessageArgument };
        }
    }
}

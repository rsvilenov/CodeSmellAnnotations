using CodeSmellAnnotations.Analyzers.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal class DuplicateCodeAttributeRule : IRule
    {
        public DiagnosticDescriptor Descriptor 
            => new DiagnosticDescriptor("CSM002", 
                "Duplicate code!", 
                "Duplcates{0}", 
                "CodeSmell", 
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true);

        public string TriggeringAttributeName => "CodeSmellAnnotations.Attributes.DuplicateCodeAttribute";

        public string[] GetDiagnosticMessageArguments(AttributeSyntax attributeSyntax)
        {
            var message = attributeSyntax.GetStringArgumentValue("Message");
            var duplicates = attributeSyntax.GetStringArgumentValue("Duplicates");

            // prior to c#11 we can't use nameof with attribute parameters
            // TODO: write an analyzer about this
            if (duplicates != null && duplicates.StartsWith("nameof("))
            {
                int startIdx = duplicates.IndexOf("(");
                duplicates = duplicates.Substring(startIdx + 1, duplicates.Length - startIdx - 2);
            }

            var diagnosticMessageArgument = string.IsNullOrEmpty(duplicates) ? null : $": {duplicates}";
            return new[] { diagnosticMessageArgument };
        }
    }
}

using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal class LeakyAbstractionAttributeRule : IRule
    {
        public DiagnosticDescriptor Descriptor 
            => new DiagnosticDescriptor("SML004", 
                "Leaky abstraction",
                "Leaky abstraction{0}", 
                "CodeSmell", 
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true);

        public Type TriggeringAttributeType => typeof(LeakyAbstractionAttribute);

        public string[] GetDiagnosticMessageArguments(AttributeSyntax attributeSyntax)
        {
            var message = attributeSyntax.GetStringArgumentValue();
            return new[] { string.IsNullOrEmpty(message) ? null : $": {message}" };
        }
    }
}

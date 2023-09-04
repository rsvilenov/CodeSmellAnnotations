using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal class DuplicatedCodeAttributeRule : IRule
    {
        private static readonly DiagnosticDescriptor _descriptor
            = new DiagnosticDescriptor("SML002", 
                "Duplicate code", 
                "Duplcate code.{0}{1}{2}", 
                "CodeSmell", 
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true,
                description: "Duplicate code",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML002.md");

        public DiagnosticDescriptor Descriptor => _descriptor;
        public Type TriggeringAttributeType => typeof(DuplicatedCodeAttribute);

        public string[] GetDiagnosticMessageArguments(AttributeSyntax attributeSyntax)
        {
            var reason = attributeSyntax.GetArgumentValueAsString("Reason");
            var duplicates = attributeSyntax.GetArgumentValueAsString("Duplicates");
            var kind = attributeSyntax.GetArgumentValueAsString("Kind");
            string kindDisplayString = null;
            if (kind != null && !_duplicationKindEnumStringToDisplayStringMapping.TryGetValue(kind, out kindDisplayString))
            {
                throw new InvalidOperationException($"{nameof(Kind)} enum does not contain a member called {kind}");
            }

            return new[] 
            { 
                string.IsNullOrEmpty(duplicates) ? null : $" Duplicates {duplicates}.", 
                string.IsNullOrEmpty(kindDisplayString) ? null : $" Kind: {kindDisplayString}.", 
                string.IsNullOrEmpty(reason) ? null : $" {reason}", 
            };
        }

        private static Dictionary<string, string> _duplicationKindEnumStringToDisplayStringMapping = new()
        {
            { nameof(DuplicationKind.FullDuplication), "full duplication" },
            { nameof(DuplicationKind.OddballSolution), "oddball solution" }
        };
    }
}

using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal class DuplicatedCodeAttributeRule : IRule
    {
        private static readonly DiagnosticDescriptor _descriptor
            = new DiagnosticDescriptor("SML002", 
                "Duplicated code", 
                "Duplcated code.{0}{1}{2}", 
                "CodeSmell", 
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true,
                description: "Duplicate code",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML002.md");

        public DiagnosticDescriptor Descriptor => _descriptor;
        public Type TriggeringAttributeType => typeof(DuplicatedCodeAttribute);

        public string[] GetDiagnosticMessageArguments(IEnumerable<AttributeArgument> attributeArguments)
        {
            var reason = attributeArguments.FirstOrDefault(a => a.Name == "Reason")?.Value?.ToString();
            
            var duplicates = attributeArguments.FirstOrDefault(a => a.Name == "Duplicates")?.Value?.ToString();

            string kindDisplayString = null;
            var kind = attributeArguments.FirstOrDefault(a => a.Name == "Kind");
            if (kind != null)
            {
                var kindEnumValue = kind.GetEnumValue<DuplicationKind>();

                if (kind != null && !_duplicationKindEnumStringToDisplayStringMapping.TryGetValue(kindEnumValue, out kindDisplayString))
                {
                    throw new InvalidOperationException($"{nameof(Kind)} enum does not contain a member called {kind}");
                }
            }

            return new[] 
            { 
                string.IsNullOrEmpty(duplicates) ? null : $" Duplicates {duplicates}.", 
                string.IsNullOrEmpty(kindDisplayString) ? null : $" Kind: {kindDisplayString}.", 
                string.IsNullOrEmpty(reason) ? null : $" {reason}", 
            };
        }

        private static Dictionary<DuplicationKind, string> _duplicationKindEnumStringToDisplayStringMapping = new()
        {
            { DuplicationKind.FullDuplication, "full duplication" },
            { DuplicationKind.OddballSolution, "oddball solution" }
        };
    }
}

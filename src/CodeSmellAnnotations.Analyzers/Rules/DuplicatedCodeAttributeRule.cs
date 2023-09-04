using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal partial class DuplicatedCodeAttributeRule : IRule
    {
        public Type TriggeringAttributeType => typeof(DuplicatedCodeAttribute);

        public Diagnosis GetDiagnosis(IEnumerable<AttributeArgument> attributeArguments)
        {
            var reason = attributeArguments.FirstOrDefault(a => a.Name == "Reason")?.Value?.ToString();
            
            var duplicates = attributeArguments.FirstOrDefault(a => a.Name == "Duplicates")?.Value?.ToString();

            var kind = attributeArguments.FirstOrDefault(a => a.Name == "Kind");
            DuplicationKind kindEnumValue = kind == null ? default : kind.GetEnumValue<DuplicationKind>(); ;
            if (!_duplicationKindEnumStringToDisplayStringMapping.TryGetValue(kindEnumValue, out string kindDisplayString))
            {
                throw new InvalidOperationException($"{nameof(Kind)} enum does not contain a member called {kind}");
            }

            var diagnosticMessageArguments = new[]
            {
                kindDisplayString,
                string.IsNullOrEmpty(duplicates) ? null : $". Duplicates {duplicates}.", 
                string.IsNullOrEmpty(reason) ? null : $" {reason}", 
            };

            return new Diagnosis
            {
                Descriptor = GetDescriptorForKind(kindEnumValue),
                DiagnosticMessageArguments = diagnosticMessageArguments
            };
        }

        private static Dictionary<DuplicationKind, string> _duplicationKindEnumStringToDisplayStringMapping = new()
        {
            { DuplicationKind.General, "Duplicate code" },
            { DuplicationKind.OddballSolution, "Oddball solution" }
        };
    }
}

using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal partial class DuplicatesAttributeRuleContainer : IAttributeRuleContainer
    {
        public Type TriggeringAttributeType => typeof(DuplicatesAttribute);

        public Diagnosis GetDiagnosis(IEnumerable<AttributeArgument> attributeArguments)
        {
            var duplicates = attributeArguments.ElementAtOrDefault(0)?.Value?.ToString();

            var reason = attributeArguments.FirstOrDefault(a => a.Name == "Reason")?.Value?.ToString();
            
            var kind = attributeArguments.FirstOrDefault(a => a.Name == "Kind");
            DuplicationKind kindEnumValue = kind == null ? default : kind.GetEnumValue<DuplicationKind>(); ;
            
            var diagnosticMessageArguments = new[]
            {
                string.IsNullOrEmpty(duplicates) ? null : $"Duplicates {duplicates}.", 
                string.IsNullOrEmpty(reason) ? null : $" {reason}", 
            };

            return new Diagnosis
            {
                Descriptor = GetDescriptorForKind(kindEnumValue),
                DiagnosticMessageArguments = diagnosticMessageArguments
            };
        }
    }
}

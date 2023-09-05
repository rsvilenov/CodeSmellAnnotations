using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal partial class DuplicatedCodeAttributeRuleContainer : IAttributeRuleContainer
    {
        public Type TriggeringAttributeType => typeof(DuplicatedCodeAttribute);

        public Diagnosis GetDiagnosis(IEnumerable<AttributeArgument> attributeArguments)
        {
            var reason = attributeArguments.FirstOrDefault(a => a.Name == "Reason")?.Value?.ToString();
            
            var duplicates = attributeArguments.FirstOrDefault(a => a.Name == "Duplicates")?.Value?.ToString();

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

using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal partial class CodeSmellAttributeRule : IRule
    {
        public Type TriggeringAttributeType => typeof(CodeSmellAttribute);

        public Diagnosis GetDiagnosis(IEnumerable<AttributeArgument> attributeArguments)
        {
            var kindAttributeValue = attributeArguments.ElementAtOrDefault(0);
            if (kindAttributeValue == null)
            {
                throw new InvalidOperationException($"Could not get the 'Kind' parameter value.");
            }

            var kindEnumValue = kindAttributeValue.GetEnumValue<Kind>();

            var reason = attributeArguments.FirstOrDefault(a => a.Name == "Reason")?.Value?.ToString();

            return new Diagnosis
            {
                Descriptor = GetDescriptorForKind(kindEnumValue),
                DiagnosticMessageArguments = new[] { reason }
            };
        }
    }
}

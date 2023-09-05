using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal partial class SolidViolationAttributeRuleContainer : IAttributeRuleContainer
    {
        public Type TriggeringAttributeType => typeof(SolidViolationAttribute);

        public Diagnosis GetDiagnosis(IEnumerable<AttributeArgument> attributeArguments)
        {
            var violatesAttributeValue = attributeArguments.ElementAt(0);
            Debug.Assert(violatesAttributeValue is not null, "Could not get the 'SolidPrinciple' parameter value.");
            
            var violatesEnumValue = violatesAttributeValue.GetEnumValue<SolidPrinciple>();
            
            var reason = attributeArguments.FirstOrDefault(a => a.Name == "Reason")?.Value?.ToString();

            return new Diagnosis
            {
                Descriptor = GetDescriptorForPrinciple(violatesEnumValue),
                DiagnosticMessageArguments = new [] { reason }
            };
        }
    }
}

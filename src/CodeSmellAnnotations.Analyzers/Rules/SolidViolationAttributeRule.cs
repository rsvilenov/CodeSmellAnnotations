using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal partial class SolidViolationAttributeRule : IRule
    {
        public Type TriggeringAttributeType => typeof(SolidViolationAttribute);

        public Diagnosis GetDiagnosis(IEnumerable<AttributeArgument> attributeArguments)
        {
            var violatesAttributeValue = attributeArguments.ElementAt(0);
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

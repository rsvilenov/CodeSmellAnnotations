using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal class SolidViolationAttributeRule : IRule
    {
        private static readonly DiagnosticDescriptor _descriptor
            = new DiagnosticDescriptor("SML005", 
                "SOLID violation",
                "SOLID violation. Violates the {0} principle{1}", 
                "CodeSmell", 
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true,
                description: "Violation of SOLID principles",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML005.md");

        public DiagnosticDescriptor Descriptor => _descriptor;

        public Type TriggeringAttributeType => typeof(SolidViolationAttribute);

        public string[] GetDiagnosticMessageArguments(IEnumerable<AttributeArgument> attributeArguments)
        {
            var violatesAttributeValue = attributeArguments.ElementAt(0);
            var violatesEnumValue = violatesAttributeValue.GetEnumValue<SolidPrinciple>();
            if (!_solidPrincipleEnumStringToDisplayStringMapping.TryGetValue(violatesEnumValue, out string principleDisplayString))
            {
                throw new InvalidOperationException($"{nameof(SolidPrinciple)} enum does not contain a member called {violatesAttributeValue}");
            }

            var reason = attributeArguments.FirstOrDefault(a => a.Name == "Reason")?.Value?.ToString();

            return new[] { principleDisplayString, string.IsNullOrEmpty(reason) ? null : $": {reason}" };
        }

        private static Dictionary<SolidPrinciple, string> _solidPrincipleEnumStringToDisplayStringMapping = new()
        {
            { SolidPrinciple.SingleResponsibility, "single responsibility" },
            { SolidPrinciple.OpenClosed, "open/closed" },
            { SolidPrinciple.Liskov, "Liskov substitution" },
            { SolidPrinciple.InterfaceSegregation, "interface segregation" },
            { SolidPrinciple.DependencyInversion, "dependency inversion" },
        };
    }
}

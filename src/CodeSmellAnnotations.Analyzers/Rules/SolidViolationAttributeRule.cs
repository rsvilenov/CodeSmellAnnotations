using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal class SolidViolationAttributeRule : IRule
    {
        private static readonly DiagnosticDescriptor _descriptor
            = new DiagnosticDescriptor("SML005", 
                "SOLID violation",
                "Violates the {0} principle{1}", 
                "CodeSmell", 
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true,
                description: "Violation of SOLID principles",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML005.md");

        public DiagnosticDescriptor Descriptor => _descriptor;

        public Type TriggeringAttributeType => typeof(SolidViolationAttribute);

        public string[] GetDiagnosticMessageArguments(AttributeSyntax attributeSyntax)
        {
            var violatesAttributeValue = attributeSyntax.GetArgumentValueAsString(0);
            if (!_solidPrincipleEnumStringToDisplayStringMapping.TryGetValue(violatesAttributeValue, out string principleDisplayString))
            {
                throw new InvalidOperationException($"{nameof(SolidPrinciple)} enum does not contain a member called {violatesAttributeValue}");
            }

            var reason = attributeSyntax.GetArgumentValueAsString("Reason");
            return new[] { principleDisplayString, string.IsNullOrEmpty(reason) ? null : $": {reason}" };
        }

        private static Dictionary<string, string> _solidPrincipleEnumStringToDisplayStringMapping = new()
        {
            { nameof(SolidPrinciple.SingleResponsibility), "single responsibility" },
            { nameof(SolidPrinciple.OpenClosed), "open/closed" },
            { nameof(SolidPrinciple.Liskov), "Liskov substitution" },
            { nameof(SolidPrinciple.InterfaceSegregation), "interface segregation" },
            { nameof(SolidPrinciple.DependencyInversion), "dependency inversion" },
        };
    }
}

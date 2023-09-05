using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal partial class SolidViolationAttributeRuleContainer
    {
        private static readonly DiagnosticDescriptor _descriptorSML201
               = new DiagnosticDescriptor("SML201",
                   "SOLID violation",
                   "SOLID violation: single responsibility principle. {0}",
                   "CodeSmell",
                   DiagnosticSeverity.Warning,
                   isEnabledByDefault: true,
                   description: "Violation of SOLID principles",
                   helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML005.md");

        private static readonly DiagnosticDescriptor _descriptorSML202
               = new DiagnosticDescriptor("SML202",
                   "SOLID violation",
                   "SOLID violation: open/closed principle. {0}",
                   "CodeSmell",
                   DiagnosticSeverity.Warning,
                   isEnabledByDefault: true,
                   description: "Violation of SOLID principles",
                   helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML005.md");

        private static readonly DiagnosticDescriptor _descriptorSML203
               = new DiagnosticDescriptor("SML203",
                   "SOLID violation",
                   "SOLID violation: liskov substitution principle. {0}",
                   "CodeSmell",
                   DiagnosticSeverity.Warning,
                   isEnabledByDefault: true,
                   description: "Violation of SOLID principles",
                   helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML005.md");

        private static readonly DiagnosticDescriptor _descriptorSML204
               = new DiagnosticDescriptor("SML204",
                   "SOLID violation",
                   "SOLID violation: interface segregation principle. {0}",
                   "CodeSmell",
                   DiagnosticSeverity.Warning,
                   isEnabledByDefault: true,
                   description: "Violation of SOLID principles",
                   helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML005.md");

        private static readonly DiagnosticDescriptor _descriptorSML205
               = new DiagnosticDescriptor("SML205",
                   "SOLID violation",
                   "SOLID violation: dependency inversion principle. {0}",
                   "CodeSmell",
                   DiagnosticSeverity.Warning,
                   isEnabledByDefault: true,
                   description: "Violation of SOLID principles",
                   helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML005.md");

        public DiagnosticDescriptor[] Descriptors =>  _descriptors;

        private static DiagnosticDescriptor[] _descriptors => new[] { _descriptorSML201, _descriptorSML202, _descriptorSML203, _descriptorSML204, _descriptorSML205 };

        private static Dictionary<SolidPrinciple, DiagnosticDescriptor> _kindEnumDiagnosticDescriptorMapping = new()
        {
            { SolidPrinciple.SingleResponsibility, _descriptorSML201 },
            { SolidPrinciple.OpenClosed, _descriptorSML202 },
            { SolidPrinciple.Liskov, _descriptorSML203 },
            { SolidPrinciple.InterfaceSegregation, _descriptorSML204 },
            { SolidPrinciple.DependencyInversion, _descriptorSML205 },
        };

        private DiagnosticDescriptor GetDescriptorForPrinciple(SolidPrinciple principle)
            => _kindEnumDiagnosticDescriptorMapping[principle];
    }
}

using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal partial class SolidViolationAttributeRule : IHaveDescriptors
    {
        private static readonly DiagnosticDescriptor _descriptorSML201
               = new DiagnosticDescriptor("SML201",
                   "SOLID violation",
                   "Single responsibility principle violation{0}",
                   "CodeSmell",
                   DiagnosticSeverity.Warning,
                   isEnabledByDefault: true,
                   description: "Violation of SOLID principles",
                   helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML005.md");

        private static readonly DiagnosticDescriptor _descriptorSML202
               = new DiagnosticDescriptor("SML202",
                   "SOLID violation",
                   "Open/closed principle violation{0}",
                   "CodeSmell",
                   DiagnosticSeverity.Warning,
                   isEnabledByDefault: true,
                   description: "Violation of SOLID principles",
                   helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML005.md");

        private static readonly DiagnosticDescriptor _descriptorSML203
               = new DiagnosticDescriptor("SML203",
                   "SOLID violation",
                   "Liskov substitution principle violation{0}",
                   "CodeSmell",
                   DiagnosticSeverity.Warning,
                   isEnabledByDefault: true,
                   description: "Violation of SOLID principles",
                   helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML005.md");

        private static readonly DiagnosticDescriptor _descriptorSML204
               = new DiagnosticDescriptor("SML204",
                   "SOLID violation",
                   "Interface segregation principle violation{0}",
                   "CodeSmell",
                   DiagnosticSeverity.Warning,
                   isEnabledByDefault: true,
                   description: "Violation of SOLID principles",
                   helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML005.md");

        private static readonly DiagnosticDescriptor _descriptorSML205
               = new DiagnosticDescriptor("SML205",
                   "SOLID violation",
                   "Dependency inversion principle violation{0}",
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

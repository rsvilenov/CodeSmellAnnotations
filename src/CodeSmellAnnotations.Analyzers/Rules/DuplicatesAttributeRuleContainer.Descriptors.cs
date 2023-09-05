using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal partial class DuplicatesAttributeRuleContainer
    {
        private static readonly DiagnosticDescriptor _descriptorSML100
            = new DiagnosticDescriptor("SML100",
                "Duplicated code",
                "Duplicated code. {0}{1}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Duplicated code",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML002.md");

        private static readonly DiagnosticDescriptor _descriptorSML101
            = new DiagnosticDescriptor("SML101",
                "Duplicated code",
                "Oddball solution. {0}{1}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Duplicated code",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML002.md");


        public DiagnosticDescriptor[] Descriptors => _descriptors;

        private static DiagnosticDescriptor[] _descriptors => new[] { _descriptorSML100, _descriptorSML101 };

        private static Dictionary<DuplicationKind, DiagnosticDescriptor> _kindEnumDiagnosticDescriptorMapping = new()
        {
            { DuplicationKind.General, _descriptorSML100 },
            { DuplicationKind.OddballSolution, _descriptorSML101 }
        };

        private DiagnosticDescriptor GetDescriptorForKind(DuplicationKind kind)
            => _kindEnumDiagnosticDescriptorMapping[kind];
    }
}

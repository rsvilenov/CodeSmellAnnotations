using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal partial class CodeSmellAttributeRule : IHaveDescriptors
    {
        public DiagnosticDescriptor[] Descriptors => _descriptors;


        // In order for the Roslyn's Release tracking analyzer to work,
        // the descriptor should be a field and the full "new" expression
        // should be used (the short one - new("SML00X",...) will break the analysis)
        private static readonly DiagnosticDescriptor _descriptorSML001
            = new DiagnosticDescriptor("SML001",
                "Code smell",
                "Code smell{0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "General code smell annotation",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

        private static readonly DiagnosticDescriptor _descriptorSML002
            = new DiagnosticDescriptor("SML002",
                "Code smell",
                "Inappropriate intimacy{0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Inappropriate intimacy",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");


        private static readonly DiagnosticDescriptor _descriptorSML003
            = new DiagnosticDescriptor("SML003",
                "Code smell",
                "Leaky abastraction{0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

        private static DiagnosticDescriptor[] _descriptors => new[] { _descriptorSML001, _descriptorSML002, _descriptorSML003 };

        private static Dictionary<Kind, DiagnosticDescriptor> _kindEnumDiagnosticDescriptorMapping = new()
        {
            { Kind.General, _descriptorSML001 },
            { Kind.InappropriateIntimacy, _descriptorSML002 },
            { Kind.LekyAbstraction, _descriptorSML003 },
            { Kind.SpeculativeGenerality, _descriptorSML001 },
            { Kind.IndecentExposure, _descriptorSML001 },
            { Kind.VerticalSeparation, _descriptorSML001 },
            { Kind.MagicNumbers, _descriptorSML001 },
            { Kind.BloatedConstructor, _descriptorSML001 },
            { Kind.FeatureEnvy, _descriptorSML001 },
            { Kind.HiddenBehavior, _descriptorSML001 },
            { Kind.DataClump, _descriptorSML001 },
            { Kind.InconsistentNaming, _descriptorSML001 },
            { Kind.UncommunicativeNaming, _descriptorSML001 },
            { Kind.FallaciousNaming, _descriptorSML001 },
            { Kind.TemporalCoupling, _descriptorSML001 },
            { Kind.PrimitiveObsession, _descriptorSML001 },
        };

        private DiagnosticDescriptor GetDescriptorForKind(Kind kind)
            => _kindEnumDiagnosticDescriptorMapping[kind];
    }
}

using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    [SuppressMessage("Style", "IDE0090:Use 'new(...)'", Justification = "Release tracking analyzer doesn't support this syntax.")]
    internal partial class CodeSmellAttributeRuleContainer
    {
        public DiagnosticDescriptor[] Descriptors => _descriptors;


        private static readonly DiagnosticDescriptor _descriptorSML001
            = new DiagnosticDescriptor("SML001",
                "Code smell",
                "Code smell. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "General code smell annotation",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

        private static readonly DiagnosticDescriptor _descriptorSML002
            = new DiagnosticDescriptor("SML002",
                "Inappropriate intimacy",
                "Inappropriate intimacy. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Inappropriate intimacy",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML002.md");


        private static readonly DiagnosticDescriptor _descriptorSML003
            = new DiagnosticDescriptor("SML003",
                "Leaky abastraction",
                "Leaky abastraction. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML003.md");

        private static readonly DiagnosticDescriptor _descriptorSML004
            = new DiagnosticDescriptor("SML004",
                "Speculative generality",
                "Leaky abastraction. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML004.md");

        private static readonly DiagnosticDescriptor _descriptorSML005
            = new DiagnosticDescriptor("SML005",
                "Indecent exposure",
                "Indecent exposure. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML005.md");

        private static readonly DiagnosticDescriptor _descriptorSML006
            = new DiagnosticDescriptor("SML006",
                "Vertical separation",
                "Vertical separation. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML006.md");

        private static readonly DiagnosticDescriptor _descriptorSML007
            = new DiagnosticDescriptor("SML007",
                "Magic numbers",
                "Magic numbers. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML007.md");

        private static readonly DiagnosticDescriptor _descriptorSML008
            = new DiagnosticDescriptor("SML008",
                "Bloated constructor",
                "Bloated constructor. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML008.md");

        private static readonly DiagnosticDescriptor _descriptorSML009
            = new DiagnosticDescriptor("SML009",
                "Feature envy",
                "Feature envy. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML009.md");

        private static readonly DiagnosticDescriptor _descriptorSML010
            = new DiagnosticDescriptor("SML010",
                "Hidden behavior",
                "Hidden behavior. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML010.md");

        private static readonly DiagnosticDescriptor _descriptorSML011
            = new DiagnosticDescriptor("SML011",
                "Data clump",
                "Data clump. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML011.md");

        private static readonly DiagnosticDescriptor _descriptorSML012
            = new DiagnosticDescriptor("SML012",
                "Inconsistent naming",
                "Inconsistent naming. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML012.md");

        private static readonly DiagnosticDescriptor _descriptorSML013
            = new DiagnosticDescriptor("SML013",
                "Uncommunicative naming",
                "Uncommunicative naming. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML013.md");

        private static readonly DiagnosticDescriptor _descriptorSML014
            = new DiagnosticDescriptor("SML014",
                "Fallacious naming",
                "Fallacious naming. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML014.md");

        private static readonly DiagnosticDescriptor _descriptorSML015
            = new DiagnosticDescriptor("SML015",
                "Temporal coupling",
                "Temporal coupling. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML015.md");

        private static readonly DiagnosticDescriptor _descriptorSML016
            = new DiagnosticDescriptor("SML016",
                "Primitive obsession",
                "Primitive obsession. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML016.md");

        private static DiagnosticDescriptor[] _descriptors => new[] 
        { 
            _descriptorSML001,
            _descriptorSML002,
            _descriptorSML003, 
            _descriptorSML004,
            _descriptorSML005,
            _descriptorSML006,
            _descriptorSML007,
            _descriptorSML008,
            _descriptorSML009,
            _descriptorSML010,
            _descriptorSML011,
            _descriptorSML012,
            _descriptorSML013,
            _descriptorSML014,
            _descriptorSML015,
            _descriptorSML016
        };

        private static Dictionary<Kind, DiagnosticDescriptor> _kindEnumDiagnosticDescriptorMapping = new()
        {
            { Kind.General, _descriptorSML001 },
            { Kind.InappropriateIntimacy, _descriptorSML002 },
            { Kind.LeakyAbstraction, _descriptorSML003 },
            { Kind.SpeculativeGenerality, _descriptorSML004 },
            { Kind.IndecentExposure, _descriptorSML005 },
            { Kind.VerticalSeparation, _descriptorSML006 },
            { Kind.MagicNumbers, _descriptorSML007 },
            { Kind.BloatedConstructor, _descriptorSML008 },
            { Kind.FeatureEnvy, _descriptorSML009 },
            { Kind.HiddenBehavior, _descriptorSML010 },
            { Kind.DataClump, _descriptorSML011 },
            { Kind.InconsistentNaming, _descriptorSML012 },
            { Kind.UncommunicativeNaming, _descriptorSML013 },
            { Kind.FallaciousNaming, _descriptorSML014 },
            { Kind.TemporalCoupling, _descriptorSML015 },
            { Kind.PrimitiveObsession, _descriptorSML016 },
        };

        private DiagnosticDescriptor GetDescriptorForKind(Kind kind)
            => _kindEnumDiagnosticDescriptorMapping[kind];
    }
}

﻿using CodeSmellAnnotations.Attributes;
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
        //
        // This seems to be a well-known issue:
        // https://github.com/dotnet/roslyn-analyzers/issues/5957
        // https://github.com/dotnet/roslyn-analyzers/issues/5890
        // https://github.com/docopt/docopt.net/pull/161
        // https://github.com/dotnet/roslyn-analyzers/issues/5828

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
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");


        private static readonly DiagnosticDescriptor _descriptorSML003
            = new DiagnosticDescriptor("SML003",
                "Leaky abastraction",
                "Leaky abastraction. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

        private static readonly DiagnosticDescriptor _descriptorSML004
            = new DiagnosticDescriptor("SML004",
                "Speculative generality",
                "Leaky abastraction. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

        private static readonly DiagnosticDescriptor _descriptorSML005
            = new DiagnosticDescriptor("SML005",
                "Indecent exposure",
                "Indecent exposure. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

        private static readonly DiagnosticDescriptor _descriptorSML006
            = new DiagnosticDescriptor("SML006",
                "Vertical separation",
                "Vertical separation. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

        private static readonly DiagnosticDescriptor _descriptorSML007
            = new DiagnosticDescriptor("SML007",
                "Magic numbers",
                "Magic numbers. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

        private static readonly DiagnosticDescriptor _descriptorSML008
            = new DiagnosticDescriptor("SML008",
                "Bloated constructor",
                "Bloated constructor. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

        private static readonly DiagnosticDescriptor _descriptorSML009
            = new DiagnosticDescriptor("SML009",
                "Feature envy",
                "Feature envy. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

        private static readonly DiagnosticDescriptor _descriptorSML010
            = new DiagnosticDescriptor("SML010",
                "Hidden behavior",
                "Hidden behavior. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

        private static readonly DiagnosticDescriptor _descriptorSML011
            = new DiagnosticDescriptor("SML011",
                "Data clump",
                "Data clump. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

        private static readonly DiagnosticDescriptor _descriptorSML012
            = new DiagnosticDescriptor("SML012",
                "Inconsistent naming",
                "Inconsistent naming. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

        private static readonly DiagnosticDescriptor _descriptorSML013
            = new DiagnosticDescriptor("SML013",
                "Uncommunicative naming",
                "Uncommunicative naming. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

        private static readonly DiagnosticDescriptor _descriptorSML014
            = new DiagnosticDescriptor("SML014",
                "Fallacious naming",
                "Fallacious naming. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

        private static readonly DiagnosticDescriptor _descriptorSML015
            = new DiagnosticDescriptor("SML015",
                "Temporal coupling",
                "Temporal coupling. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

        private static readonly DiagnosticDescriptor _descriptorSML016
            = new DiagnosticDescriptor("SML016",
                "Primitive obsession",
                "Primitive obsession. {0}",
                "CodeSmell",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: "Leaky abastraction",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

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
            { Kind.LekyAbstraction, _descriptorSML003 },
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

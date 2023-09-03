using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal class CodeSmellAttributeRule : IRule
    {
        // In order for the Roslyn's Release tracking analyzer to work,
        // the descriptor should be a field and the full "new" expression
        // should be used (the short one - new("SML00X",...) will break the analysis)
        private static readonly DiagnosticDescriptor _descriptor 
            = new DiagnosticDescriptor("SML001", 
                "Code smell", 
                "{0}{1}", 
                "CodeSmell",
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true,
                description: "General code smell annotation",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML001.md");

        public DiagnosticDescriptor Descriptor => _descriptor;

        public Type TriggeringAttributeType => typeof(CodeSmellAttribute);

        public string[] GetDiagnosticMessageArguments(AttributeSyntax attributeSyntax)
        {
            var kindAttributeValue = attributeSyntax.GetArgumentValueAsString(0);
            if (!_kindEnumStringToDisplayStringMapping.TryGetValue(kindAttributeValue, out string kindDisplayString))
            {
                throw new InvalidOperationException($"{nameof(Kind)} enum does not contain a member called {kindAttributeValue}");
            }

            var reason = attributeSyntax.GetArgumentValueAsString("Reason");

            return new[] 
            {
                kindDisplayString,
                string.IsNullOrEmpty(reason) ? null : $". {reason}" 
            };
        }
    

        private static Dictionary<string, string> _kindEnumStringToDisplayStringMapping = new()
        {
            { nameof(Kind.General), "Code smell" },
            { nameof(Kind.InappropriateIntimacy), "Inappropriate intimacy" },
            { nameof(Kind.LekyAbstraction), "Leaky abastraction" },
            { nameof(Kind.SpeculativeGenerality), "Speculative generality" },
            { nameof(Kind.IndecentExposure), "Indecent exposure" },
            { nameof(Kind.VerticalSeparation), "Vertical separation" },
            { nameof(Kind.MagicNumbers), "Magic numbers" },
            { nameof(Kind.BloatedConstructor), "Bloated constructor" },
            { nameof(Kind.FeatureEnvy), "Feature envy" },
            { nameof(Kind.HiddenBehavior), "Hidden behavior" },
            { nameof(Kind.DataClump), "Data clump" },
            { nameof(Kind.InconsistentNaming), "Inconsistent naming" },
            { nameof(Kind.UncommunicativeNaming), "Uncommunicative naming" },
            { nameof(Kind.FallaciousNaming), "Fallacious naming" },
            { nameof(Kind.TemporalCoupling), "Temporal coupling" },
            { nameof(Kind.PrimitiveObsession), "Primitive obsession" },
        };
    }
}

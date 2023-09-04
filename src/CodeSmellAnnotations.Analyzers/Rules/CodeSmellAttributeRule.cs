using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public string[] GetDiagnosticMessageArguments(IEnumerable<AttributeArgument> attributeArguments)
        {
            var kindAttributeValue = attributeArguments.ElementAtOrDefault(0);
            if (kindAttributeValue == null)
            {
                throw new InvalidOperationException($"Could not get the 'Kind' parameter value.");
            }

            var kindEnumValue = kindAttributeValue.GetEnumValue<Kind>();

            if (!_kindEnumStringToDisplayStringMapping.TryGetValue(kindEnumValue, out string kindDisplayString))
            {
                throw new InvalidOperationException($"{nameof(Kind)} enum does not contain a member called {kindEnumValue}");
            }

            var reason = attributeArguments.FirstOrDefault(a => a.Name == "Reason")?.Value?.ToString();

            return new[] 
            {
                kindDisplayString,
                string.IsNullOrEmpty(reason) ? null : $". {reason}" 
            };
        }

        private static Dictionary<Kind, string> _kindEnumStringToDisplayStringMapping = new()
        {
            { Kind.General, "Code smell" },
            { Kind.InappropriateIntimacy, "Inappropriate intimacy" },
            { Kind.LekyAbstraction, "Leaky abastraction" },
            { Kind.SpeculativeGenerality, "Speculative generality" },
            { Kind.IndecentExposure, "Indecent exposure" },
            { Kind.VerticalSeparation, "Vertical separation" },
            { Kind.MagicNumbers, "Magic numbers" },
            { Kind.BloatedConstructor, "Bloated constructor" },
            { Kind.FeatureEnvy, "Feature envy" },
            { Kind.HiddenBehavior, "Hidden behavior" },
            { Kind.DataClump, "Data clump" },
            { Kind.InconsistentNaming, "Inconsistent naming" },
            { Kind.UncommunicativeNaming, "Uncommunicative naming" },
            { Kind.FallaciousNaming, "Fallacious naming" },
            { Kind.TemporalCoupling, "Temporal coupling" },
            { Kind.PrimitiveObsession, "Primitive obsession" },
        };
    }
}

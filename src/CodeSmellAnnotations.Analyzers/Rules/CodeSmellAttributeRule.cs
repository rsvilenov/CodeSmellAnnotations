using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal partial class CodeSmellAttributeRule : IRule
    {
        public Type TriggeringAttributeType => typeof(CodeSmellAttribute);

        public Diagnosis GetDiagnosis(IEnumerable<AttributeArgument> attributeArguments)
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

            var diagnosticMessageArguments = new[] 
            {
                kindDisplayString,
                string.IsNullOrEmpty(reason) ? null : $". {reason}" 
            };

            return new Diagnosis
            {
                Descriptor = GetDescriptorForKind(kindEnumValue),
                DiagnosticMessageArguments = diagnosticMessageArguments
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

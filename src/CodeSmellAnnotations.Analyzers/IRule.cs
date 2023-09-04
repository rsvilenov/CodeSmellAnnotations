using System;
using System.Collections.Generic;

namespace CodeSmellAnnotations.Analyzers
{
    internal interface IRule : IHaveDescriptors
    {
        Type TriggeringAttributeType { get; }
        Diagnosis GetDiagnosis(IEnumerable<AttributeArgument> attributeArguments);
    }
}

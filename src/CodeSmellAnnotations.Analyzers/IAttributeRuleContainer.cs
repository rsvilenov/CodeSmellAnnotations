using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace CodeSmellAnnotations.Analyzers
{
    internal interface IAttributeRuleContainer
    {
        DiagnosticDescriptor[] Descriptors { get; }
        Type TriggeringAttributeType { get; }
        Diagnosis GetDiagnosis(IEnumerable<AttributeArgument> attributeArguments);
    }
}

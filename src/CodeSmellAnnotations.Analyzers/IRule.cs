using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace CodeSmellAnnotations.Analyzers
{
    internal interface IRule
    {
        DiagnosticDescriptor Descriptor { get; }
        Type TriggeringAttributeType { get; }
        string[] GetDiagnosticMessageArguments(IEnumerable<AttributeArgument> attributeArguments);
    }
}

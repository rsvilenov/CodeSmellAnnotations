using System;
using Microsoft.CodeAnalysis;

namespace CodeSmellAnnotations.Analyzers
{
    internal interface IHaveDescriptors
    {
        DiagnosticDescriptor[] Descriptors { get; }
    }
}

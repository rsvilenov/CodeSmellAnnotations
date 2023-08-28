using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace CodeSmellAnnotations.Analyzers
{
    internal interface IRule
    {
        DiagnosticDescriptor Descriptor { get; }
        Type TriggeringAttributeType { get; }
        string[] GetDiagnosticMessageArguments(AttributeSyntax attributeSyntax);
    }
}

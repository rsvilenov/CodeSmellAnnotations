using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeSmellAnnotations.Analyzers
{
    internal interface IRule
    {
        DiagnosticDescriptor Descriptor { get; }
        string TriggeringAttributeName { get; }
        string[] GetDiagnosticMessageArguments(AttributeSyntax attributeSyntax);
    }
}

using Microsoft.CodeAnalysis;

namespace CodeSmellAnnotations.Analyzers
{
    internal class Diagnosis
    {
        public DiagnosticDescriptor Descriptor { get; set; }
        public string[] DiagnosticMessageArguments { get; set; }
    }
}

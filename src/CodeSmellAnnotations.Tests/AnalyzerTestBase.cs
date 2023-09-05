using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeSmellAnnotations.Tests
{
    public class AnalyzerTestBase
    {
        private static readonly string _attributeAssemblyLocation = typeof(CodeSmellAnnotations.Attributes.CodeSmellAttribute).Assembly.Location;
        private static readonly string _analyzerAssemblyLocation = typeof(CodeSmellAnnotations.Analyzers.CodeSmellAnnotatationAnalyzer).Assembly.Location;
        
        protected async Task VerifyAnnotationAnalysis(string code, List<DiagnosticResult> expectedDiagnostics)
        {
            await Verify<Analyzers.CodeSmellAnnotatationAnalyzer>(code, expectedDiagnostics);
        }

        protected async Task VerifyAttributeParameterAnalysis(string code, List<DiagnosticResult> expectedDiagnostics)
        {
            await Verify<CodeSmellAnnotations.Analyzers.AttributeArgumentAnalyzer>(code, expectedDiagnostics);
        }

        private async Task Verify<TAnalyzer>(string code, List<DiagnosticResult> expectedDiagnostics)
            where TAnalyzer : DiagnosticAnalyzer, new()
        {
            var test = new Microsoft.CodeAnalysis.CSharp.Testing.CSharpAnalyzerTest<TAnalyzer, Microsoft.CodeAnalysis.Testing.Verifiers.XUnitVerifier>
            {
                TestCode = code
            };

            test.TestState.AdditionalReferences.Add(MetadataReference.CreateFromFile(_attributeAssemblyLocation));
            test.TestState.AdditionalReferences.Add(MetadataReference.CreateFromFile(_analyzerAssemblyLocation));
            test.ExpectedDiagnostics.AddRange(expectedDiagnostics);

            await test.RunAsync();
        }
    }
}

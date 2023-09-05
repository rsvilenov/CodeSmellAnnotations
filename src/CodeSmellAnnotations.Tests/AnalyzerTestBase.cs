using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnalyzerTest = Microsoft.CodeAnalysis.CSharp.Testing.CSharpAnalyzerTest<CodeSmellAnnotations.Analyzers.CodeSmellAnnotatationAnalyzer, Microsoft.CodeAnalysis.Testing.Verifiers.XUnitVerifier>;

namespace CodeSmellAnnotations.Tests
{
    public class AnalyzerTestBase
    {
        private static readonly string _attributeAssemblyLocation = typeof(CodeSmellAnnotations.Attributes.CodeSmellAttribute).Assembly.Location;
        private static readonly string _analyzerAssemblyLocation = typeof(CodeSmellAnnotations.Analyzers.CodeSmellAnnotatationAnalyzer).Assembly.Location;
        
        protected async Task Verify(string code, List<DiagnosticResult> expectedDiagnostics)
        {
            var test = new AnalyzerTest
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

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using System.Threading.Tasks;
using Xunit;
using AnalyzerTest = Microsoft.CodeAnalysis.CSharp.Testing.CSharpAnalyzerTest<CodeSmellAnnotations.Analyzers.CodeSmellAnnotatatedCodeAnalyzer, Microsoft.CodeAnalysis.Testing.Verifiers.XUnitVerifier>;

namespace CodeSmellAnnotations.Tests
{
    public class Test1
    {
        [Fact]
        public async Task BlobInputAttribute_String_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeFunction
                    {
                        [CodeSmell(""test"")]
                        public SomeFunction()
                        {
                        }
                    }
                }";

            var test = new AnalyzerTest
            {
                TestCode = testCode
            };

            test.TestState.AdditionalReferences.Add(MetadataReference.CreateFromFile(typeof(CodeSmellAnnotations.Attributes.CodeSmellAttribute).Assembly.Location));
            test.TestState.AdditionalReferences.Add(MetadataReference.CreateFromFile(typeof(CodeSmellAnnotations.Analyzers.CodeSmellAnnotatatedCodeAnalyzer).Assembly.Location));
            test.ExpectedDiagnostics.Add(new DiagnosticResult(
                "SML001",
                DiagnosticSeverity.Warning)
                .WithSpan(10, 32, 10, 44).WithArguments(": \"test\""));
            //test.ExpectedDiagnostics.Add(
            //    Verify.Diagnostic(new CodeSmellAnnotations.Analyzers.Rules.CodeSmellAttributeRule().Descriptor));

            await test.RunAsync();
        }
    }
}
using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CodeSmellAnnotations.Tests
{
    public class DuplicateOfCodeAttributeAnalysisTest : AnalyzerTestBase
    {
        private const string _duplicationDiagnosticId = "SML100";
        private const string _oddballSolutionDiagnosticId = "SML101";

        [Fact]
        public async Task DuplicatedCodeAttribute_OnClass_Diagnostics_Expected()
        {
            const string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [DuplicateOf(""test"")]
                    public class SomeClass
                    {
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_duplicationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(8, 34, 8, 43)
                    .WithArguments("Duplicates test.", "")
            });
        }

        [Fact]
        public async Task DuplicatedCodeAttribute_OnConstructor_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [DuplicateOf(""test"")]
                        public SomeClass()
                        {
                        }
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_duplicationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 32, 10, 41)
                    .WithArguments("Duplicates test.", "")
            });
        }

        [Fact]
        public async Task DuplicatedCodeAttribute_OnField_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [DuplicateOf(""test"")]
                        private string _field;
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_duplicationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 33, 10, 46)
                    .WithArguments("Duplicates test.", "")
            });
        }

        [Fact]
        public async Task DuplicatedCodeAttribute_OnAutoProperty_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [DuplicateOf(""test"")]
                        public bool IsTrueAuto { get; set; }
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_duplicationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 37, 10, 47)
                    .WithArguments("Duplicates test.", "")
            });
        }

        [Fact]
        public async Task DuplicatedCodeAttribute_OnProperty_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [DuplicateOf(""test"")]
                        public bool IsTrue 
                        {
                            get => true;
                        }
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                
                new DiagnosticResult(_duplicationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 37, 10, 43)
                    .WithArguments("Duplicates test.", "")
            });
        }

        [Fact]
        public async Task DuplicatedCodeAttribute_OnAccessor_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        public bool IsTrue 
                        {
                            [DuplicateOf(""test"")]
                            get => true;
                        }
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_duplicationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(11, 29, 12, 41)
                    .WithArguments("Duplicates test.", "")
            });
        }

        [Fact]
        public async Task DuplicatedCodeAttribute_MultipleAttributes_MultipleDiagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [DuplicateOf(""dup"", Kind = DuplicationKind.General)]
                    [DuplicateOf(""dup2"", Kind = DuplicationKind.OddballSolution)]
                    public class SomeClass
                    {
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult("SML100", DiagnosticSeverity.Warning)
                    .WithSpan(9, 34, 9, 43)
                    .WithArguments("Duplicates dup.", ""),
                new DiagnosticResult("SML101", DiagnosticSeverity.Warning)
                    .WithSpan(9, 34, 9, 43)
                    .WithArguments("Duplicates dup2.", "")
            });
        }

        [Theory]
        [InlineData(DuplicationKind.General, _duplicationDiagnosticId)]
        [InlineData(DuplicationKind.OddballSolution, _oddballSolutionDiagnosticId)]
        public async Task DuplicatedCodeAttribute_WithKindArgument_Diagnostics_Expected(DuplicationKind kind, string diagnosticId)
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [DuplicateOf(""test"", Kind = DuplicationKind." + kind.ToString() + @")]
                    public class SomeClass
                    {
                    }

                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(8, 34, 8, 43)
                    .WithArguments("Duplicates test.", "")
            });
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task SMLE002_Diagnostics_Expected(string duplicatesText)
        {
            const string diagnosticId = "SMLE002";
            string duplicatesParameter = duplicatesText != null 
                ? $@"(""{duplicatesText}"")" 
                : $@"(null)";
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [DuplicateOf" + duplicatesParameter + @"]
                    public class SomeClass
                    {
                    }
                }";

            await VerifyAttributeParameterAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(diagnosticId, DiagnosticSeverity.Error)
                    .WithSpan(7, 22, 7, 33 + (duplicatesParameter?.Length ?? 0))
            });
        }

        [Fact]
        public async Task SMLE002_Diagnostics_Not_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [DuplicateOf(""test"")]
                    public class SomeClass
                    {
                    }
                }";

            await VerifyAttributeParameterAnalysis(testCode, new List<DiagnosticResult>
            {
            });
        }
    }
}
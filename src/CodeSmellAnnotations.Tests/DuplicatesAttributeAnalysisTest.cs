using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CodeSmellAnnotations.Tests
{
    public class DuplicatedCodeAttributeAnalysisTest : AnalyzerTestBase
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
                    [Duplicates("")]
                    public class SomeClass
                    {
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_duplicationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(8, 34, 8, 43)
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
                        [Duplicates]
                        public SomeClass()
                        {
                        }
                    
                        public int Property1 { get; set; }
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_duplicationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 32, 10, 41)
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
                        [Duplicates]
                        private string _field;
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_duplicationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 33, 10, 46)
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
                        [Duplicates]
                        public bool IsTrueAuto { get; set; }
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_duplicationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 37, 10, 47)
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
                        [Duplicates]
                        public bool IsTrue 
                        {
                            get
                            {
                                return false;
                            }
                        }
                        
                        

                        public int Property1 { get; set; }
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                
                new DiagnosticResult(_duplicationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 37, 10, 43)
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
                            [Duplicates]
                            get
                            {
                                return false;
                            }
                        }
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_duplicationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(13, 29, 15, 30)
            });
        }

        [Fact]
        public async Task DuplicatedCodeAttribute_WithReasonArgument_DiagnosticsWithMessage_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [Duplicates(Reason = ""reason"")]
                    public class SomeClass
                    {
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_duplicationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(8, 34, 8, 43)
                    .WithArguments("", @" reason")
            });
        }

        [Fact]
        public async Task DuplicatedCodeAttribute_WithDuplicatesArgument_DiagnosticsWithMessage_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [Duplicates(Duplicates = ""OtherClass"")]
                    public class SomeClass
                    {
                        private int _fld;
                    }

                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_duplicationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(8, 34, 8, 43)
                    .WithArguments(@"Duplicates OtherClass.", "")
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
                    [Duplicates(Kind = DuplicationKind." + kind.ToString() + @")]
                    public class SomeClass
                    {
                        private int _fld;
                    }

                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(8, 34, 8, 43)
            });
        }
    }
}
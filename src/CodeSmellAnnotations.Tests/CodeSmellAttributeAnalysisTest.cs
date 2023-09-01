using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CodeSmellAnnotations.Tests
{
    public class CodeSmellAttributeAnalysisTest : AnalyzerTestBase
    {
        private const string _diagnosticId = "SML001";

        [Fact]
        public async Task CodeSmellAttribute_OnClass_Diagnostics_Expected()
        {
            const string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [CodeSmell]
                    public class SomeClass
                    {
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(8, 34, 8, 43)
            });
        }

        [Fact]
        public async Task CodeSmellAttribute_OnConstructor_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [CodeSmell]
                        public SomeClass()
                        {
                        }
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 32, 10, 41)
            });
        }

        [Fact]
        public async Task CodeSmellAttribute_OnField_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [CodeSmell]
                        private string _field;
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 33, 10, 46)
            });
        }

        [Fact]
        public async Task CodeSmellAttribute_OnAutoProperty_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [CodeSmell]
                        public bool IsTrueAuto { get; set; }
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 37, 10, 47)
            });
        }

        [Fact]
        public async Task CodeSmellAttribute_OnProperty_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [CodeSmell]
                        public bool IsTrue 
                        {
                            get
                            {
                                return false;
                            }
                        }
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                
                new DiagnosticResult(_diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 37, 10, 43)
            });
        }

        [Fact]
        public async Task CodeSmellAttribute_OnAccessor_Diagnostics_Expected()
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
                            [CodeSmell]
                            get
                            {
                                return false;
                            }
                        }
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(13, 29, 15, 30)
            });
        }

        [Fact]
        public async Task CodeSmellAttribute_WithReasonArgument_DiagnosticsWithMessage_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [CodeSmell(Reason = ""reason"")]
                    public class SomeClass
                    {
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(8, 34, 8, 43)
                    .WithArguments(@": ""reason""")
            });
        }

        [Fact]
        public async Task CodeSmellAttribute_WithConstructorArgument_DiagnosticsWithMessage_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [CodeSmell(""reason"")]
                    public class SomeClass
                    {
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(8, 34, 8, 43)
                    .WithArguments(@": ""reason""")
            });
        }
    }
}
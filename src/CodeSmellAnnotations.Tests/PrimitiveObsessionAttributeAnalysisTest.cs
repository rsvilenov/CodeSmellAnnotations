using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CodeSmellAnnotations.Tests
{
    public class PrimitiveObsessionAttributeAnalysisTest : AnalyzerTestBase
    {
        private const string _diagnosticId = "SML003";

        [Fact]
        public async Task PrimitiveObsessionAttribute_OnClass_Diagnostics_Expected()
        {
            const string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [PrimitiveObsession]
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
        public async Task PrimitiveObsessionAttribute_OnConstructor_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [PrimitiveObsession]
                        public SomeClass()
                        {
                        }
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult("CS0592", DiagnosticSeverity.Error)
                    .WithSpan(9, 26, 9, 44)
                    .WithArguments("PrimitiveObsession", "class, struct, method, property, indexer, interface")
            });
        }

        [Fact]
        public async Task PrimitiveObsessionAttribute_OnField_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [PrimitiveObsession]
                        private string _field;
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult("CS0592", DiagnosticSeverity.Error)
                    .WithSpan(9, 26, 9, 44)
                    .WithArguments("PrimitiveObsession", "class, struct, method, property, indexer, interface")
            });
        }

        [Fact]
        public async Task PrimitiveObsessionAttribute_OnAutoProperty_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [PrimitiveObsession]
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
        public async Task PrimitiveObsessionAttribute_OnProperty_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [PrimitiveObsession]
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
        public async Task PrimitiveObsessionAttribute_OnAccessor_Diagnostics_Expected()
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
                            [PrimitiveObsession]
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
        public async Task PrimitiveObsessionAttribute_WithReasonArgument_DiagnosticsWithMessage_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [PrimitiveObsession(Reason = ""reason"")]
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
        public async Task PrimitiveObsessionAttribute_WithConstructorArgument_DiagnosticsWithMessage_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [PrimitiveObsession(""reason"")]
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
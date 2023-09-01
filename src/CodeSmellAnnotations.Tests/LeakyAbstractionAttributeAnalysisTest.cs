using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CodeSmellAnnotations.Tests
{
    public class LeakyAbstractionAttributeAnalysisTest : AnalyzerTestBase
    {
        private const string _diagnosticId = "SML004";

        [Fact]
        public async Task LeakyAbstractionAttribute_OnClass_Diagnostics_Expected()
        {
            const string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [LeakyAbstraction]
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
        public async Task LeakyAbstractionAttribute_OnConstructor_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [LeakyAbstraction]
                        public SomeClass()
                        {
                        }
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult("CS0592", DiagnosticSeverity.Error)
                    .WithSpan(9, 26, 9, 42)
                    .WithArguments("LeakyAbstraction", "class, struct, method, property, indexer, interface")
            });
        }

        [Fact]
        public async Task LeakyAbstractionAttribute_OnField_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [LeakyAbstraction]
                        private string _field;
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult("CS0592", DiagnosticSeverity.Error)
                    .WithSpan(9, 26, 9, 42)
                    .WithArguments("LeakyAbstraction", "class, struct, method, property, indexer, interface")
            });
        }

        [Fact]
        public async Task LeakyAbstractionAttribute_OnAutoProperty_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [LeakyAbstraction]
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
        public async Task LeakyAbstractionAttribute_OnProperty_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [LeakyAbstraction]
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
        public async Task LeakyAbstractionAttribute_OnAccessor_Diagnostics_Expected()
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
                            [LeakyAbstraction]
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
        public async Task LeakyAbstractionAttribute_WithReasonArgument_DiagnosticsWithMessage_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [LeakyAbstraction(Reason = ""reason"")]
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
        public async Task LeakyAbstractionAttribute_WithConstructorArgument_DiagnosticsWithMessage_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [LeakyAbstraction(""reason"")]
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
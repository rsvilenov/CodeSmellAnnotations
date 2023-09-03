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
                        [CodeSmell(""reason"")]
                        public SomeClass()
                        {
                        }
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 32, 10, 41)
                    .WithArguments(@": ""reason""")
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
                        [CodeSmell(""reason"")]
                        private string _field;
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 33, 10, 46)
                    .WithArguments(@": ""reason""")
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
                        [CodeSmell(""reason"")]
                        public bool IsTrueAuto { get; set; }
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 37, 10, 47)
                    .WithArguments(@": ""reason""")
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
                        [CodeSmell(""reason"")]
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
                    .WithArguments(@": ""reason""")
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
                            [CodeSmell(""reason"")]
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
                    .WithArguments(@": ""reason""")
            });
        }
    }
}
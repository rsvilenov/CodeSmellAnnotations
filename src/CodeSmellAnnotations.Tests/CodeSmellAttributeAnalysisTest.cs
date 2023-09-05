using CodeSmellAnnotations.Attributes;
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
                    [CodeSmell(Kind.General, Reason = ""bad class"")]
                    public class SomeClass
                    {
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(8, 34, 8, 43)
                    .WithArguments("bad class")
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
                    public class SomeClass1
                    {
                        [CodeSmell(Kind.General, Reason = ""bad constructor"")]
                        public SomeClass1()
                        {
                        }

                        // This field is here to satisfy the compiler.
                        // Without it we get the following: 'System.InvalidOperationException: Cannot enqueue data after PromiseNotToEnqueue'
                        private string _field;
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 32, 10, 42)
                    .WithArguments("bad constructor")
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
                        [CodeSmell(Kind.General, Reason = ""bad field"")]
                        private string _field;
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 33, 10, 46)
                    .WithArguments("bad field")
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
                        [CodeSmell(Kind.General, Reason = ""bad auto property"")]
                        public bool IsTrueAuto { get; set; }
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 37, 10, 47)
                    .WithArguments("bad auto property")
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
                        [CodeSmell(Kind.General, Reason = ""bad property"")]
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
                    .WithArguments("bad property")
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
                            [CodeSmell(Kind.General, ""bad accessor"")]
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
                    .WithArguments("bad accessor")
            });
        }

        [Theory]
        [InlineData(Kind.InappropriateIntimacy, "SML002")]
        [InlineData(Kind.PrimitiveObsession, "SML016")]
        public async Task SML00X_Diagnostics_Expected(Kind kind, string diagnosticId)
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [CodeSmell(Kind." + kind.ToString() + @")]
                    public class SomeClass
                    {
                    }
                }";

            await Verify(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(8, 34, 8, 43)
            });
        }
    }
}
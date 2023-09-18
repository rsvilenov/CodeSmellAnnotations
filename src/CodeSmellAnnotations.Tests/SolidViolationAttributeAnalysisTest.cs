using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CodeSmellAnnotations.Tests
{
    public class SolidViolationAttributeAnalysisTest : AnalyzerTestBase
    {
        private const string _singleResponsibilityViolationDiagnosticId = "SML201";

        [Fact]
        public async Task SolidViolationAttribute_OnClass_Diagnostics_Expected()
        {
            const string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [SolidViolation(SolidPrinciple.SingleResponsibility, Reason = ""bad class"")]
                    public class SomeClass
                    {
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_singleResponsibilityViolationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(8, 34, 8, 43)
                    .WithArguments("bad class")
            });
        }

        [Fact]
        public async Task SolidViolationAttribute_OnConstructor_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass1
                    {
                        [SolidViolation(SolidPrinciple.SingleResponsibility, Reason = ""bad constructor"")]
                        public SomeClass1()
                        {
                        }
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_singleResponsibilityViolationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 32, 10, 42)
                    .WithArguments("bad constructor")
            });
        }

        [Fact]
        public async Task SolidViolationAttribute_OnField_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [SolidViolation(SolidPrinciple.SingleResponsibility, Reason = ""bad field"")]
                        private string _field;
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_singleResponsibilityViolationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 33, 10, 46)
                    .WithArguments("bad field")
            });
        }

        [Fact]
        public async Task SolidViolationAttribute_OnAutoProperty_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [SolidViolation(SolidPrinciple.SingleResponsibility, Reason = ""bad auto property"")]
                        public bool IsTrueAuto { get; set; }
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_singleResponsibilityViolationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 37, 10, 47)
                    .WithArguments("bad auto property")
            });
        }

        [Fact]
        public async Task SolidViolationAttribute_OnProperty_Diagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    public class SomeClass
                    {
                        [SolidViolation(SolidPrinciple.SingleResponsibility, Reason = ""bad property"")]
                        public bool IsTrue 
                        {
                            get
                            {
                                return false;
                            }
                        }
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                
                new DiagnosticResult(_singleResponsibilityViolationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(10, 37, 10, 43)
                    .WithArguments("bad property")
            });
        }

        [Fact]
        public async Task SolidViolationAttribute_OnAccessor_Diagnostics_Expected()
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
                            [SolidViolation(SolidPrinciple.SingleResponsibility, Reason = ""bad accessor"")]
                            get
                            {
                                return false;
                            }
                        }
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_singleResponsibilityViolationDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(13, 29, 15, 30)
                    .WithArguments("bad accessor")
            });
        }

        [Fact]
        public async Task SolidViolationAttribute_MultipleAttributes_MultipleDiagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [SolidViolation(SolidPrinciple.SingleResponsibility)]
                    [SolidViolation(SolidPrinciple.OpenClosed)]
                    public class SomeClass
                    {
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult("SML201", DiagnosticSeverity.Warning)
                    .WithSpan(9, 34, 9, 43),
                new DiagnosticResult("SML202", DiagnosticSeverity.Warning)
                    .WithSpan(9, 34, 9, 43)
            });
        }

        [Theory]
        [InlineData(SolidPrinciple.SingleResponsibility, "SML201")]
        [InlineData(SolidPrinciple.OpenClosed, "SML202")]
        [InlineData(SolidPrinciple.Liskov, "SML203")]
        [InlineData(SolidPrinciple.InterfaceSegregation, "SML204")]
        [InlineData(SolidPrinciple.DependencyInversion, "SML205")]
        public async Task SML20X_Diagnostics_Expected(SolidPrinciple solidPrinciple, string diagnosticId)
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [SolidViolation(SolidPrinciple." + solidPrinciple.ToString() + @")]
                    public class SomeClass
                    {
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
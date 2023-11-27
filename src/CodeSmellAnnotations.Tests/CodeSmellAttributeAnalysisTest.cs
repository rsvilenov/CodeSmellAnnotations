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
        private const string _generalCodeSmellDiagnosticId = "SML001";

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

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_generalCodeSmellDiagnosticId, DiagnosticSeverity.Warning)
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
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_generalCodeSmellDiagnosticId, DiagnosticSeverity.Warning)
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

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_generalCodeSmellDiagnosticId, DiagnosticSeverity.Warning)
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

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_generalCodeSmellDiagnosticId, DiagnosticSeverity.Warning)
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

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                
                new DiagnosticResult(_generalCodeSmellDiagnosticId, DiagnosticSeverity.Warning)
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
                            [CodeSmell(Kind.General, Reason = ""bad accessor"")]
                            get
                            {
                                return false;
                            }
                        }
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(_generalCodeSmellDiagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(13, 29, 15, 30)
                    .WithArguments("bad accessor")
            });
        }

        [Theory]
        [InlineData(Kind.InappropriateIntimacy, "SML002")]
        [InlineData(Kind.LeakyAbstraction, "SML003")]
        [InlineData(Kind.SpeculativeGenerality, "SML004")]
        [InlineData(Kind.IndecentExposure, "SML005")]
        [InlineData(Kind.VerticalSeparation, "SML006")]
        [InlineData(Kind.MagicNumbers, "SML007")]
        [InlineData(Kind.BloatedConstructor, "SML008")]
        [InlineData(Kind.FeatureEnvy, "SML009")]
        [InlineData(Kind.HiddenBehavior, "SML010")]
        [InlineData(Kind.DataClump, "SML011")]
        [InlineData(Kind.InconsistentNaming, "SML012")]
        [InlineData(Kind.UncommunicativeNaming, "SML013")]
        [InlineData(Kind.FallaciousNaming, "SML014")]
        [InlineData(Kind.TemporalCoupling, "SML015")]
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

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(diagnosticId, DiagnosticSeverity.Warning)
                    .WithSpan(8, 34, 8, 43)
            });
        }

        [Fact]
        public async Task CodeSmellAttribute_MultipleAttributes_MultipleDiagnostics_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [CodeSmell(Kind.PrimitiveObsession)]
                    [CodeSmell(Kind.InappropriateIntimacy)]
                    public class SomeClass
                    {
                    }
                }";

            await VerifyAnnotationAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult("SML016", DiagnosticSeverity.Warning)
                    .WithSpan(9, 34, 9, 43),
                new DiagnosticResult("SML002", DiagnosticSeverity.Warning)
                    .WithSpan(9, 34, 9, 43)
            });
        }

        [Theory]
        [InlineData(true, "")]
        [InlineData(true, null)]
        [InlineData(false, null)]
        public async Task SMLE001_Diagnostics_Expected(bool includeReason, string reason)
        {
            const string diagnosticId = "SMLE001";
            string reasonParameter = includeReason ? $@", Reason = ""{reason}""" : "";
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [CodeSmell(Kind.General" + reasonParameter + @")]
                    public class SomeClass
                    {
                    }
                }";

            await VerifyAttributeParameterAnalysis(testCode, new List<DiagnosticResult>
            {
                new DiagnosticResult(diagnosticId, DiagnosticSeverity.Error)
                    .WithSpan(7, 22, 7, 45 + reasonParameter.Length)
            });
        }

        [Fact]
        public async Task SMLE001_Diagnostics_Not_Expected()
        {
            string testCode = @"
                using System;
                using CodeSmellAnnotations.Attributes;

                namespace TestApp
                {
                    [CodeSmell(Kind.General, Reason = ""rsn"")]
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
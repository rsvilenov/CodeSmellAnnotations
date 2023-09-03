using CodeSmellAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellAnnotations
{
    [CodeSmell(Kind.InconsistentNaming)]
    [DuplicatedCode(Duplicates = "t", Reason = "test")]
    internal interface ICodeSmellAnnotations
    {

    }

    [CodeSmell(Kind.General, Reason = "refactor")]
    [DuplicatedCode(Reason = "test")]
    internal class SandboxClass1
    {
        //[CodeSmell("empty constructor", Kind.InconsistentNaming)]
        public SandboxClass1()
        {
            a1 = 1;
        }

        [CodeSmell(Kind.HiddenBehavior, Reason = "static method")]
        public static void Run()
        {

        }

        [CodeSmell(Kind.General, Reason = "casing")]
        struct struct1
        {

        }


        [CodeSmell(Kind.General, Reason = "do not use auto properties")]
        public int MyAutoProperty { get; set; }

        [CodeSmell(Kind.PrimitiveObsession, Reason = "do not use fields")]
        private readonly int a1;

        //[CodeSmell("refactor property")]
        [SolidViolation(SolidPrinciple.SingleResponsibility, Reason = "don't know why")]
        public int MyProperty
        {
            [CodeSmell(Kind.General, Reason = "refactor accessor")]
            get
            {
                return a1;
            }
        }
    }
}

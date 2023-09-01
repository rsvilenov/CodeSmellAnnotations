using CodeSmellAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellAnnotations
{
    [CodeSmell("empty interface")]
    [DuplicateCode(Duplicates = nameof(SandboxClass1), Reason = "test")]
    [PrimitiveObsession(Reason = "test")]
    [LeakyAbstraction(Reason = "exposes members")]
    internal interface ICodeSmellAnnotations
    {

    }

    [CodeSmell("refactor")]
    [DuplicateCode(Reason = "test")]
    internal class SandboxClass1
    {
        [CodeSmell(Reason = "empty constructor")]
        public SandboxClass1()
        {
            a1 = 1;
        }

        [CodeSmell("static method")]
        public static void Run()
        {

        }

        [CodeSmell("casing")]
        struct struct1
        {

        }


        [CodeSmell("do not use auto properties")]
        public int MyAutoProperty { get; set; }

        [CodeSmell("do not use fields")]
        private readonly int a1;

        //[CodeSmell("refactor property")]
        [SolidViolation(SolidPrinciple.SingleResponsibility, Reason = "don't know why")]
        public int MyProperty 
        {
            [CodeSmell("refactor accessor")]
            get 
            { 
                return a1; 
            } 
        }
    }
}

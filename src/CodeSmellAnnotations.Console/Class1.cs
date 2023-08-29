using CodeSmellAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellAnnotations
{
    [CodeSmell("empty interface")]
    [DuplicateCode(Duplicates = nameof(Class1), Reason = "test")]
    [PrimitiveObsession(Reason = "test")]
    [LeakyAbstraction(Reason = "exposes members")]
    internal interface ICodeSmellAnnotations
    {

    }

    [CodeSmell("refactor")]
    internal class Class1
    {
        [CodeSmell("empty constructor")]
        public Class1()
        {
            a1 = 1;
        }

        [CodeSmell("casing")]
        struct struct1
        {

        }

        [CodeSmell("do not use fields")]
        private readonly int a1;

        //[CodeSmell("refactor property")]
        [SolidViolation(SolidPrinciple.OpenClosed, Reason = "don't know why")]
        public int MyProperty 
        {
            // [CodeSmell("refactor accessor")]
            get 
            { 
                return a1; 
            } 
        }
    }
}

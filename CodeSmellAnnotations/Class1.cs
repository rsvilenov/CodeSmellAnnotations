using CodeSmellAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellAnnotations
{
    [CodeSmell("empty interface")]
    [DuplicateCode(Duplicates = nameof(Class1))]
    internal interface ICodeSmellAnnotations
    {

    }

    [CodeSmell("refactor")]
    internal class Class1
    {
        [CodeSmell("empty constructor")]
        public Class1()
        {

        }

        [CodeSmell("casing")]
        struct struct1
        {

        }

        [CodeSmell("do not use fields")]
        private int a1;

        [CodeSmell("refactor property")]
        public int MyProperty 
        {
            [CodeSmell("refactor accessor")]
            get 
            { 
                return 1; 
            } 
        }
    }
}

using CodeSmellAnnotations.Attributes;

namespace Demo
{
    public class StoreItem
    {
        public string Name { get; set; }

        [CodeSmell(Kind.PrimitiveObsession, Reason = "Use a custom money class with currency info")]
        public decimal Price { get; set; }
    }
}



//namespace CodeSmellAnnotations
//{
//    [CodeSmell(Kind.InconsistentNaming)]
//    [DuplicateOf("t", Kind = DuplicationKind.OddballSolution, Reason = "test")]
//    internal interface ICodeSmellAnnotations
//    {

//    }

//    [CodeSmell(Kind.General, Reason = "aa")]
//    [DuplicateOf("a", Reason = "test")]
//    [SolidViolation(SolidPrinciple.InterfaceSegregation)]
//    internal class SandboxClass1
//    {
//        //[CodeSmell("empty constructor", Kind.InconsistentNaming)]
//        public SandboxClass1()
//        {
//            a1 = 1;
//        }

//        [CodeSmell(Kind.HiddenBehavior, Reason = "Can be run from somewhere")]
//        public static void Run()
//        {

//        }

//        [CodeSmell(Kind.General, Reason = "casing")]
//        struct struct1
//        {

//        }


//        [CodeSmell(Kind.General, Reason = "do not use auto properties")]
//        public int MyAutoProperty { get; set; }

//        [CodeSmell(Kind.PrimitiveObsession, Reason = "do not use fields")]
//        private readonly int a1;

//        //[CodeSmell("refactor property")]
//        [SolidViolation(SolidPrinciple.SingleResponsibility, Reason = "don't know why")]
//        public int MyProperty
//        {
//            [CodeSmell(Kind.General, Reason = "refactor accessor")]
//            get
//            {
//                return a1;
//            }
//        }
//       }
//}

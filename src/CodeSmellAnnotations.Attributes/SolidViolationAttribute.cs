using System;

namespace CodeSmellAnnotations.Attributes
{
    [AttributeUsage(AttributeTargets.Property |
        AttributeTargets.Method |
        AttributeTargets.Field |
        AttributeTargets.Class |
        AttributeTargets.Constructor |
        AttributeTargets.Interface |
        AttributeTargets.Struct)]
    public class SolidViolationAttribute : Attribute
    {
        public SolidPrinciple Violates { get; set; }
        public string Reason { get; set; }
        public SolidViolationAttribute(SolidPrinciple violates)
        {
            Violates = violates;
        }
    }
}

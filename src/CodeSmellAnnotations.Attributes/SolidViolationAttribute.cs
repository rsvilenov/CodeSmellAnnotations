using System;

namespace CodeSmellAnnotations.Attributes
{
    /// <summary>
    /// <para>An annotation for code which violates one or more SOLID principle.</para>
    /// <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/CodeSmells/SolidViolation.md">Read more</see>
    /// </summary>
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

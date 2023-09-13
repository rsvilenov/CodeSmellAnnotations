using System;

namespace CodeSmellAnnotations.Attributes
{
    /// <summary>
    /// <para>General code smell annotation.</para>
    /// <see href="https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/attributes/CodeSmellAttribute.md">Read more</see>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property |
        AttributeTargets.Method | 
        AttributeTargets.Field | 
        AttributeTargets.Class | 
        AttributeTargets.Constructor | 
        AttributeTargets.Interface | 
        AttributeTargets.Struct)]
    public class CodeSmellAttribute : Attribute
    {
        public Kind Kind { get; set; }
        public string Reason { get; set; }
        public CodeSmellAttribute(Kind kind)
        {
            Kind = kind;
        }
    }
}

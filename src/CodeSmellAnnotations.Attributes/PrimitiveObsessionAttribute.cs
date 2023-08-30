using System;

namespace CodeSmellAnnotations.Attributes
{
    /// <summary>
    /// <para>An annotation for code which suffers from primitive obsession.</para>
    /// <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/CodeSmells/DuplicateCode.md">Read more</see>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property |
        AttributeTargets.Method | 
        AttributeTargets.Class | 
        AttributeTargets.Interface | 
        AttributeTargets.Struct)]
    public class PrimitiveObsessionAttribute : Attribute
    {
        public string Reason { get; set; }
        public PrimitiveObsessionAttribute(string reason = null)
        {
            Reason = reason;
        }
    }
}

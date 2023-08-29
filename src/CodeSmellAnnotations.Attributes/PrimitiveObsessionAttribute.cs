using System;

namespace CodeSmellAnnotations.Attributes
{
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

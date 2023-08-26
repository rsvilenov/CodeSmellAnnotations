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
    public class CodeSmellAttribute : Attribute
    {
        public string Reason { get; set; }
        public CodeSmellAttribute(string reason = null)
        {
            Reason = reason;
        }
    }
}

using System;

namespace CodeSmellAnnotations.Attributes
{
    [AttributeUsage(AttributeTargets.Property |
           AttributeTargets.Method |
           AttributeTargets.Class |
           AttributeTargets.Interface |
           AttributeTargets.Struct)]
    public class LeakyAbstractionAttribute : Attribute
    {
        public string Reason { get; set; }
        public LeakyAbstractionAttribute(string reason = null)
        {
            Reason = reason;
        }
    }
}

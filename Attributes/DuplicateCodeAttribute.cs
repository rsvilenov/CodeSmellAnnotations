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
    public class DuplicateCodeAttribute : Attribute
    {
        public string Duplicates { get; set; }
        public string Reason { get; set; }
    }
}

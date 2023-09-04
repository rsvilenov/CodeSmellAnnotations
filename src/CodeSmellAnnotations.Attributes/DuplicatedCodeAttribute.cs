using System;

namespace CodeSmellAnnotations.Attributes
{
    /// <summary>
    /// <para>An annotation for code which duplicate another portion of code in the codebase.</para>
    /// <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/CodeSmells/DuplicateCode.md">Read more</see>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property |
              AttributeTargets.Method |
              AttributeTargets.Field |
              AttributeTargets.Class |
              AttributeTargets.Constructor |
              AttributeTargets.Interface |
              AttributeTargets.Struct)]
    public class DuplicatedCodeAttribute : Attribute
    {
        public DuplicationKind Kind { get; set; }
        public string Duplicates { get; set; }
        public string Reason { get; set; }
    }
}

using System;

namespace CodeSmellAnnotations.Attributes
{
    /// <summary>
    /// <para>An annotation for leaky abstraction.</para>
    /// <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/CodeSmells/LeakyAbstraction.md">Read more</see>
    /// </summary>
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

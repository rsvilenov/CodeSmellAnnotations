﻿using System;

namespace CodeSmellAnnotations.Attributes
{
    /// <summary>
    /// <para>An annotation for code which duplicate another portion of code in the codebase.</para>
    /// <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/attributes/DuplicateOfAttribute.md">Read more</see>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property |
              AttributeTargets.Method |
              AttributeTargets.Field |
              AttributeTargets.Class |
              AttributeTargets.Constructor |
              AttributeTargets.Interface |
              AttributeTargets.Struct, AllowMultiple = true)]
    public class DuplicateOfAttribute : Attribute
    {
        public DuplicateOfAttribute(string duplicates)
        {
            Duplicates = duplicates;
        }

        public string Duplicates { get; }
        public DuplicationKind Kind { get; set; }
        public string Reason { get; set; }
    }
}

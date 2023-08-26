using System;

namespace CodeSmellAnnotations.Attributes
{
    public class DuplicateCodeAttribute : Attribute
    {
        public string Duplicates { get; set; }
        public string Message { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSmellAnnotations.Analyzers.Extensions
{
    internal static class AttributeArgumentExtensions
    {

        public static TEnum GetEnumValue<TEnum>(this AttributeArgument attributeArgument)
             where TEnum : System.Enum
        {
            var type = typeof(TEnum);

            if (attributeArgument.Value == null)
            {
                throw new InvalidOperationException($"Could not get the '{type.Name}' parameter value.");
            }

            if (!Enum.IsDefined(type, attributeArgument.Value))
            {
                throw new InvalidOperationException($"Could not parse the '{type.Name}' parameter value.");
            }

            return (TEnum)attributeArgument.Value;
        }
    }
}

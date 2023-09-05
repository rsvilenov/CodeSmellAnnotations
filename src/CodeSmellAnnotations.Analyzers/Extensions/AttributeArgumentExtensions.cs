using System;

namespace CodeSmellAnnotations.Analyzers.Extensions
{
    internal static class AttributeArgumentExtensions
    {
        public static TEnum GetEnumValue<TEnum>(this AttributeArgument attributeArgument)
             where TEnum : Enum
        {
            var type = typeof(TEnum);
            var value = attributeArgument.Value;

            if (value is null)
            {
                throw new InvalidOperationException($"Could not get the '{type.Name}' parameter value.");
            }

            if (!Enum.IsDefined(type, value))
            {
                throw new InvalidOperationException($"Enum '{type.Name}' does not contain a value of {value}.");
            }

            return (TEnum)value;
        }
    }
}

using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace CodeSmellAnnotations.Analyzers.Extensions
{
    internal static class AttributeSyntaxExtensions
    {
        public static string GetArgumentValueAsString(this AttributeSyntax attributeSyntax, int argumentIndex)
        {
            var argumentSyntax = attributeSyntax
                ?.ArgumentList
                ?.Arguments
                .ElementAtOrDefault(argumentIndex);

            if (argumentSyntax == null) return null;

            return GetArgumentValueAsString(argumentSyntax);
        }

        public static string GetArgumentValueAsString(this AttributeSyntax attributeSyntax, string name)
        {
            var argumentSyntax = attributeSyntax
                ?.ArgumentList
                ?.Arguments
                .FirstOrDefault(a => name == null || GetArgumentName(a) == name);

            if (argumentSyntax == null) return null;

            return GetArgumentValueAsString(argumentSyntax);
        }

        private static string GetArgumentValueAsString(AttributeArgumentSyntax argumentSyntax)
        {
            if (argumentSyntax.Expression is MemberAccessExpressionSyntax memberAccessExpression)
                return memberAccessExpression.Name.Identifier.ValueText;

            return argumentSyntax.Expression.ToString();
        }

        public static string GetArgumentName(this AttributeArgumentSyntax a)
        {
            return a.NameColon?.Name?.ToString() ?? a.NameEquals?.Name?.ToString();
        }
    }
}

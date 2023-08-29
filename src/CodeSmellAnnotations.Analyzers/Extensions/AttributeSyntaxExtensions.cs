using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace CodeSmellAnnotations.Analyzers.Extensions
{
    internal static class AttributeSyntaxExtensions
    {
        public static string GetStringArgumentValue(this AttributeSyntax attributeSyntax, string name = null)
        {
            var argumentSyntax = attributeSyntax
                ?.ArgumentList
                ?.Arguments
                .FirstOrDefault(a => name == null || GetArgumentName(a) == name);

            if (argumentSyntax == null) return null;

            if (argumentSyntax.Expression is MemberAccessExpressionSyntax memberAccessExpression)
                return memberAccessExpression.Name.Identifier.ValueText;

            return argumentSyntax.Expression.ToString();

            /* 
                var argumentValue = semanticModel.GetConstantValue(argumentSyntax.Expression).Value;

                var argumentName = argumentSyntax.Expression.NormalizeWhitespace().ToFullString();
            */
        }

        private static string GetArgumentName(AttributeArgumentSyntax a)
        {
            return a.NameColon?.Name?.ToString() ?? a.NameEquals?.Name?.ToString();
        }
    }
}

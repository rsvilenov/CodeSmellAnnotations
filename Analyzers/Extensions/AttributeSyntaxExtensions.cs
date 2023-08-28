using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace CodeSmellAnnotations.Analyzers.Extensions
{
    public static class AttributeSyntaxExtensions
    {
        public static string GetStringArgumentValue(this AttributeSyntax attributeSyntax, string name = null)
        {
            var argumentSyntax = attributeSyntax
                ?.ArgumentList
                ?.Arguments
                .FirstOrDefault(a =>
                {
                    if (name == null)
                    {
                        return true;
                    }

                    if (a.NameColon != null)
                    {
                        return a.NameColon.Name.ToString() == name;
                    }
                    else if (a.NameEquals != null)
                    {
                        return a.NameEquals.Name.ToString() == name;
                    }

                    return false;
                });

            if (argumentSyntax == null)
            {
                return null;
            }

            if (argumentSyntax.Expression is MemberAccessExpressionSyntax memberAccessExpression)
            {
                return memberAccessExpression.Name.Identifier.ValueText;
            }
            
             return argumentSyntax.Expression.ToString();
            // return argumentSyntax.Expression.NormalizeWhitespace().ToFullString();
        }
    }
}

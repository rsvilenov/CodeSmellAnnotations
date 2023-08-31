using Microsoft.CodeAnalysis;

namespace CodeSmellAnnotations.Analyzers.Extensions
{
    internal static class SymbolExtensions
    {
        public static bool CompareTo(this ISymbol thisSymbol, ISymbol symbol)
        {
            return SymbolEqualityComparer.Default.Equals(thisSymbol, symbol);
        }
    }
}

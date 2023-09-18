namespace CodeSmellAnnotations.Attributes
{
    public enum DuplicationKind
    {
        /// <summary>
        /// <para>The code fully duplicate another piece of code/funcionality.</para>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML100.md">SML100</see>.
        /// </summary>
        General,

        /// <summary>
        /// <para>A duplicate solution of the same problem.  <br />
        /// There should only be one way of solving the same problem in the code.</para>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML101.md">SML101</see>.
        /// </summary>
        OddballSolution
    }
}

namespace CodeSmellAnnotations.Attributes
{
    public enum DuplicationKind
    {
        /// <summary>
        /// The code fully duplicate another piece of code/funcionality
        /// </summary>
        General,

        /// <summary>
        /// A duplicate solution of the same problem. 
        /// There should only be one way of solving the same problem in the code.
        /// </summary>
        OddballSolution
    }
}

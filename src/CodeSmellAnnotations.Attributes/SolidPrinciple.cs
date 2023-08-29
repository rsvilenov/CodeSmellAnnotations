namespace CodeSmellAnnotations.Attributes
{
    /// <summary>
    /// <para>SOLID principles</para>
    /// <see href="https://en.wikipedia.org/wiki/SOLID">Read more</see>
    /// </summary>
    public enum SolidPrinciple
    {
        /// <summary>
        /// <para>The Single Responsibility Principle</para>
        /// <para>"A class should have one, and only one, reason to change." -- Uncle Bob</para>
        /// <see href="https://en.wikipedia.org/wiki/Single-responsibility_principle">Read more on SRP here</see>
        /// </summary>
        SingleResponsibility,

        /// <summary>
        /// <para>The Open Closed Principle</para>
        /// <para>"You should be able to extend a classes behavior, without modifying it." -- Uncle Bob</para>
        /// <see href="https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle">Read more on OCP here</see>
        /// </summary>
        OpenClosed,

        /// <summary>
        /// <para>The Liskov Substitution Principle</para>
        /// <para>"Derived classes must be substitutable for their base classes." -- Uncle Bob</para>
        /// <see href="https://en.wikipedia.org/wiki/Liskov_substitution_principle">Read more on LSP here</see>
        /// </summary>
        Liskov,

        /// <summary>
        /// <para>The Interface Segregation Principle</para>
        /// <para>"Make fine grained interfaces that are client specific." -- Uncle Bob</para>
        /// <see href="https://en.wikipedia.org/wiki/Interface_segregation_principle">Read more on ISP here</see>
        /// </summary>
        InterfaceSegregation,

        /// <summary>
        /// <para>The Dependency Inversion Principle</para>
        /// <para>"Depend on abstractions, not on concretions." -- Uncle Bob</para>
        /// <see href="https://en.wikipedia.org/wiki/Dependency_inversion_principle">Read more on DIP here</see>
        /// </summary>
        DependencyInversion
    }
}

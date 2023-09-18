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
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML201.md">SML201</see>.
        /// </summary>
        SingleResponsibility,

        /// <summary>
        /// <para>The Open Closed Principle</para>
        /// <para>"You should be able to extend a classes behavior, without modifying it." -- Uncle Bob</para>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML202.md">SML202</see>.
        /// </summary>
        OpenClosed,

        /// <summary>
        /// <para>The Liskov Substitution Principle</para>
        /// <para>"Derived classes must be substitutable for their base classes." -- Uncle Bob</para>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML203.md">SML203</see>.
        /// </summary>
        Liskov,

        /// <summary>
        /// <para>The Interface Segregation Principle</para>
        /// <para>"Make fine grained interfaces that are client specific." -- Uncle Bob</para>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML204.md">SML204</see>.
        /// </summary>
        InterfaceSegregation,

        /// <summary>
        /// <para>The Dependency Inversion Principle</para>
        /// <para>"Depend on abstractions, not on concretions." -- Uncle Bob</para>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML205.md">SML205</see>.
        /// </summary>
        DependencyInversion
    }
}

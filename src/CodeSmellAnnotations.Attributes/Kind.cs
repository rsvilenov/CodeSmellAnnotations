namespace CodeSmellAnnotations.Attributes
{
    /// <summary>
    /// <para>Common code smells</para>
    /// <see href="https://en.wikipedia.org/wiki/SOLID">Read more</see>
    /// </summary>
    public enum Kind
    {
        /// <summary>
        /// General code smell. "Reason" should be provided.
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML001.md">SML001</see>.
        /// </summary>
        General,
        /// <summary>
        /// Impossible to understand what the named component does.
        /// Stick to the principle of least astonishment (POLA).
        /// </summary>
        UncommunicativeNaming,
        /// <summary>
        /// The name does not describe the behavior accurately.
        /// Stick to the principle of least astonishment (POLA).
        /// </summary>
        FallaciousNaming,
        /// <summary>
        /// 'Foo' should be named 'foo' everywhere in the code.
        /// Stick to the principle of least astonishment (POLA).
        /// </summary>
        InconsistentNaming,
        LekyAbstraction,
        PrimitiveObsession,
        FeatureEnvy,
        SpeculativeGenerality,
        IndecentExposure,
        InappropriateIntimacy,
        /// <summary>
        /// Also known as Sequential coupling
        /// </summary>
        TemporalCoupling,
        MagicNumbers,
        DataClump,
        BloatedConstructor,
        HiddenBehavior,
        /// <summary>
        /// Using of regions or method grouping.
        /// </summary>
        VerticalSeparation
    }
}

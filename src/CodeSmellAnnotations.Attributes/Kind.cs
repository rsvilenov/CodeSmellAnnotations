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
        /// SML002
        /// </summary>
        InappropriateIntimacy,

        /// <summary>
        /// SML003
        /// </summary>
        LekyAbstraction,

        /// <summary>
        /// SML004
        /// </summary>
        SpeculativeGenerality,

        /// <summary>
        /// SML005
        /// </summary>
        IndecentExposure,

        /// <summary>
        /// SML006. Using of regions or method grouping.
        /// </summary>
        VerticalSeparation,

        /// <summary>
        /// SML007
        /// </summary>
        MagicNumbers,

        /// <summary>
        /// SML008
        /// </summary>
        BloatedConstructor,

        /// <summary>
        /// SML009
        /// </summary>
        FeatureEnvy,

        /// <summary>
        /// SML010
        /// </summary>
        HiddenBehavior,

        /// <summary>
        /// SML011
        /// </summary>
        DataClump,

        /// <summary>
        /// SML012
        /// 'Foo' should be named 'foo' everywhere in the code.
        /// Stick to the principle of least astonishment (POLA).
        /// </summary>
        InconsistentNaming,

        /// <summary>
        /// SML013
        /// Impossible to understand what the named component does.
        /// Stick to the principle of least astonishment (POLA).
        /// </summary>
        UncommunicativeNaming,

        /// <summary>
        /// SML014
        /// The name does not describe the behavior accurately.
        /// Stick to the principle of least astonishment (POLA).
        /// </summary>
        FallaciousNaming,

        /// <summary>
        /// SML015
        /// Also known as Sequential coupling
        /// </summary>
        TemporalCoupling,

        /// <summary>
        /// SML016
        /// </summary>
        PrimitiveObsession
    }
}

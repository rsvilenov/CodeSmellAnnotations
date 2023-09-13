namespace CodeSmellAnnotations.Attributes
{
    /// <summary>
    /// <para>Common code smells</para>
    /// </summary>
    public enum Kind
    {
        /// <summary>
        /// General code smell. "Reason" should be provided.
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML001.md">SML001</see>.
        /// </summary>
        General,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML002.md">SML002</see>.
        /// </summary>
        InappropriateIntimacy,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML003.md">SML003</see>.
        /// </summary>
        LekyAbstraction,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML004.md">SML004</see>.
        /// </summary>
        SpeculativeGenerality,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML005.md">SML005</see>.
        /// </summary>
        IndecentExposure,

        /// <summary>
        /// Using of regions or method grouping.
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML006.md">SML006</see>.
        /// </summary>
        VerticalSeparation,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML007.md">SML007</see>.
        /// </summary>
        MagicNumbers,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML008.md">SML008</see>.
        /// </summary>
        BloatedConstructor,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML009.md">SML009</see>.
        /// </summary>
        FeatureEnvy,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML010.md">SML010</see>.
        /// </summary>
        HiddenBehavior,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML011.md">SML011</see>.
        /// </summary>
        DataClump,

        /// <summary>
        /// 'Foo' should be named 'foo' everywhere in the code.
        /// Stick to the principle of least astonishment (POLA).
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML012.md">SML012</see>.
        /// </summary>
        InconsistentNaming,

        /// <summary>
        /// Impossible to understand what the named component does.
        /// Stick to the principle of least astonishment (POLA).
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML002.md">SML013</see>.
        /// </summary>
        UncommunicativeNaming,

        /// <summary>
        /// The name does not describe the behavior accurately.
        /// Stick to the principle of least astonishment (POLA).
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML014.md">SML014</see>.
        /// </summary>
        FallaciousNaming,

        /// <summary>
        /// Also known as Sequential coupling
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML015.md">SML015</see>.
        /// </summary>
        TemporalCoupling,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/docs/rules/SML016.md">SML016</see>.
        /// </summary>
        PrimitiveObsession
    }
}

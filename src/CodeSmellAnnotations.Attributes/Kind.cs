namespace CodeSmellAnnotations.Attributes
{
    /// <summary>
    /// <para>Common code smells</para>
    /// </summary>
    public enum Kind
    {
        /// <summary>
        /// General code smell. <br/>
        /// A value for the "Reason" parameter must be provided.<br/><br/>
        /// <para>Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML001.md">SML001</see>.</para>
        /// </summary>
        General,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML002.md">SML002</see>.
        /// </summary>
        InappropriateIntimacy,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML003.md">SML003</see>.
        /// </summary>
        LeakyAbstraction,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML004.md">SML004</see>.
        /// </summary>
        SpeculativeGenerality,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML005.md">SML005</see>.
        /// </summary>
        IndecentExposure,

        /// <summary>
        /// Using of regions or method grouping.<br/><br/>
        /// <para>Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML006.md">SML006</see>.</para>
        /// </summary>
        VerticalSeparation,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML007.md">SML007</see>.
        /// </summary>
        MagicNumbers,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML008.md">SML008</see>.
        /// </summary>
        BloatedConstructor,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML009.md">SML009</see>.
        /// </summary>
        FeatureEnvy,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML010.md">SML010</see>.
        /// </summary>
        HiddenBehavior,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML011.md">SML011</see>.
        /// </summary>
        DataClump,

        /// <summary>
        /// 'Foo' should be named 'foo' everywhere in the code.<br/>
        /// We should stick to the <see href="https://en.wikipedia.org/wiki/Principle_of_least_astonishment">principle of least astonishment (POLA)</see>.<br/><br/>
        /// <para>Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML012.md">SML012</see>.</para>
        /// </summary>
        InconsistentNaming,

        /// <summary>
        /// Impossible to understand what the named component does.<br/>
        /// We should stick to the <see href="https://en.wikipedia.org/wiki/Principle_of_least_astonishment">principle of least astonishment (POLA)</see>.<br/><br/>
        /// <para>Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML013.md">SML013</see>.</para>
        /// </summary>
        UncommunicativeNaming,

        /// <summary>
        /// The name does not describe the behavior accurately.<br/>
        /// We should stick to the <see href="https://en.wikipedia.org/wiki/Principle_of_least_astonishment">principle of least astonishment (POLA)</see>.<br/><br/>
        /// <para>Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML014.md">SML014</see>.</para>
        /// </summary>
        FallaciousNaming,

        /// <summary>
        /// Also known as Sequential coupling.<br/><br/>
        /// <para>Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML015.md">SML015</see>.</para>
        /// </summary>
        TemporalCoupling,

        /// <summary>
        /// Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML016.md">SML016</see>.
        /// </summary>
        PrimitiveObsession,

        /// <summary>
        /// Hiding of errors or error details.<br/><br/>
        /// <para>Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML017.md">SML017</see>.</para>
        /// </summary>
        ErrorSwallowing,

        /// <summary>
        /// Code, which should not be in this component.<br/><br/>
        /// <para>Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML018.md">SML018</see>.</para>
        /// </summary>
        DoesNotBelong,

        /// <summary>
        /// Wrong abstraction.<br/><br/>
        /// <para>Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML019.md">SML019</see>.</para>
        /// </summary>
        WrongAbstraction,

        /// <summary>
        /// This code belongs to another layer.<br/><br/>
        /// <para>Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML020.md">SML020</see>.</para>
        /// </summary>
        WrongLayer,

        /// <summary>
        /// Wrong use of inheritance.<br/><br/>
        /// <para>Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML021.md">SML021</see>.</para>
        /// </summary>
        InheritanceAbuse,

        /// <summary>
        /// An interface or api, which decieves the programmer about its purpose/usage.<br/><br/>
        /// <para>Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML022.md">SML022</see>.</para>
        /// </summary>
        DeceptiveDesign,

        /// <summary>
        /// Possible race conditions in the code. <br/>
        /// Not technically a code smell, but such buggy code often stays unattended for a long time. See <see href="https://en.wikipedia.org/wiki/Heisenbug">Heisenbug</see>.<br/><br/>
        /// <para>Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML023.md">SML023</see>.</para>
        /// </summary>
        RaceCondition,

        /// <summary>
        /// Security concern. <br/>
        /// Not technically a code smell, but, as with RaceCondition, such buggy code often stays unattended for a long time.<br/><br/>
        /// <para>Corresponds to diagnostic rule <see href="https://github.com/rsvilenov/CodeSmellAnnotations/blob/master/docs/rules/SML024.md">SML024</see>.</para>
        /// </summary>
        SecurityConcern
    }
}

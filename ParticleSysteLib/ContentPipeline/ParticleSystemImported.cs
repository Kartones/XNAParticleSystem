namespace ParticleSystemLib
{
    /// <summary>
    /// Data of a particle system that has been imported by the content pipeline.
    /// </summary>
    public class ParticleSystemImported
    {
        #region Fields

        /// <summary>
        /// Definition of the system as concatenated string of key-value pairs.
        /// </summary>
        private string _systemDefinition;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the definition of the particle system.
        /// </summary>
        public string SystemDefinition
        {
            get { return _systemDefinition; }
            set { _systemDefinition = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="systemDefinition">Definition of the particle system.</param>
        public ParticleSystemImported(string systemDefinition)
        {
            _systemDefinition = systemDefinition;
        }

        #endregion
    }
}

#region Using Statements

using System;
using System.IO;

using Microsoft.Xna.Framework.Content.Pipeline;

#endregion

namespace ParticleSystemLib
{
    /// <summary>
    /// Imports a file that contains a particle system into the content pipeline.
    /// </summary>
    /// <remarks>
    /// A particle system is defined by the .pfx extension.
    /// </remarks>
    [ContentImporter(".pfx", DisplayName = "Custom - Particle System Importer", DefaultProcessor = "ParticleSystemProcessor")]
    public class ParticleSystemImporter : ContentImporter<ParticleSystemImported>
    {
        #region Methods

        /// <summary>
        /// Imports the particle system.
        /// </summary>
        /// <param name="filename">File to load.</param>
        /// <param name="context">Importing context.</param>
        /// <returns>The imported particle system.</returns>
        public override ParticleSystemImported Import(string filename, ContentImporterContext context)
        {
            using (StreamReader reader = new StreamReader(File.Open(filename, FileMode.Open, FileAccess.Read)))
            {
                return new ParticleSystemImported(reader.ReadToEnd());
            }
        }

        #endregion
    }
}

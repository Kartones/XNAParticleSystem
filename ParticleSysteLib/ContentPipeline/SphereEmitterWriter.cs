#region Using Statements

using System;

using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

#endregion

namespace ParticleSystemLib
{
    /// <summary>
    /// Class that writes a sphere emitter system into a content binary file.
    /// </summary>
    [ContentTypeWriter]
    public class SphereEmitterWriter : ContentTypeWriter<SphereEmitter>
    {
        #region Methods

        /// <summary>
        /// Writes a sphere emitter.
        /// </summary>
        /// <param name="output">Output stream.</param>
        /// <param name="value">Sphere emitter to write.</param>
        protected override void Write(ContentWriter output, SphereEmitter value)
        {
            output.Write(value.Radius);
            output.Write(value.IsConstant);
        }

        /// <summary>
        /// Gets the type the writer is able to write.
        /// </summary>
        /// <param name="targetPlatform">Target platform</param>
        /// <returns>Fully qualified type name for the content.</returns>
        public override string GetRuntimeType(Microsoft.Xna.Framework.TargetPlatform targetPlatform)
        {
            return typeof(SphereEmitter).AssemblyQualifiedName;
        }

        /// <summary>
        /// Gets the fully qualified name of the reader that reads this writer output.
        /// </summary>
        /// <param name="targetPlatform">Target platformm.</param>
        /// <returns>Fully qualified type name for the reader.</returns>
        public override string GetRuntimeReader(Microsoft.Xna.Framework.TargetPlatform targetPlatform)
        {
            return "ParticleSystemLib.SphereEmitterReader, ParticleSystemLib, Version=1.0.0.0, Culture=neutral";
        }

        #endregion
    }
}



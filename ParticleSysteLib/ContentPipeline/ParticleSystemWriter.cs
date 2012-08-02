#region Using Statements

using System;

using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

#endregion

namespace ParticleSystemLib
{
    /// <summary>
    /// Class that writes a particle system into a content binary file.
    /// </summary>
    [ContentTypeWriter]
    public class ParticleSystemWriter : ContentTypeWriter<ParticleSystemProcessed>
    {
        #region Methods

        /// <summary>
        /// Writes a particle system.
        /// </summary>
        /// <param name="output">Output stream.</param>
        /// <param name="value">Particle system to write.</param>
        protected override void Write(ContentWriter output, ParticleSystemProcessed value)
        {
            output.Write(value.Loop);
            output.Write(value.TotalNumberParticles);
            output.Write(value.BirthRate);
            output.Write(value.Position);
            output.WriteObject<Emitter>(value.Emitter);
            
            output.Write(value.DragForce);
            output.Write(value.Gravity);
            output.Write(value.ParticleMinimumAge);
            output.Write(value.ParticleMaximumAge);
            output.Write(value.ParticleInitialSize);
            output.Write(value.ParticleFinalSize);

            //Color
            output.Write(value.ParticleInitialColor.R);
            output.Write(value.ParticleInitialColor.G);
            output.Write(value.ParticleInitialColor.B);
            output.Write(value.ParticleInitialColor.A);

            //Color
            output.Write(value.ParticleFinalColor.R);
            output.Write(value.ParticleFinalColor.G);
            output.Write(value.ParticleFinalColor.B);
            output.Write(value.ParticleFinalColor.A);

            output.Write(value.ParticleMinimumSpeed);
            output.Write(value.ParticleMaximumSpeed);
            output.Write(value.ParticleMinimumAcceleration);
            output.Write(value.ParticleMaximumAcceleration);
            output.Write(value.ParticleMinimumMass);
            output.Write(value.ParticleMaximumMass);
        }

        /// <summary>
        /// Gets the type the writer is able to write.
        /// </summary>
        /// <param name="targetPlatform">Target platform</param>
        /// <returns>Fully qualified type name for the content.</returns>
        public override string GetRuntimeType(Microsoft.Xna.Framework.TargetPlatform targetPlatform)
        {
            return typeof(ParticleSystem).AssemblyQualifiedName;
        }

        /// <summary>
        /// Gets the fully qualified name of the reader that reads this writer output.
        /// </summary>
        /// <param name="targetPlatform">Target platformm.</param>
        /// <returns>Fully qualified type name for the reader.</returns>
        public override string GetRuntimeReader(Microsoft.Xna.Framework.TargetPlatform targetPlatform)
        {
            return "ParticleSystemLib.ParticleSystemReader, ParticleSystemLib, Version=1.0.0.0, Culture=neutral";
        }

        #endregion
    }
}

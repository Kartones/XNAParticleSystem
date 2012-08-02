#region Using Statements

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace ParticleSystemLib
{
    /// <summary>
    /// Class that reads a particle system from a content binary file.
    /// </summary>
    public class ParticleSystemReader : ContentTypeReader<ParticleSystem>
    {
        #region Methods

        /// <summary>
        /// Reads a particle system.
        /// </summary>
        /// <param name="input">Input stream to read.</param>
        /// <param name="existingInstance">Particle system to overwrite if you want with new data.</param>
        /// <returns>New or modified particle system.</returns>
        protected override ParticleSystem Read(ContentReader input, ParticleSystem existingInstance)
        {

            ParticleSystem system = new ParticleSystem();

            system.Loop = input.ReadBoolean();
            system.TotalNumberParticles = input.ReadInt32();
            system.BirthRate = input.ReadInt32();
            system.Position = input.ReadVector3();
            system.Emitter = input.ReadObject<Emitter>();

            system.DragForce = input.ReadSingle();
            system.Gravity = input.ReadVector3();

            system.ParticleMinimumAge = input.ReadSingle();
            system.ParticleMaximumAge = input.ReadSingle();
            system.ParticleInitialSize = input.ReadSingle();
            system.ParticleFinalSize = input.ReadSingle();
            
            system.ParticleInitialColor = new Color(input.ReadByte(), input.ReadByte(), input.ReadByte(), input.ReadByte());
            system.ParticleFinalColor = new Color(input.ReadByte(), input.ReadByte(), input.ReadByte(), input.ReadByte());

            system.ParticleMinimumSpeed = input.ReadSingle();
            system.ParticleMaximumSpeed = input.ReadSingle();
            system.ParticleMinimumAcceleration = input.ReadSingle();
            system.ParticleMaximumAcceleration = input.ReadSingle();
            system.ParticleMinimumMass = input.ReadSingle();
            system.ParticleMaximumMass = input.ReadSingle();

            return system;
        }

        #endregion
    }
}

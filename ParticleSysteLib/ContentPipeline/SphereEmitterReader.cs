#region Using Statements

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace ParticleSystemLib
{
    /// <summary>
    /// Class that reads a sphere emitter from a content binary file.
    /// </summary>
    public class SphereEmitterReader : ContentTypeReader<SphereEmitter>
    {

        #region Methods

        /// <summary>
        /// Reads a sphere emitter.
        /// </summary>
        /// <param name="input">Input stream to read.</param>
        /// <param name="existingInstance">Sphere emitter to overwrite if you want with new data.</param>
        /// <returns>New or modified sphere emitter.</returns>
        protected override SphereEmitter Read(ContentReader input, SphereEmitter existingInstance)
        {

            SphereEmitter emitter = new SphereEmitter();

            emitter.Radius = input.ReadSingle();
            emitter.IsConstant = input.ReadBoolean();

            return emitter;
        }

        #endregion
    }
}



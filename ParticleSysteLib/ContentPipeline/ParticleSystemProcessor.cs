#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;

#endregion

namespace ParticleSystemLib
{
    /// <summary>
    /// Processes an imoprted particle system.
    /// </summary>
    [ContentProcessor(DisplayName = "Custom - Particle System Processor")]
    public class ParticleSystemProcessor : ContentProcessor<ParticleSystemImported, ParticleSystemProcessed>
    {
        #region Methods

        /// <summary>
        /// Processes an imported particle system.
        /// </summary>
        /// <param name="input">Imported particle system to process.</param>
        /// <param name="context">Processing context.</param>
        /// <returns>The processed particle system.</returns>
        public override ParticleSystemProcessed Process(ParticleSystemImported input, ContentProcessorContext context)
        {
            string[] fields;
            string[] values;

            ParticleSystemProcessed processedContent = new ParticleSystemProcessed();

            fields = input.SystemDefinition.Split(new string[1] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < fields.Length; i++)
            {
                values = fields[i].Split(new string[1] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                switch (values[0].ToLower())
                {
                    case "loop":
                        processedContent.Loop = bool.Parse(values[1]);
                        break;

                    case "totalnumberparticles":
                        processedContent.TotalNumberParticles = int.Parse(values[1]);
                        break;

                    case "birthrate":
                        processedContent.BirthRate = int.Parse(values[1]);
                        break;

                    case "position":
                        processedContent.Position = ParseVector(values[1]);
                        break;

                    case "emitter":
                        processedContent.Emitter = ParseEmitter(values[1]);
                        break;

                    case "dragforce":
                        processedContent.DragForce = float.Parse(values[1]);
                        break;

                    case "gravity":
                        processedContent.Gravity = ParseVector(values[1]);
                        break;

                    case "particleminimumage":
                        processedContent.ParticleMinimumAge = float.Parse(values[1]);
                        break;

                    case "particlemaximumage":
                        processedContent.ParticleMaximumAge = float.Parse(values[1]);
                        break;

                    case "particleinitialsize":
                        processedContent.ParticleInitialSize = float.Parse(values[1]);
                        break;

                    case "particlefinalsize":
                        processedContent.ParticleFinalSize = float.Parse(values[1]);
                        break;

                    case "particleinitialcolor":
                        processedContent.ParticleInitialColor = ParseColor(values[1]);
                        break;

                    case "particlefinalcolor":
                        processedContent.ParticleFinalColor = ParseColor(values[1]);
                        break;

                    case "particleminimumspeed":
                        processedContent.ParticleMinimumSpeed = float.Parse(values[1]);
                        break;

                    case "particlemaximumspeed":
                        processedContent.ParticleMaximumSpeed = float.Parse(values[1]);
                        break;

                    case "particleminimumacceleration":
                        processedContent.ParticleMinimumAcceleration = float.Parse(values[1]);
                        break;

                    case "particlemaximumacceleration":
                        processedContent.ParticleMaximumAcceleration = float.Parse(values[1]);
                        break;

                    case "particleminimummass":
                        processedContent.ParticleMinimumMass = float.Parse(values[1]);
                        break;

                    case "particlemaximummass":
                        processedContent.ParticleMaximumMass = float.Parse(values[1]);
                        break;

                    default:
                        break;
                }
            }

            return processedContent;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Parses a Vector3.
        /// </summary>
        /// <param name="vectorData">String with the Vector3 data.</param>
        /// <returns>The Vector3.</returns>
        private Vector3 ParseVector(string vectorData)
        {
            string[] values;

            values = vectorData.Split(new string[1] {";"}, StringSplitOptions.RemoveEmptyEntries);
            return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
        }

        /// <summary>
        /// Parses a Color.
        /// </summary>
        /// <param name="vectorData">String with the Color data.</param>
        /// <returns>The Color.</returns>
        private Color ParseColor(string colorData)
        {
            string[] values;

            values = colorData.Split(new string[1] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            return new Color(byte.Parse(values[0]), byte.Parse(values[1]), byte.Parse(values[2]), byte.Parse(values[3]));
        }

        /// <summary>
        /// Parses an Emmiter.
        /// </summary>
        /// <param name="vectorData">String with the Emmiter data.</param>
        /// <returns>The Emmiter.</returns>
        private Emitter ParseEmitter(string emitterData)
        {
            string[] values;
            Emitter emitter;

            values = emitterData.Split(new string[1] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            switch(values[0].ToLower())
            {
                case "sphere":
                    emitter = new SphereEmitter(float.Parse(values[1]), bool.Parse(values[2]));
                    break;

                default:
                    return null;
            }

            return emitter;
        }

        #endregion
    }
}

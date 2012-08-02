using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using ParticleSystemLib;
using CameraManagerLib;


namespace ExampleParticleEngine
{
    /// <summary>
    /// Example game using particle  engine.
    /// </summary>
    public class ParticleEngineGame : Microsoft.Xna.Framework.Game
    {
        #region Fields

        private GraphicsDeviceManager graphics;
        private ContentManager content;
        private GraphicsDevice graphicsDevice;
        private Effect effect;

        private Texture2D particleTexture;
        private ParticleSystem particleSystem;

        private float effectAmbientLightValue = 1f;
        private Vector3 effectLightDirection = new Vector3(0, 0, 1);

        #endregion

        #region Constructors

        /// <summary>
        /// Class Constructor
        /// </summary>
        public ParticleEngineGame()
        {
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services);

            CameraManager cameraManager = new CameraManager(this);
            Components.Add(cameraManager);

            InputManager inputManager = new InputManager(this);
            Components.Add(inputManager);

            ParticleSystemManager particleManager = new ParticleSystemManager(this);
            Components.Add(particleManager);
        }

        #endregion

        #region Content loading and unloading

        /// <summary>
        /// Load your graphics content. If loadAllContent is true, you should
        /// load content from both ResourceManagementMode pools. Otherwise, just
        /// load ResourceManagementMode.Manual content.
        /// </summary>
        /// <param name="loadAllContent">Which type of content to load.</param>
        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            // Load ResourceManagementMode.Automatic content
            if (loadAllContent)
            {
                SetupGraphicsDevice();
                SetupShaderEffects();
            }
            
            // And always load ResourceManagementMode.Manual content
            particleTexture = content.Load<Texture2D>("content/particle");
            particleSystem = content.Load<ParticleSystem>("content/testSystem");
        }

        /// <summary>
        /// Unload your graphics content. If unloadAllContent is true, you should
        /// unload content from both ResourceManagementMode pools. Otherwise, just
        /// unload ResourceManagementMode.Manual content. Manual content will get
        /// Disposed by the GraphicsDevice during a Reset.
        /// </summary>
        /// <param name="unloadAllContent">Which type of content to unload.</param>
        protected override void UnloadGraphicsContent(bool unloadAllContent)
        {
            // Unload ResourceManagementMode.Automatic content
            if (unloadAllContent)
            {
                content.Unload();
            }
            // And always unload ResourceManagementMode.Manual content
        }

        #endregion

        #region Game Loop & states

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content. Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IInputManagerService inputManager = 
                (IInputManagerService)Services.GetService(typeof(IInputManagerService));
            {
                inputManager.OnKeyDown += new InputManagerHandler(HandleInputs);
            }

            base.Initialize();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            DrawParticles();

            base.Draw(gameTime);
        }

        #endregion

        #region Input Handling

        /// <summary>
        /// Handles 
        /// </summary>
        /// <param name="sender">who sends the input</param>
        /// <param name="keys">List of pressed keys</param>
        protected void HandleInputs(InputManager sender, List<Keys> keys)
        {
            float speed = 0.25f;

            ICameraManagerService cameraManager =
                    (ICameraManagerService)Services.GetService(typeof(ICameraManagerService));

            if (keys.Contains(Keys.Escape))
            {
                Exit();
            }

            if (keys.Contains(Keys.LeftShift))
                speed *= 2;

            if (keys.Contains(Keys.W))
            {
                cameraManager.Camera.Translate(new Vector3(0, 0, -1) * speed);
            }
            if (keys.Contains(Keys.S))
            {
                cameraManager.Camera.Translate(new Vector3(0, 0, 1) * speed);
            }
            if (keys.Contains(Keys.A))
            {
                cameraManager.Camera.Translate(new Vector3(-1, 0, 0) * speed);
            }
            if (keys.Contains(Keys.D))
            {
                cameraManager.Camera.Translate(new Vector3(1, 0, 0) * speed);
            }

            // Create a Sample particle system
            if (keys.Contains(Keys.Space))
            {
                IParticleSystemManagerService particleManager = 
                    (IParticleSystemManagerService)Services.GetService(typeof(IParticleSystemManagerService));
                particleManager.AddParticleSystem(particleSystem.Clone());
            }
        }

        #endregion

        #region Graphics Handling Methods

        /// <summary>
        /// Setup the Graphics device and game window
        /// </summary>
        private void SetupGraphicsDevice()
        {
            graphicsDevice = graphics.GraphicsDevice;

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            Window.Title = "XNA Particle System demo";
        }

        // TODO: This should be moved to ParticleSystemManager class
        /// <summary>
        /// Loads custom shader effects.
        /// <remarks>Loads an effect to allow using PointSprites correctly</remarks>
        /// </summary>
        private void SetupShaderEffects()
        {
            CompiledEffect compiledEffect = Effect.CompileEffectFromFile(
                "content/pointsprites.fx",               // Effects file
                null,                       // Preprocessor defines
                null,                       // Include handler
                CompilerOptions.None,       // Compiler options
                TargetPlatform.Windows);    // Target platform

            effect = new Effect(
                graphics.GraphicsDevice,        // Graphics device
                compiledEffect.GetEffectCode(), // Effect code
                CompilerOptions.None,           // Compiler options
                null);                          // Effect pool

            ICameraManagerService cameraManager =
                    (ICameraManagerService)Services.GetService(typeof(ICameraManagerService));

            // Effect parameter values
            effect.Parameters["xView"].SetValue(cameraManager.Camera.View);
            effect.Parameters["xProjection"].SetValue(cameraManager.Camera.Projection);
            effect.Parameters["xWorld"].SetValue(Matrix.Identity);
            effect.Parameters["xEnableLighting"].SetValue(true);
            effect.Parameters["xLightDirection"].SetValue(effectLightDirection);
            effect.Parameters["xAmbient"].SetValue(effectAmbientLightValue);
        }

        // TODO: This should be moved to ParticleSystemManager class
        /// <summary>
        /// Draws all sprites in the graphics device
        /// </summary>
        private void DrawParticles()
        {
            IParticleSystemManagerService particleManager =
                (IParticleSystemManagerService)Services.GetService(typeof(IParticleSystemManagerService));

            ICameraManagerService cameraManager =
                (ICameraManagerService)Services.GetService(typeof(ICameraManagerService));

            // Prepare effect parameters
            effect.CurrentTechnique = effect.Techniques["PointSprites"];
            effect.Parameters["xWorld"].SetValue(Matrix.Identity);
            effect.Parameters["xView"].SetValue(cameraManager.Camera.View);
            effect.Parameters["xProjection"].SetValue(cameraManager.Camera.Projection);
            this.effect.Parameters["xTexture"].SetValue(particleTexture);

            // TODO: Test that after adding "color particles" alphablending works correctly
            // For alphablending
            graphicsDevice.RenderState.AlphaBlendEnable = true;
            graphicsDevice.RenderState.SourceBlend = Blend.One;
            graphicsDevice.RenderState.DestinationBlend = Blend.One;

            for (int systCnt = 0; systCnt < particleManager.ParticleSystems.Count; systCnt++)
            {
                PointSpriteVertexFormat[] particleCoords =
                    new PointSpriteVertexFormat[particleManager.ParticleSystems[systCnt].Particles.Count];

                // We'll paint each particle system's particles at once
                for (int partCnt = 0; 
                    partCnt < particleManager.ParticleSystems[systCnt].Particles.Count;
                    partCnt++)
                {
                    particleCoords[partCnt] = new PointSpriteVertexFormat(
                            particleManager.ParticleSystems[systCnt].Particles[partCnt].Position,
                            particleManager.ParticleSystems[systCnt].Particles[partCnt].Size,
                            particleManager.ParticleSystems[systCnt].Particles[partCnt].Color);
                }

                if (particleManager.ParticleSystems[systCnt].Particles.Count > 0)
                {
                    effect.Begin();
                    foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Begin();
                        graphicsDevice.VertexDeclaration =
                            new VertexDeclaration(graphicsDevice, PointSpriteVertexFormat.Elements);
                        graphicsDevice.DrawUserPrimitives(PrimitiveType.PointList, particleCoords, 0,
                            particleCoords.Length);
                        pass.End();
                    }
                    effect.End();
                }
            }

            graphicsDevice.RenderState.AlphaBlendEnable = false;
        }

        #endregion
    }
}
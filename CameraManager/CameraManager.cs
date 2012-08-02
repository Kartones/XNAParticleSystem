using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Camera & Input Manager by Fegelein (http://www.fegelein.com)
/// </summary>
namespace CameraManagerLib
{
    public interface ICameraManagerService
    {
        Camera Camera { get; }
    }

    /// <summary>
    /// Camera & Input Manager by Fegelein (http://www.fegelein.com)
    /// </summary>
    public class CameraManager : Microsoft.Xna.Framework.GameComponent, ICameraManagerService
    {
        protected Vector2 offMousePos;
        protected Vector2 preMousePos;
        protected Camera camera;

        public CameraManager(Game game) : base(game)
        {
            this.camera = new Camera();
            {
                this.camera.Rotation = new Quaternion(0, 0, 0, 0);
                this.camera.Position = new Vector3(0.0f, 0.0f, 2.0f);
            }

            this.Game.Services.AddService(typeof(ICameraManagerService), this);
        }

        public Camera Camera
        {
            get
            {
                return this.camera;
            }
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            IGraphicsDeviceService graphicsDeviceService = (IGraphicsDeviceService)this.Game.Services.GetService(typeof(IGraphicsDeviceService));
            {
                this.camera.SetAspectRatio((float)graphicsDeviceService.GraphicsDevice.Viewport.Width / (float)graphicsDeviceService.GraphicsDevice.Viewport.Height);
            }

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Retrieve the mousestate
            MouseState ms = Mouse.GetState();

            // Check if pressed the left mouse button
            if (ms.LeftButton == ButtonState.Pressed)
            {
                // Rotate camera
                this.camera.Rotate(offMousePos.X * 0.005f, offMousePos.Y * 0.005f, 0);
            }

            // Save the offset between mousecoordinates, and the current mouse pos
            offMousePos = preMousePos - new Vector2(ms.X, ms.Y);
            preMousePos = new Vector2(ms.X, ms.Y);

            base.Update(gameTime);
        }
    }
}



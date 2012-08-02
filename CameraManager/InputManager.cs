using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// Camera & Input Manager by Fegelein (http://www.fegelein.com)
/// </summary>
namespace CameraManagerLib
{
    public interface IInputManagerService
    {
        event InputManagerHandler OnKeyPress;
        event InputManagerHandler OnKeyDown;
        event InputManagerHandler OnKeyRelease;
    }

    /// <summary>
    /// Delegate for this GameComponent
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="keys"></param>
    public delegate void InputManagerHandler(InputManager sender, List<Keys> keys);

    /// <summary>
    /// Camera & Input Manager by Fegelein (http://www.fegelein.com)
    /// </summary>
    public partial class InputManager : Microsoft.Xna.Framework.GameComponent, IInputManagerService
    {
        /// <summary>
        /// Events
        /// </summary>
        public event InputManagerHandler OnKeyPress;
        public event InputManagerHandler OnKeyDown;
        public event InputManagerHandler OnKeyRelease;

        /// <summary>
        /// Private members 
        /// </summary>
        protected List<Keys> PreviousKeyMap;
        protected List<Keys> CurrentKeyMap;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">The main game component</param>
        public InputManager(Game game) : base(game)
        {
            // Init the lists
            this.PreviousKeyMap = new List<Keys>();
            this.CurrentKeyMap = new List<Keys>();

            // Add the service
            this.Game.Services.AddService(typeof(IInputManagerService), this);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Retrieve the current keyboard key state
            KeyboardState ks = Keyboard.GetState();

            // Create new lists to store currently pressed and released keys
            List<Keys> pressedKeys = new List<Keys>();
            List<Keys> releasedKeys = new List<Keys>();

            // Save the current key map in the previous map, so we can check it the next update
            this.PreviousKeyMap = this.CurrentKeyMap;
            this.CurrentKeyMap = new List<Keys>();

            // Add every pressed key to the pressed key map
            foreach (Keys key in ks.GetPressedKeys())
                this.CurrentKeyMap.Add(key);

            // Add every key that wasn't previously pressed to the pressed key map
            foreach (Keys key in this.CurrentKeyMap)
            {
                if (!this.PreviousKeyMap.Contains(key))
                    pressedKeys.Add(key);
            }

            // Add every key that was previously pressed, but NOT currently to the released key map
            foreach (Keys key in this.PreviousKeyMap)
            {
                if (!this.CurrentKeyMap.Contains(key))
                    releasedKeys.Add(key);
            }

            // Invoke the OnKeyDown event
            if (this.CurrentKeyMap.Count > 0 && this.OnKeyDown != null)
                this.OnKeyDown(this, this.CurrentKeyMap);

            // Invoke the OnKeyPress event
            if (pressedKeys.Count > 0 && this.OnKeyPress != null)
                this.OnKeyPress(this, pressedKeys);

            // Invoke the OnKeyRelease event
            if (releasedKeys.Count > 0 && this.OnKeyRelease != null)
                this.OnKeyRelease(this, releasedKeys);

            base.Update(gameTime);
        }
    }
}



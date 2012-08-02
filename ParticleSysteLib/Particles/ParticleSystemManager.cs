using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace ParticleSystemLib
{
    public class ParticleSystemManager : Microsoft.Xna.Framework.GameComponent, IParticleSystemManagerService
    {
        #region Fields

        private List<ParticleSystem> _registeredSystems = new List<ParticleSystem>();
        private static int _lastAssignedID = 0;

        #endregion

        #region Properties

        public List<ParticleSystem> ParticleSystems
        {
            get { return _registeredSystems; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Adds the particle system manager as a Game Service
        /// </summary>
        /// <param name="game"></param>
        public ParticleSystemManager(Game game) : base(game)
        {
            Game.Services.AddService(typeof(IParticleSystemManagerService), this);
        }

        #endregion

        #region Methods

        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Updates all particle systems, removing dead ones
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            int i = 0;
            while (i < _registeredSystems.Count)
            {
                if (_registeredSystems[i].IsAlive)
                {
                    _registeredSystems[i].Update(gameTime);
                    i++;
                }
                else
                    _registeredSystems.RemoveAt(i);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Creates a new particle system
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <returns></returns>
        public int AddParticleSystem(ParticleSystem particleSystem)
        {
            if (particleSystem == null)
                return -1;

            particleSystem.ID = _lastAssignedID++;
            _registeredSystems.Add(particleSystem);

            return particleSystem.ID;
        }

        /// <summary>
        /// Deletes an existing particle system
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveParticleSystem(int id)
        {
            if (id < 0 || id > _lastAssignedID)
                return false;

            for(int i = 0; i < _registeredSystems.Count; i++)
                if (_registeredSystems[i].ID == id)
                {
                    _registeredSystems.RemoveAt(i);
                    return true;
                }

            return false;
        }

        #endregion
    }
}

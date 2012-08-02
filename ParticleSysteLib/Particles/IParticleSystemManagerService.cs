using System;
using System.Collections.Generic;

namespace ParticleSystemLib
{
    /// <summary>
    /// This interface defines the services exposed by a manager of particle systems.
    /// </summary>
    public interface IParticleSystemManagerService
    {
        #region Properties

        /// <summary>
        /// Gets the particle systems registered in the manager
        /// </summary>
        List<ParticleSystem> ParticleSystems 
        { 
            get; 
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a new particle system to the manager.
        /// </summary>
        /// <param name="particleSystem">Particle system to add.</param>
        /// <returns>The ID of the assigned system.</returns>
        int AddParticleSystem(ParticleSystem particleSystem);

        /// <summary>
        /// Removes a particle system from the manager.
        /// </summary>
        /// <param name="id">ID of the system to remove.</param>
        /// <returns>True if the system was removed, false if it didn't exist.</returns>
        bool RemoveParticleSystem(int id);

        #endregion
    }
}



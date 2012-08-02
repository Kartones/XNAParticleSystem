#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace ParticleSystemLib
{
    /// <summary>
    /// Data of a particle system processed by the content pipeline.
    /// </summary>
    public class ParticleSystemProcessed
    {
        #region Fields

        private bool _loop; //If the particle system is infinite or not
        private int _totalNumberParticles; //Particles emitted in the total lifetime of the system
        private int _birthRate; //Particles emitted per second
        private Vector3 _position; //Position of the system
        private Emitter _emitter; //Emmiter of the system
        private float _dragForce = 1; //Drag force for the particles off the system
        private Vector3 _gravity = new Vector3(0, 0, 0); //Gravity for the particles of the system

        //PARTICLES DATA
        private float _particleMinimumAge; //Minimum age of a particle
        private float _particleMaximumAge; //Maximum age of a particle

        private float _particleInitialSize; //Initial size of a particle
        private float _particleFinalSize; //Final size of a particle

        private Color _particleInitialColor; //Initial color of the particle
        private Color _particleFinalColor; //Final color of the particle

        private float _particleMinimumSpeed; //Minimum speed of a particle
        private float _particleMaximumSpeed; //Maximum speed of the particle

        private float _particleMinimumAcceleration; //Minimum acceleration of a particle
        private float _particleMaximumAcceleration; //Maximum acceleration of a particle

        private float _particleMinimumMass; //Minimum mass of a particle
        private float _particleMaximumMass; //Maximum mass of a particle

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the particle system loops or not.
        /// </summary>
        public bool Loop
        {
            get { return _loop; }
            set { _loop = value; }
        }

        /// <summary>
        /// Gets or sets the total number of particles of the system.
        /// </summary>
        public int TotalNumberParticles
        {
            get { return _totalNumberParticles; }
            set { _totalNumberParticles = value; }
        }

        /// <summary>
        /// Gets or sets the birthrate of the particles of the system.
        /// </summary>
        public int BirthRate
        {
            get { return _birthRate; }
            set { _birthRate = value; }
        }

        /// <summary>
        /// Gets or sets the position of the system.
        /// </summary>
        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// Gets or sets the emitter of the system.
        /// </summary>
        public Emitter Emitter
        {
            get { return _emitter; }
            set { _emitter = value; }
        }

        /// <summary>
        /// Gets or sets the drag force that affects the system.
        /// </summary>
        public float DragForce
        {
            get { return _dragForce; }
            set { _dragForce = value; }
        }

        /// <summary>
        /// Gets or sets the gravity that affects the system.
        /// </summary>
        public Vector3 Gravity
        {
            get { return _gravity; }
            set { _gravity = value; }
        }

        /// <summary>
        /// Gets or sets the minimum age of the particles.
        /// </summary>
        public float ParticleMinimumAge
        {
            get { return _particleMinimumAge; }
            set { _particleMinimumAge = value; }
        }

        /// <summary>
        /// Gets or sets the maximum age of the particles.
        /// </summary>
        public float ParticleMaximumAge
        {
            get { return _particleMaximumAge; }
            set { _particleMaximumAge = value; }
        }

        /// <summary>
        /// Gets or sets the particles initial size.
        /// </summary>
        public float ParticleInitialSize
        {
            get { return _particleInitialSize; }
            set { _particleInitialSize = value; }
        }

        /// <summary>
        /// Gets or sets the particles final size.
        /// </summary>
        public float ParticleFinalSize
        {
            get { return _particleFinalSize; }
            set { _particleFinalSize = value; }
        }

        /// <summary>
        /// Gets or sets the particles initial color.
        /// </summary>
        public Color ParticleInitialColor
        {
            get { return _particleInitialColor; }
            set { _particleInitialColor = value; }
        }

        /// <summary>
        /// Gets or sets the particles final color.
        /// </summary>
        public Color ParticleFinalColor
        {
            get { return _particleFinalColor; }
            set { _particleFinalColor = value; }
        }

        /// <summary>
        /// Gets or sets the minimum speed of the particles.
        /// </summary>
        public float ParticleMinimumSpeed
        {
            get { return _particleMinimumSpeed; }
            set { _particleMinimumSpeed = value; }
        }

        /// <summary>
        /// Gets or sets the maximum speed of the particles.
        /// </summary>
        public float ParticleMaximumSpeed
        {
            get { return _particleMaximumSpeed; }
            set { _particleMaximumSpeed = value; }
        }

        /// <summary>
        /// Gets or sets the minimum acceleration of the particles.
        /// </summary>
        public float ParticleMinimumAcceleration
        {
            get { return _particleMinimumAcceleration; }
            set { _particleMinimumAcceleration = value; }
        }

        /// <summary>
        /// Gets or sets the maximum acceleration of the particles.
        /// </summary>
        public float ParticleMaximumAcceleration
        {
            get { return _particleMaximumAcceleration; }
            set { _particleMaximumAcceleration = value; }
        }

        /// <summary>
        /// Gets or sets the minimum mass of the particles.
        /// </summary>
        public float ParticleMinimumMass
        {
            get { return _particleMinimumMass; }
            set { _particleMinimumMass = value; }
        }

        /// <summary>
        /// Gets or sets the maximum mass of the particles.
        /// </summary>
        public float ParticleMaximumMass
        {
            get { return _particleMaximumMass; }
            set { _particleMaximumMass = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public ParticleSystemProcessed() { }

        #endregion
    }
}

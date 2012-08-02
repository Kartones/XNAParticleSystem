#region Using Statements

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace ParticleSystemLib
{
    /// <summary>
    /// Represents a group of particles with a specific behavior
    /// </summary>
    public class ParticleSystem
    {
        #region Fields

        //SYSTEM SETTINGS 
        private int _id = -1;
        private bool _isAlive = true;

        private bool _loop = false; //If the particle system is infinite or not
        private int _totalNumberParticles = 0; //Particles emitted in the total lifetime of the system
        private int _createdParticles = 0; //Number of emitted particles so far
        private int _birthRate = 0; //Particles emitted per second
        private Vector3 _position = new Vector3(0, 0, 0); //Position of the system
        private Emitter _emitter = null; //Emmiter of the system
        private float _dragForce = 1; //Drag force for the particles off the system
        private Vector3 _gravity = new Vector3(0, 0, 0); //Gravity for the particles of the system

        //PARTICLES DATA
        private float _particleMinimumAge = 0; //Minimum age of a particle
        private float _particleMaximumAge = 0; //Maximum age of a particle

        private float _particleInitialSize = 0; //Initial size of a particle
        private float _particleFinalSize = 0; //Final size of a particle

        private Color _particleInitialColor = new Color(0, 0, 0, 0); //Initial color of the particle
        private Color _particleFinalColor = new Color(0, 0, 0, 0); //Final color of the particle

        private float _particleMinimumSpeed = 0; //Minimum speed of a particle
        private float _particleMaximumSpeed = 0; //Maximum speed of the particle

        private float _particleMinimumAcceleration = 0; //Minimum acceleration of a particle
        private float _particleMaximumAcceleration = 0; //Maximum acceleration of a particle

        private float _particleMinimumMass = 0; //Minimum mass of a particle
        private float _particleMaximumMass = 0; //Maximum mass of a particle

        private List<Particle> _particles = new List<Particle>();

        private Random _random = new Random();

        #endregion

        #region Properties

        public int ID
        {
            get { return _id; }
            internal set { _id = value; }
        }

        public bool IsAlive
        {
            get { return _isAlive; }
            set { _isAlive = true; }
        }

        public bool Loop
        {
            get { return _loop; }
            set { _loop = value; }
        }

        public int TotalNumberParticles
        {
            get { return _totalNumberParticles; }
            set { _totalNumberParticles = value; }
        }

        public int BirthRate
        {
            get { return _birthRate; }
            set { _birthRate = value; }
        }

        public Vector3 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_emitter != null)
                    _emitter.Position = value;
            }
        }

        public Emitter Emitter
        {
            get { return _emitter; }
            set
            {
                _emitter = value;
                _emitter.Position = _position;
            }
        }

        public float DragForce
        {
            get { return _dragForce; }
            set { _dragForce = value; }
        }

        public Vector3 Gravity
        {
            get { return _gravity; }
            set { _gravity = value; }
        }

        public float ParticleMinimumAge
        {
            get { return _particleMinimumAge; }
            set { _particleMinimumAge = value; }
        }

        public float ParticleMaximumAge
        {
            get { return _particleMaximumAge; }
            set { _particleMaximumAge = value; }
        }

        public float ParticleInitialSize
        {
            get { return _particleInitialSize; }
            set { _particleInitialSize = value; }
        }

        public float ParticleFinalSize
        {
            get { return _particleFinalSize; }
            set { _particleFinalSize = value; }
        }

        public Color ParticleInitialColor
        {
            get { return _particleInitialColor; }
            set { _particleInitialColor = value; }
        }

        public Color ParticleFinalColor
        {
            get { return _particleFinalColor; }
            set { _particleFinalColor = value; }
        }

        public float ParticleMinimumSpeed
        {
            get { return _particleMinimumSpeed; }
            set { _particleMinimumSpeed = value; }
        }

        public float ParticleMaximumSpeed
        {
            get { return _particleMaximumSpeed; }
            set { _particleMaximumSpeed = value; }
        }

        public float ParticleMinimumAcceleration
        {
            get { return _particleMinimumAcceleration; }
            set { _particleMinimumAcceleration = value; }
        }

        public float ParticleMaximumAcceleration
        {
            get { return _particleMaximumAcceleration; }
            set { _particleMaximumAcceleration = value; }
        }

        public float ParticleMinimumMass
        {
            get { return _particleMinimumMass; }
            set { _particleMinimumMass = value; }
        }

        public float ParticleMaximumMass
        {
            get { return _particleMaximumMass; }
            set { _particleMaximumMass = value; }
        }

        public List<Particle> Particles
        {
            get { return _particles; }
        }

        #endregion

        #region Constructors

        public ParticleSystem() { }

        #endregion

        #region Methods

        /// <summary>
        /// Updating a particle system consists in updating each particle's age, and then
        /// create and destroy particles
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //Update the particles
            int j = 0;
            while (j < _particles.Count)
            {
                if (_particles[j].Age > 0.0f)
                {
                    _particles[j].Update(gameTime);
                    j++;
                }
                else
                    _particles.RemoveAt(j);
            }

            //Update the system
            if (_loop || (!_loop && (_createdParticles < _totalNumberParticles)))
            {
                int bornParticles;

                bornParticles = (int) (gameTime.ElapsedGameTime.Milliseconds * 0.001f * _birthRate);

                for (int i = 0; i < bornParticles; i++)
                    CreateParticle();
            }

            if (_particles.Count == 0)
                _isAlive = false;
        }

        /// <summary>
        /// Clones a particle system (used when reading from the content pipeline)
        /// </summary>
        /// <returns></returns>
        public ParticleSystem Clone()
        {
            ParticleSystem newSystem = new ParticleSystem();

            newSystem.Loop = Loop;
            newSystem.TotalNumberParticles = TotalNumberParticles;
            newSystem.BirthRate = BirthRate;
            newSystem.Position = Position;
            newSystem.Emitter = Emitter;
            newSystem.DragForce = DragForce;
            newSystem.Gravity = Gravity;
            newSystem.ParticleMinimumAge = ParticleMinimumAge;
            newSystem.ParticleMaximumAge = ParticleMaximumAge;
            newSystem.ParticleInitialSize = ParticleInitialSize;
            newSystem.ParticleFinalSize = ParticleFinalSize;
            newSystem.ParticleInitialColor = ParticleInitialColor;
            newSystem.ParticleFinalColor = ParticleFinalColor;
            newSystem.ParticleMinimumSpeed = ParticleMinimumSpeed;
            newSystem.ParticleMaximumSpeed = ParticleMaximumSpeed;
            newSystem.ParticleMinimumAcceleration = ParticleMinimumAcceleration;
            newSystem.ParticleMaximumAcceleration = ParticleMaximumAcceleration;
            newSystem.ParticleMinimumMass = ParticleMinimumMass;
            newSystem.ParticleMaximumMass = ParticleMaximumMass;

            return newSystem;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Creates a new particle and sets it's properties
        /// </summary>
        private void CreateParticle()
        {
            byte r, g, b, a;
            Particle particle = new Particle();
            Vector3 position, direction;

            // Particle creation
            particle.Age = _particleMinimumAge + (float) _random.NextDouble() * (_particleMaximumAge - _particleMinimumAge);

            particle.DeltaSize = _particleFinalSize - _particleInitialSize;
            particle.Size = _particleInitialSize + (float) _random.NextDouble() * particle.DeltaSize;
            particle.DeltaSize /= particle.Age;

            r = (byte) (_particleFinalColor.R - _particleInitialColor.R);
            g = (byte) (_particleFinalColor.G - _particleInitialColor.G);
            b = (byte) (_particleFinalColor.B - _particleInitialColor.B);
            a = (byte) (_particleFinalColor.A - _particleInitialColor.A);

            particle.DeltaColor = new Color(r, g, b, a);
            
            r = (byte) (_particleInitialColor.R + (byte) (_random.NextDouble() * particle.DeltaColor.R));
            g = (byte) (_particleInitialColor.G + (byte) (_random.NextDouble() * particle.DeltaColor.G));
            b = (byte) (_particleInitialColor.B + (byte) (_random.NextDouble() * particle.DeltaColor.B));
            a = (byte) (_particleInitialColor.A + (byte) (_random.NextDouble() * particle.DeltaColor.A));

            particle.Color = new Color(r, g, b, a);
            particle.DeltaColor = new Color((byte) (particle.DeltaColor.R / particle.Age), (byte) (particle.DeltaColor.G / particle.Age), (byte) (particle.DeltaColor.B / particle.Age), (byte) (particle.DeltaColor.A / particle.Age));

            _emitter.EmittPosition(out position, out direction);

            particle.Position = position;

            particle.Velocity = direction * (_particleMinimumSpeed + (float) _random.NextDouble() * (_particleMaximumSpeed - _particleMinimumSpeed));
            particle.Acceleration = direction * (_particleMinimumAcceleration + (float) _random.NextDouble() * (_particleMaximumAcceleration - _particleMinimumAcceleration));
            
            particle.Mass = _particleMinimumMass + (float) _random.NextDouble() * (_particleMaximumMass - _particleMinimumMass);

            particle.Parent = this;

            // Add the particle to the system
            _particles.Add(particle);
            _createdParticles++;
        }

        #endregion
    }
}

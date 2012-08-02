#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace ParticleSystemLib
{
    public class Particle
    {
        #region Fields

        private float _age;

        private float _size;
        private float _deltaSize;

        private Color _color;
        private Color _deltaColor;

        private Vector3 _position;

        private Vector3 _velocity;
        private Vector3 _acceleration;

        private float _mass;

        private ParticleSystem _parent;

        #endregion

        #region Properties

        public float Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public float Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public float DeltaSize
        {
            get { return _deltaSize; }
            set { _deltaSize = value; }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public Color DeltaColor
        {
            get { return _deltaColor; }
            set { _deltaColor = value; }
        }

        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector3 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Vector3 Acceleration
        {
            get { return _acceleration; }
            set { _acceleration = value; }
        }

        public float Mass
        {
            get { return _mass; }
            set { _mass = value; }
        }

        public ParticleSystem Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Class constructor
        /// </summary>
        public Particle() 
        { 
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the particle data (size, age, acceleration, position...)
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            float time;

            time = gameTime.ElapsedGameTime.Milliseconds * 0.001f;
            _age -= time;

            _position += _velocity * time;
            _acceleration += _parent.Gravity * _mass;

            _velocity += _acceleration * time;
            _velocity *= (float) Math.Pow(_parent.DragForce, time);

            _size += _deltaSize * time;
            _color = new Color((byte) (_color.R + _deltaColor.R * time), (byte) (_color.G + _deltaColor.G * time), (byte) (_color.B + _deltaColor.B * time), (byte) (_color.A + _deltaColor.A * time));
        }

        #endregion
    }
}
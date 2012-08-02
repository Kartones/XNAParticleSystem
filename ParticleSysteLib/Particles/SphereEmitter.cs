#region Using Statements

using System;
using Microsoft.Xna.Framework;

#endregion

namespace ParticleSystemLib
{
    /// <summary>
    /// Sample particle emitter in a sphere
    /// </summary>
    public class SphereEmitter : Emitter
    {
        #region Fields

        private float _radius = 1;
        private bool _isConstant = false;

        #endregion

        #region Properties

        public float Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        public bool IsConstant
        {
            get { return _isConstant; }
            set { _isConstant = value; }
        }

        #endregion

        #region Constructors

        public SphereEmitter() { }

        public SphereEmitter(float radius, bool constant)
        {
            _radius = radius;
            _isConstant = constant;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Emitts particles in a spherical area
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        public override void EmittPosition(out Vector3 position, out Vector3 direction)
        {
            if (_isConstant)
                position = EmitDirection(new Vector3(1.0f, 1.0f, 1.0f), 180.0f) * _radius + Position;

            else
            {
                float x = (float) (Random.NextDouble() * (_radius * 2) - _radius + Position.X);
                float y = (float) (Random.NextDouble() * (_radius * 2) - _radius + Position.Y);
                float z = (float) (Random.NextDouble() * (_radius * 2) - _radius + Position.Z);

                position = new Vector3(x, y, z);
            }

            direction = new Vector3((float) (Random.NextDouble() * 2 - 1), (float) (Random.NextDouble() * 2 - 1), (float) (Random.NextDouble() * 2 - 1));
            direction.Normalize();
        }

        #endregion
    }
}



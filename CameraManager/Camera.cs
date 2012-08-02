using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

/// <summary>
/// Camera & Input Manager by Fegelein (http://www.fegelein.com)
/// </summary>
namespace CameraManagerLib
{
    /// <summary>
    /// Camera & Input Manager by Fegelein (http://www.fegelein.com)
    /// </summary>
    public class Camera
    {
        protected float FOV = MathHelper.Pi / 3;
        protected float aspectRatio = 1;
        protected float nearClip = 1.0f;
        protected float farClip = 10000.0f;

        protected Quaternion cameraRotation;
        protected Vector3 cameraPosition;

        protected float yaw;
        protected float pitch;
        protected float roll;

        public Quaternion Rotation
        {
            get
            {
                return cameraRotation;
            }
            set
            {
                cameraRotation = value;
            }
        }

        public Vector3 Position
        {
            get
            {
                return cameraPosition;
            }
            set
            {
                cameraPosition = value;
            }
        }

        public Matrix Projection
        {
            get
            {
                return Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), aspectRatio, nearClip, farClip);
            }
        }

        public Matrix View
        {
            get
            {
                return Matrix.Invert(Matrix.CreateFromQuaternion(Rotation) * Matrix.CreateTranslation(Position));
            }
        }

        public Camera()
        {
            cameraRotation = new Quaternion(0, 0, 0, 0);
            cameraPosition = Vector3.Zero;

            yaw = 0;
            pitch = 0;
            roll = 0;
        }

        public void Rotate(float xRotation, float yRotation, float zRotation)
        {
            yaw += xRotation;
            pitch += yRotation;
            roll += zRotation;

            Quaternion q1 = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), yaw);
            Quaternion q2 = Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), pitch);
            Quaternion q3 = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), roll);

            cameraRotation = q1 * q2 * q3;
        }

        public void Translate(Vector3 distance)
        {
            Vector3 diff = Vector3.Transform(distance, Matrix.CreateFromQuaternion(cameraRotation));
            cameraPosition += diff;
        }

        public void SetAspectRatio(float ratio)
        {
            aspectRatio = ratio;
        }
    }
}

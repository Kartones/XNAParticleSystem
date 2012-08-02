using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleSystemLib
{
    /// <summary>
    /// Structure to handle our PointSprites' vertex format
    /// </summary>
    public struct PointSpriteVertexFormat
    {
        #region Variables

        private Vector3 position;
        private float pointSize;
        private Color color;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Position">Position of the PointSprite</param>
        /// <param name="PointSize">Size of the PointSprite</param>
        public PointSpriteVertexFormat(Vector3 Position, float PointSize, Color Color)
        {
            this.position = Position;
            this.pointSize = PointSize;
            this.color = Color;
        }

        #endregion

        #region VertexFormat Data
        
        public static VertexElement[] Elements =
              {
                  new VertexElement(0, 0, VertexElementFormat.Vector3, VertexElementMethod.Default, 
                    VertexElementUsage.Position, 0),
                  new VertexElement(0, sizeof(float)*3, VertexElementFormat.Single, 
                    VertexElementMethod.Default, VertexElementUsage.PointSize, 0),
                  new VertexElement( 0, sizeof(float) * 4, VertexElementFormat.Color, 
                  VertexElementMethod.Default, VertexElementUsage.Color, 0 ),
              };

        public static int SizeInBytes = sizeof(float) * (3 + 1 + 4);

        #endregion
    }
}

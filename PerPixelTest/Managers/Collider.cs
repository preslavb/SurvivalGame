namespace PerPixelTest.Managers
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    class Collider
    {
        private Rectangle rect;

        public Texture2D Texture { get; set; }
        public Rectangle Rect
        {
            get
            {
                return rect;
            }
            
        }
    }
}

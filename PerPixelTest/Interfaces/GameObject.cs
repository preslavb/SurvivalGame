namespace PerPixelTest.Interfaces
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PerPixelTest.Managers;
    using System;
    public class GameObject: Collider, IRenderable
    {
        public Vector2 Position { get; set; }
        public virtual void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Draw(this.Texture, this.Position, Color.White);
        }
    }
}

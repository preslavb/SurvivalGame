namespace PerPixelTest.Interfaces
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    public interface IRenderable
    {
        void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics);
    }
}

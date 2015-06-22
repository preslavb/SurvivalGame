namespace PerPixelTest.Managers
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public static class Debuger
    {
        private static Texture2D pixel;
        private static SpriteFont font;

        public static void LoadContent(ContentManager content, GraphicsDeviceManager graphics)
        {
            pixel = new Texture2D(graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            font = content.Load<SpriteFont>("DebugFont");
        }

        public static void Print(object debugObject)
        {
        }
    }
}

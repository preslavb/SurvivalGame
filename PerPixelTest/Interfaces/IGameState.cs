namespace PerPixelTest.Interfaces
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    public interface IGameState
    {
        List<Texture2D> Textures { get; }
        bool Initialized { get; set; }

        void Initialize(GraphicsDeviceManager graphics);
        void LoadContent(ContentManager Content, GraphicsDeviceManager graphics);
        void UnloadContent();
        void Update(GraphicsDeviceManager graphics, GameTime gameTime);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDeviceManager graphics);
    }
}

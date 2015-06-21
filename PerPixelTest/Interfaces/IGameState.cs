namespace PerPixelTest.Interfaces
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    public interface IGameState : IRenderable
    {
        bool Initialized { get; set; }

        void Initialize(GraphicsDeviceManager graphics);
        void LoadContent(ContentManager Content, GraphicsDeviceManager graphics);
        void UnloadContent();
        void Update(ContentManager Content, GraphicsDeviceManager graphics, GameTime gameTime);
    }
}

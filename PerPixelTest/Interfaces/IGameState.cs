namespace PerPixelTest.Interfaces
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public interface IGameState : IRenderable
    {
        bool Initialized { get; set; }

        void Initialize(GraphicsDeviceManager graphics);

        void LoadContent(ContentManager content, GraphicsDeviceManager graphics);

        void UnloadContent();

        void Update(ContentManager content, GraphicsDeviceManager graphics, GameTime gameTime);
    }
}

namespace PerPixelTest.Managers.GameStates
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework.Graphics;
    using PerPixelTest.Interfaces;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    public class SplashScreen : IGameState
    {
        public bool Initialized
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public List<Texture2D> Textures
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Initialize(GraphicsDeviceManager graphics)
        {
            throw new NotImplementedException();
        }

        public void LoadContent(ContentManager Content, GraphicsDeviceManager graphics)
        {
            throw new NotImplementedException();
        }

        public void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public void Update(GraphicsDeviceManager graphics, GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, null);
            spriteBatch.End();
        }
    }
}

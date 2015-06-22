namespace PerPixelTest.Managers.GameStates
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using PerPixelTest.Interfaces;

    public class SplashScreen : IGameState
    {
        private Texture2D splashTexture;

        public bool Initialized { get; set; }

        public void Initialize(GraphicsDeviceManager graphics)
        {
        }

        public void LoadContent(ContentManager content, GraphicsDeviceManager graphics)
        {
            this.splashTexture = content.Load<Texture2D>("placeholder");
        }

        public void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public void Update(ContentManager content, GraphicsDeviceManager graphics, GameTime gameTime)
        {
            for (int i = 0; i < InputHandler.PressedKeys.Count; i++)
            {
                if ((InputHandler.PressedKeys[i] == Keys.P && InputHandler.PressedKeysStates[i] == InputHandler.KeyState.Clicked) && GameStateHandler.CurrentGameState is SplashScreen)
                {
                    GameStateHandler.CurrentGameState = new Gameplay();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, null);
            spriteBatch.End();
        }
    }
}

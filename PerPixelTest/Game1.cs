namespace PerPixelTest
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using PerPixelTest.Camera;
    using PerPixelTest.Characters;
    using PerPixelTest.Sprites;
    using PerPixelTest.Managers;
    using PerPixelTest.Interfaces;
    using PerPixelTest.Managers.GameStates;

    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public SpriteFont font;

        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 1920;
            this.graphics.PreferredBackBufferHeight = 1080;
        }

        protected override void Initialize()
        {
            GameStateHandler.currentGameState = new Gameplay();

            GameStateHandler.currentGameState.Initialize(graphics);

            InputHandler.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            GameStateHandler.currentGameState.LoadContent(Content, this.graphics);

            InputHandler.LoadContent();

            font = Content.Load<SpriteFont>("DebugFont");
        }

        protected override void UnloadContent()
        {
            GameStateHandler.currentGameState.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            InputHandler.Update();

            GameStateHandler.currentGameState.Update(this.graphics, gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GameStateHandler.currentGameState.Draw(gameTime, this.spriteBatch, this.graphics);

            

            base.Draw(gameTime);
        }
    }
}

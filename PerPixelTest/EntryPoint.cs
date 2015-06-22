namespace PerPixelTest
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PerPixelTest.Managers;
    using PerPixelTest.Managers.GameStates;

    public class EntryPoint : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        public EntryPoint()
        {
            this.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 1920;
            this.graphics.PreferredBackBufferHeight = 1080;

            this.Window.Title = "Survive!";
        }

        protected override void Initialize()
        {
            GameStateHandler.CurrentGameState = new Gameplay();

            GameStateHandler.CurrentGameState.Initialize(this.graphics);

            InputHandler.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            Debuger.LoadContent(this.Content, this.graphics);

            GameStateHandler.CurrentGameState.LoadContent(this.Content, this.graphics);

            InputHandler.LoadContent();
        }

        protected override void UnloadContent()
        {
            GameStateHandler.CurrentGameState.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            InputHandler.Update();

            GameStateHandler.CurrentGameState.Update(this.Content, this.graphics, gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GameStateHandler.CurrentGameState.Draw(this.spriteBatch, this.graphics);

            base.Draw(gameTime);
        }
    }
}

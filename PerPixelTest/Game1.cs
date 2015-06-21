namespace PerPixelTest
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PerPixelTest.Managers;
    using PerPixelTest.Managers.GameStates;

    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        

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

            Debuger.LoadContent(Content, graphics);

            GameStateHandler.currentGameState.LoadContent(Content, this.graphics);

            InputHandler.LoadContent();

            
        }

        protected override void UnloadContent()
        {
            GameStateHandler.currentGameState.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            InputHandler.Update();

            GameStateHandler.currentGameState.Update(Content, graphics, gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GameStateHandler.currentGameState.Draw(spriteBatch, graphics);

            

            base.Draw(gameTime);
        }
    }
}

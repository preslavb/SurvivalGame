namespace PerPixelTest
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PerPixelTest.Managers;
    using PerPixelTest.Managers.GameStates;

    public class EntryPoint : Game
    {        
        public EntryPoint()
        {
            Globals.content = this.Content;
            Globals.graphics = new GraphicsDeviceManager(this);
            Globals.content.RootDirectory = "Content";

            Globals.graphics.PreferredBackBufferWidth = 1920;
            Globals.graphics.PreferredBackBufferHeight = 1080;

            this.Window.Title = "Survive!";
        }

        protected override void Initialize()
        {
            GameStateHandler.CurrentGameState = new Gameplay();

            GameStateHandler.CurrentGameState.Initialize(Globals.graphics);

            InputHandler.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

            Debuger.LoadContent(this.Content, Globals.graphics);

            GameStateHandler.CurrentGameState.LoadContent(this.Content, Globals.graphics);

            InputHandler.LoadContent();
        }

        protected override void UnloadContent()
        {
            GameStateHandler.CurrentGameState.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            InputHandler.Update();

            GameStateHandler.CurrentGameState.Update(this.Content, Globals.graphics, gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GameStateHandler.CurrentGameState.Draw();

            base.Draw(gameTime);
        }
    }
}

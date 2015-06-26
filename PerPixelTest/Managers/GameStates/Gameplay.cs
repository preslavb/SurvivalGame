namespace PerPixelTest.Managers.GameStates
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using PerPixelTest.Interfaces;
    using PerPixelTest.Levels;

    public class Gameplay : IGameState
    {
        private bool paused;

        private AbstractLevel currentLevel;

        public bool Initialized { get; set; }

        public bool Loaded { get; set; }

        public void Initialize(GraphicsDeviceManager graphics)
        {
            this.currentLevel = new Camp();
            this.currentLevel.Initialize(graphics);
            this.paused = false;

            this.Initialized = true;
        }

        public void LoadContent(ContentManager content, GraphicsDeviceManager graphics)
        {
            this.currentLevel.LoadContent(content, graphics);

            this.Loaded = true;
        }

        public void UnloadContent()
        {
        }

        public void Update(ContentManager content, GraphicsDeviceManager graphics, GameTime gameTime)
        {
            if (!this.Initialized)
            {
                this.Initialize(graphics);
            }

            if (!this.Loaded)
            {
                this.LoadContent(content, graphics);
            }

            if (!this.currentLevel.Initialized)
            {
                this.currentLevel.Initialize(graphics);
            }

            if (!this.currentLevel.Loaded)
            {
                this.currentLevel.LoadContent(content, graphics);
            }

            InputHandler.CheckPauseKey(this.paused);

            if (!this.paused)
            {
                for (int i = 0; i < InputHandler.PressedKeys.Count; i++)
                {
                    if ((InputHandler.PressedKeys[i] == Keys.P && InputHandler.PressedKeysStates[i] == InputHandler.KeyState.Clicked) && !(GameStateHandler.CurrentGameState is SplashScreen))
                    {
                        GameStateHandler.CurrentGameState = new SplashScreen();
                    }
                }

                this.currentLevel.Update(gameTime);
            }
        }

        public void Draw()
        {
            this.currentLevel.Draw();
        }
    }
}

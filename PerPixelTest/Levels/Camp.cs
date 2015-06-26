namespace PerPixelTest.Levels
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using PerPixelTest.Camera;
    using PerPixelTest.Characters;
    using PerPixelTest.Interfaces;
    using PerPixelTest.Sprites;

    public class Camp : Level
    {
        private Camera2D camera;

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            this.camera = new Camera2D(graphics.GraphicsDevice.Viewport);
            this.PlayerInLevel = new Player();
            this.Objects = new List<GameObject>();

            this.PlayerInLevel.Acceleration = new Vector2(0.0f, 0.0f);

            this.Initialized = true;
        }

        public override void LoadContent(ContentManager content, GraphicsDeviceManager graphics)
        {
            this.BackgroundLayers = new List<Layer>()
            {
                new Layer(this.camera) { Parallax = new Vector2(0.2f, 1.0f) },
                new Layer(this.camera) { Parallax = new Vector2(0.4f, 1.0f) },
            };

            this.ForegroundLayers = new List<Layer>()
            {
                new Layer(this.camera) { Parallax = new Vector2(1.5f, 1.0f) }
            };

            // Draw And Position Background
            this.BackgroundLayers[0].Sprites.Add(new Sprite(content.Load<Texture2D>("sky")));
            this.BackgroundLayers[1].Sprites.Add(new Sprite(content.Load<Texture2D>("mountain")));

            this.BackgroundLayers[0].Sprites[0].Position = this.BackgroundLayers[0].ScreenToWorld(new Vector2(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Y));
            this.BackgroundLayers[1].Sprites[0].Position = this.BackgroundLayers[1].ScreenToWorld(new Vector2(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Y - 200));

            // Draw And Position Foreground
            this.ForegroundLayers[0].Sprites.Add(new Sprite(content.Load<Texture2D>("floorSprite")));

            this.ForegroundLayers[0].Sprites[0].Position = this.ForegroundLayers[0].ScreenToWorld(new Vector2(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Y + 700));

            this.PlayerInLevel.LoadContent();

            // Load And Position Player Character
            this.PlayerInLevel.Position = new Vector2(-this.PlayerInLevel.PlayerRectangle.Height / 2, 0);

            // Load And Position Ground Texture
            this.Ground = new Sprite(content.Load<Texture2D>("floorSprite"));
            this.Ground.Position = new Vector2(0, 0) + new Vector2(-this.Ground.Texture.Width / 2, this.PlayerInLevel.Position.Y + this.PlayerInLevel.PlayerRectangle.Height);
  
            this.Objects.Add(this.Ground);
            this.Objects.Add(this.PlayerInLevel);

            this.Loaded = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (!((Keyboard.GetState().IsKeyDown(Keys.D)) || (Keyboard.GetState().IsKeyDown(Keys.A))))
            {
                this.PlayerInLevel.PlayerAnimation.CurrentAnimationState = PlayerInLevel.PlayerAnimation.AnimationActions["Running"];
            }

            // Focus Camera on Player
            this.camera.Focus(this.PlayerInLevel.Position);

            this.PlayerInLevel.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            foreach (Layer layer in this.BackgroundLayers)
            {
                layer.Draw(spriteBatch);
            }

            spriteBatch.Begin(
                             SpriteSortMode.BackToFront,
                             BlendState.AlphaBlend,
                             null,
                             null,
                             null,
                             null,
                             this.camera.GetTransformation(graphics.GraphicsDevice));

            foreach (GameObject gameObject in this.Objects)
            {
                gameObject.Draw(spriteBatch, graphics);
            }

            spriteBatch.End();

            foreach (Layer layer in this.ForegroundLayers)
            {
                layer.Draw(spriteBatch);
            }
        }
    }
}

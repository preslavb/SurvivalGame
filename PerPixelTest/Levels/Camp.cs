namespace PerPixelTest.Levels
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using PerPixelTest.Camera;
    using PerPixelTest.Characters;
    using PerPixelTest.Interfaces;
    using PerPixelTest.Sprites;
    using System.Collections.Generic;
    class Camp : Level
    {
        private Camera2D camera;

        public Camp()
        {

        }
        public override void Initialize(GraphicsDeviceManager graphics)
        {
            this.camera = new Camera2D(graphics.GraphicsDevice.Viewport);
            this.player = new Player();
            this.Objects = new List<GameObject>();

            this.player.Initialize();

            this.player.LeftRestricted = false;
            this.player.RightRestricted = false;

            this.player.Acceleration = new Vector2(0.0f, 0.0f);

            this.Initialized = true;
        }

        public override void LoadContent(ContentManager Content, GraphicsDeviceManager graphics)
        {
            this.backgroundLayers = new List<Layer>()
            {
                new Layer(this.camera) { Parallax = new Vector2(0.2f, 1.0f) },
                new Layer(this.camera) { Parallax = new Vector2(0.4f, 1.0f) },
            };

            this.foregroundLayers = new List<Layer>()
            {
                new Layer(this.camera) { Parallax = new Vector2(1.5f, 1.0f) }
            };

            // Draw And Position Background
            this.backgroundLayers[0].Sprites.Add(new Sprite(Content.Load<Texture2D>("sky")));
            this.backgroundLayers[1].Sprites.Add(new Sprite(Content.Load<Texture2D>("mountain")));

            this.backgroundLayers[0].Sprites[0].Position = this.backgroundLayers[0].ScreenToWorld(new Vector2(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Y));
            this.backgroundLayers[1].Sprites[0].Position = this.backgroundLayers[1].ScreenToWorld(new Vector2(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Y - 200));

            // Draw And Position Foreground
            this.foregroundLayers[0].Sprites.Add(new Sprite(Content.Load<Texture2D>("floorSprite")));

            this.foregroundLayers[0].Sprites[0].Position = this.foregroundLayers[0].ScreenToWorld(new Vector2(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Y + 700));

            this.player.LoadContent(Content);

            // Load And Position Player Character
            //this.player.Position = this.backgroundLayers[2].ScreenToWorld(new Vector2(-this.player.Rect.Height / 2, 0));
            this.player.Position = new Vector2(-this.player.Rect.Height / 2, 0);
            this.player.Data = new Color[this.player.SpriteSheet.Width * this.player.SpriteSheet.Height];
            this.player.SpriteSheet.GetData(this.player.Data);

            // Load And Position Ground Texture
            this.ground = new Sprite(Content.Load<Texture2D>("floorSprite"));
            //this.ground.Position = this.backgroundLayers[2].ScreenToWorld(new Vector2(0, 0)) + new Vector2(-this.ground.Texture.Width / 2, this.player.Position.Y + this.player.Rect.Height);
            this.ground.Position = new Vector2(0, 0) + new Vector2(-this.ground.Texture.Width / 2, this.player.Position.Y + this.player.Rect.Height);
  
            this.Objects.Add(this.ground);
            this.Objects.Add(this.player);

            this.Loaded = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (!((Keyboard.GetState().IsKeyDown(Keys.D) && !this.player.RightRestricted) || (Keyboard.GetState().IsKeyDown(Keys.A) && !this.player.LeftRestricted)))
            {
                this.player.PlayerAnimation.SpritesPositionY = 271;
                this.player.PlayerAnimation.AmountOfFramesX = 5;
                this.player.PlayerAnimation.ApplyChanges();
            }

            // Focus Camera on Player
            this.camera.Focus(this.player.Position);

            this.player.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            foreach (Layer layer in this.backgroundLayers)
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
                             this.camera.GetTransformation(graphics.GraphicsDevice)
                             );

            foreach (GameObject gameObject in Objects)
            {
                gameObject.Draw(spriteBatch, graphics);
            }

            spriteBatch.End();

            foreach (Layer layer in this.foregroundLayers)
            {
                layer.Draw(spriteBatch);
            }
        }
    }
}

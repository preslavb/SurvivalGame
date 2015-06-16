namespace PerPixelTest.Characters
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using PerPixelTest.Interfaces;
    using PerPixelTest.Managers;
    using PerPixelTest.Sprites.Animation;

    public class Player
    {
        private const float DEFAULT_JUMP_INIT_VELOCITY = -25;
        private const float TIME_IN_AIR = 0;
        public const float GRAVITY = 1.1f;
        private const float FRICTION_FORCE = 0.8f;
        private const float MAX_PLAYER_SPEED = 8;

        private float jumpVelocity;

        public Player()
        {
            this.Falling = false;
        }

        public Texture2D SpriteSheet { get; set; }

        public Texture2D Texture { get; set; }

        public Animation PlayerAnimation { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 Acceleration { get; set; }

        public Color[] Data { get; set; }

        public bool Falling { get; set; }

        public bool Jumped { get; set; }

        public bool LeftRestricted { get; set; }

        public bool RightRestricted { get; set; }

        public bool FacingRight { get; set; }

        //public bool[] ActiveOctants { get; set; }

        public Rectangle Rect { get; set; }

        public void Initialize()
        {
            this.PlayerAnimation = new Animation();
            this.FacingRight = true;
            this.PlayerAnimation.SpritesPosition = new Rectangle(0, 135, 950, 135);
            this.PlayerAnimation.Initialize(this.Position, new Vector2(10, 1));
            this.jumpVelocity = 0.0f;
        }

        public void LoadContent(ContentManager content)
        {
            this.SpriteSheet = content.Load<Texture2D>("actions");
            this.PlayerAnimation.AnimationImage = this.SpriteSheet;
        }

        public void Update(GameTime gameTime)
        {
            this.PlayerAnimation.Active = true;
            this.PlayerAnimation.Update(gameTime);
            this.PlayerAnimation.Position = this.Position;

            this.Rect = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.PlayerAnimation.Source.Width, this.PlayerAnimation.Source.Height);

            if (Keyboard.GetState().IsKeyDown(Keys.A) && !this.LeftRestricted)
            {
                if (this.Acceleration.X > -MAX_PLAYER_SPEED)
                {
                    this.Acceleration = new Vector2(Acceleration.X - 1, Acceleration.Y);
                }

                this.FacingRight = false;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) && !this.RightRestricted)
            {
                if (this.Acceleration.X < MAX_PLAYER_SPEED)
                {
                    this.Acceleration = new Vector2(Acceleration.X + 1, Acceleration.Y);
                }

                this.FacingRight = true;
            }
            else
            {
                if (this.Acceleration.X > FRICTION_FORCE)
                {
                    this.Acceleration = new Vector2(Acceleration.X - FRICTION_FORCE, Acceleration.Y);
                }
                else if (this.Acceleration.X < -FRICTION_FORCE)
                {
                    this.Acceleration = new Vector2(Acceleration.X + FRICTION_FORCE, Acceleration.Y);
                }
                else
                {
                    this.Acceleration = new Vector2(0 , Acceleration.Y);
                }
            }

            for (int i = 0; i < InputHandler.PressedKeys.Count; i++)
            {
                if (InputHandler.PressedKeys[i] == Keys.Space && InputHandler.PressedKeysStates[i] == InputHandler.KeyState.Clicked)
                {
                    this.Position = new Vector2(this.Position.X, this.Position.Y-1);
                    this.Jumped = true;
                    this.jumpVelocity = DEFAULT_JUMP_INIT_VELOCITY;
                }
            }

            if (this.Falling)
            {
                this.Acceleration = new Vector2(this.Acceleration.X, this.jumpVelocity);
                this.jumpVelocity += GRAVITY;
                this.PlayerAnimation.SpritesPositionY = 0;
                this.PlayerAnimation.AmountOfFramesX = 10;
                this.PlayerAnimation.ApplyChanges();
            }
            else
            {
                this.PlayerAnimation.SpritesPositionY = 136;
                this.PlayerAnimation.AmountOfFramesX = 12;
                this.PlayerAnimation.ApplyChanges();
            }

            this.Move(this.Acceleration);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.PlayerAnimation.Draw(spriteBatch, this.FacingRight);
        }

        public void Move(Vector2 value)
        {
            this.Position += value;
        }
    }
}

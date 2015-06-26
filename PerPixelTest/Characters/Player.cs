namespace PerPixelTest.Characters
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using PerPixelTest.Interfaces;
    using PerPixelTest.Managers;
    using PerPixelTest.Sprites.Animation;
    using System.Collections.Generic;

    public class Player : GameObject
    {
        private const float FRICTION_FORCE = 0.8f;
        private const float MAX_PLAYER_SPEED = 8;

        public Animation PlayerAnimation { get; set; }

        private bool FacingRight { get; set; }

        public Vector2 Acceleration { get; set; }

        public Rectangle PlayerRectangle { get; set; }

        public Player()
        {
        }

        public void LoadContent()
        {
            AnimationFactory.CreatePlayerAnimations(this);
        }

        public void Update(GameTime gameTime)
        {
            this.PlayerAnimation.Active = true;
            this.PlayerAnimation.Update(gameTime);
            this.PlayerAnimation.AnimationPosition = this.Position;

            this.PlayerRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.PlayerAnimation.SourceRectangle.Width, this.PlayerAnimation.SourceRectangle.Height);
            if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.PlayerAnimation.CurrentAnimationState = this.PlayerAnimation.AnimationActions["Walking"];
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    if (this.Acceleration.X > -MAX_PLAYER_SPEED)
                    {
                        this.Acceleration = new Vector2(this.Acceleration.X - 1, this.Acceleration.Y);
                    }

                    this.FacingRight = false;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    if (this.Acceleration.X < MAX_PLAYER_SPEED)
                    {
                        this.Acceleration = new Vector2(this.Acceleration.X + 1, this.Acceleration.Y);
                    }

                    this.FacingRight = true;
                }
            }
            else
            {
                this.PlayerAnimation.CurrentAnimationState = this.PlayerAnimation.AnimationActions["Dying"];

                if (this.Acceleration.X > FRICTION_FORCE)
                {
                    this.Acceleration = new Vector2(this.Acceleration.X - FRICTION_FORCE, this.Acceleration.Y);
                }
                else if (this.Acceleration.X < -FRICTION_FORCE)
                {
                    this.Acceleration = new Vector2(this.Acceleration.X + FRICTION_FORCE, this.Acceleration.Y);
                }
                else
                {
                    this.Acceleration = new Vector2(0, this.Acceleration.Y);
                }
            }

            this.Move(this.Acceleration);
        }

        public override void Draw()
        {
            this.PlayerAnimation.Draw(Globals.spriteBatch, this.FacingRight);
        }

        public void Move(Vector2 value)
        {
            this.Position += value;
        }
    }
}

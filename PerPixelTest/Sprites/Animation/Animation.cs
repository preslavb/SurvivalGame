namespace PerPixelTest.Sprites.Animation
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    public class Animation
    {
        private AnimationState currentAnimationState;
        public bool Active { get; set; }

        public Texture2D SpriteSheet { get; set; }

        public Dictionary<string, AnimationState> AnimationActions { get; set; }

        public AnimationState CurrentAnimationState
        {
            get
            {
                return currentAnimationState;
            }
            set
            {
                if (currentAnimationState != null)
                {
                    if (!currentAnimationState.Equals(value))
                    {
                        this.CurrentFramePosition = new Vector2(0, this.CurrentFramePosition.Y);
                        this.currentAnimationState = value;
                    }
                }
                else
                {
                    this.currentAnimationState = value;
                }
            }
        }

        public Vector2 CurrentFramePosition { get; set; }

        public Vector2 AmountOfFrames { get; set; }

        public int FramesElapsed { get; set; }

        public int SwitchFrameTimer { get; set; }

        public Vector2 AnimationPosition { get; set; }

        public Rectangle SourceRectangle { get; set; }

        public Animation(Texture2D spriteSheet, Vector2 objectPosition)
        {
            this.Active = false;
            this.SpriteSheet = spriteSheet;
            AnimationActions = new Dictionary<string, AnimationState>();
        }

        public void Update(GameTime gameTime)
        {
            if (this.Active) 
            { 
                this.FramesElapsed += (int)gameTime.ElapsedGameTime.TotalMilliseconds; 
            }
            else 
            { 
                this.FramesElapsed = 0; 
            }
            
            if (this.FramesElapsed >= this.CurrentAnimationState.AnimationSpeed)
            {
                this.FramesElapsed = 0;

                this.CurrentFramePosition = new Vector2(CurrentFramePosition.X + this.CurrentAnimationState.FrameDimensions.X, CurrentAnimationState.ActionDimensions.Y);

                if (this.CurrentFramePosition.X >= this.CurrentAnimationState.ActionDimensions.Width)
                {
                    this.CurrentFramePosition = new Vector2(0, CurrentAnimationState.ActionDimensions.Y);
                }  
            }

            this.SourceRectangle = new Rectangle((int)this.CurrentFramePosition.X, (int)this.CurrentAnimationState.ActionDimensions.Y, (int)this.CurrentAnimationState.FrameDimensions.X, (int)this.CurrentAnimationState.FrameDimensions.Y);
        }

        public void Draw(SpriteBatch spriteBatch, bool facingRight)
        {
            if (facingRight)
            {
                spriteBatch.Draw(this.SpriteSheet, this.AnimationPosition, this.SourceRectangle, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(this.SpriteSheet, this.AnimationPosition, this.SourceRectangle, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, 0);
            }
        }
    }
}

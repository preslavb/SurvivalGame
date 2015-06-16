namespace PerPixelTest.Sprites.Animation
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Animation
    {
        private Vector2 currentFrame;
        private Rectangle sourceRect;
        private Rectangle spritesPosition;
        private Vector2 amountOfFrames;

        public Vector2 CurrentFrame
        {
            get
            {
                return this.currentFrame;
            }

            set
            {
                this.currentFrame = value;
            }
        }

        public float CurrentFrameX
        {
            get
            {
                return this.currentFrame.X;
            }

            set
            {
                this.currentFrame.X = value;
            }
        }

        public float CurrentFrameY
        {
            get
            {
                return this.currentFrame.Y;
            }

            set
            {
                this.currentFrame.Y = value;
            }
        }

        public int FrameCounter { get; set; }

        public int SwitchFrame { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 AmountOfFrames 
        {
            get
            {
                return this.amountOfFrames;
            }

            set
            {
                this.amountOfFrames = value;
            } 
        }

        public float AmountOfFramesX
        {
            get
            {
                return this.amountOfFrames.X;
            }

            set
            {
                this.amountOfFrames.X = value;
            }
        }

        public float AmountOfFramesY
        {
            get
            {
                return this.amountOfFrames.Y;
            }

            set
            {
                this.amountOfFrames.Y = value;
            }
        }

        public Texture2D AnimationImage { get;  set; }

        public bool Active { get; set; }

        public Rectangle SpritesPosition 
        {
            get 
            {
                return this.spritesPosition;
            }

            set
            {
                this.spritesPosition = value;
            } 
        }

        public int SpritesPositionX
        {
            get
            {
                return this.spritesPosition.X;
            }

            set
            {
                this.spritesPosition.X = value;
            }
        }

        public int SpritesPositionY 
        {
            get 
            {
                return this.spritesPosition.Y;
            }

            set
            {
                this.spritesPosition.Y = value;
            } 
        }

        public int SpritesPositionWidth
        {
            get
            {
                return this.spritesPosition.Width;
            }

            set
            {
                this.spritesPosition.Width = value;
            }
        }

        public int SpritesPositionHeight
        {
            get
            {
                return this.spritesPosition.Height;
            }

            set
            {
                this.spritesPosition.Height = value;
            }
        }

        public Rectangle Source
        {
            get
            {
                return this.sourceRect;
            }

            set
            {
                this.sourceRect = value;
            }
        }

        public int SourceX
        {
            get
            {
                return this.sourceRect.X;
            }

            set
            {
                this.sourceRect.X = value;
            }
        }

        public int SourceY
        {
            get
            {
                return this.sourceRect.Y;
            }

            set
            {
                this.sourceRect.Y = value;
            }
        }

        public int SourceWidth
        {
            get
            {
                return this.sourceRect.Width;
            }

            set
            {
                this.sourceRect.Width = value;
            }
        }

        public int SourceHeight
        {
            get
            {
                return this.sourceRect.Height;
            }

            set
            {
                this.sourceRect.Height = value;
            }
        }

        public int FrameWidth 
        {
            get 
            { 
                return this.SpritesPosition.Width / (int)this.AmountOfFrames.X; 
            } 
        }

        public int FrameHeight 
        { 
            get 
            { 
                return this.SpritesPosition.Height / (int)this.AmountOfFrames.Y; 
            } 
        }

        public void ApplyChanges()
        {
            this.SpritesPositionWidth = (int)this.AmountOfFramesX * 95;
        }

        public void Initialize(Vector2 position, Vector2 frames)
        {
            this.Active = false;
            this.SwitchFrame = 60;
            this.Position = position;
            this.AmountOfFrames = frames;
        }

        public void Update(GameTime gameTime)
        {
            if (this.Active) 
            { 
                this.FrameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds; 
            }
            else 
            { 
                this.FrameCounter = 0; 
            }
            
            if (this.FrameCounter >= this.SwitchFrame)
            {
                this.FrameCounter = 0;
                this.CurrentFrameX += this.FrameWidth;
                if (this.CurrentFrameX >= this.SpritesPosition.Width)
                {
                    this.CurrentFrameX = 0;
                }   
            }

            this.Source = new Rectangle((int)this.currentFrame.X, (int)this.SpritesPosition.Y, this.FrameWidth, this.FrameHeight);
            this.SourceX = (int)this.currentFrame.X;
            this.SourceY = (int)this.SpritesPosition.Y;
            this.SourceWidth = this.FrameWidth;
            this.SourceHeight = this.FrameHeight;
        }

        public void Draw(SpriteBatch spriteBatch, bool facingRight)
        {
            if (facingRight)
            {
                spriteBatch.Draw(this.AnimationImage, this.Position, this.Source, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(this.AnimationImage, this.Position, this.Source, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, 0);
            }
        }
    }
}

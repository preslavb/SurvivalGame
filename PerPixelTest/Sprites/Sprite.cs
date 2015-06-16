namespace PerPixelTest.Sprites
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PerPixelTest.Interfaces;

    public class Sprite
    {
        private Texture2D texture;
        private Vector2 position;
        private Color[] collisionData;
        private Rectangle collisionRect;

        public Sprite(Texture2D newTexture) 
        {
            this.SpriteSheet = newTexture;
            this.Position = new Vector2(0, 0);
            this.Data = new Color[this.SpriteSheet.Width * this.SpriteSheet.Height];
            newTexture.GetData(this.Data);
            this.Rect = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.SpriteSheet.Width, this.SpriteSheet.Height);
        }

        public Sprite(Texture2D newTexture, Vector2 newPosition)
        {
            this.SpriteSheet = newTexture;
            this.Position = newPosition;
            this.Data = new Color[this.SpriteSheet.Width * this.SpriteSheet.Height];
            this.SpriteSheet.GetData(this.Data);
            this.Rect = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.SpriteSheet.Width, this.SpriteSheet.Height);
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public Texture2D SpriteSheet
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        public Color[] Data
        {
            get { return this.collisionData; }
            set { this.collisionData = value; }
        }

        public Rectangle Rect
        {
            get 
            { 
                return this.collisionRect; 
            }

            set 
            {
                this.collisionRect = value; 
            }
        }
        public bool[] ActiveOctants
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.SpriteSheet != null)
            {
                spriteBatch.Draw(this.SpriteSheet, this.Position, Color.White);
            }
        }
    }
}

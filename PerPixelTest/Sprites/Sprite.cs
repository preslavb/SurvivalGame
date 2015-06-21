namespace PerPixelTest.Sprites
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PerPixelTest.Interfaces;

    public class Sprite : GameObject
    {
        private Color[] collisionData;

        public Sprite(Texture2D newTexture) 
        {
            this.Texture = newTexture;
            this.Position = new Vector2(0, 0);
            this.Data = new Color[this.Texture.Width * this.Texture.Height];
            newTexture.GetData(this.Data);
            this.CollisionRect = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Texture.Width, this.Texture.Height);
        }

        public Sprite(Texture2D newTexture, Vector2 newPosition)
        {
            this.Texture = newTexture;
            this.Position = newPosition;
            this.Data = new Color[this.Texture.Width * this.Texture.Height];
            this.Texture.GetData(this.Data);
            this.CollisionRect = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Texture.Width, this.Texture.Height);
        }

        public Color[] Data
        {
            get { return this.collisionData; }
            set { this.collisionData = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.Texture != null)
            {
                spriteBatch.Draw(this.Texture, this.Position, Color.White);
            }
        }
    }
}

namespace PerPixelTest.Sprites
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PerPixelTest.Camera;

    public class Layer
    {
        private readonly Camera2D camera;

        public Layer(Camera2D newCamera)
        {
            this.camera = newCamera;
            this.Parallax = Vector2.One;
            this.Sprites = new List<Sprite>();
        }

        public Vector2 Parallax { get; set; }

        public List<Sprite> Sprites { get; private set; }

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, this.camera.GetViewMatrix(this.Parallax));
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(this.camera.GetViewMatrix(this.Parallax)));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, this.camera.GetViewMatrix(this.Parallax));
            foreach (Sprite sprite in this.Sprites)
            {
                sprite.Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}

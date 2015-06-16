namespace PerPixelTest.Camera
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Camera2D
    {
        private float zoom;
        private Matrix transform;
        private Vector2 pos;
        private float rotation;
        private Vector2 origin;

        public Camera2D(Viewport viewport)
        {
            this.origin = new Vector2(viewport.Width / 2, viewport.Height / 2);
            this.zoom = 1.0f;
            this.rotation = 0.0f;
            this.pos = Vector2.Zero;
        }

        public float Zoom
        {
            set
            {
                this.zoom = value;
                if (this.zoom < 0.1f)
                {
                    this.zoom = 0.1f;
                }
            }
        }

        public float Rotation
        {
            get { return this.rotation; }
            set { this.rotation = value; }
        }

        public Vector2 Origin { get; set; }

        public Vector2 Pos { get; set; }

        public void Focus(Vector2 focusObjectPosition)
        {
            if (focusObjectPosition == null)
            {
                this.pos = Vector2.Zero;
            }
            else
            {
                this.pos = focusObjectPosition;
            }
        }

        public void Move(Vector2 amount)
        {
            this.pos += amount;
        }

        public Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            this.transform =
                Matrix.CreateTranslation(new Vector3(-this.pos.X, -this.pos.Y, 0)) *
                Matrix.CreateRotationZ(this.Rotation) *
                Matrix.CreateScale(new Vector3(this.zoom, this.zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
            return this.transform;
        }

        public Matrix GetViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-this.pos * parallax, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-this.origin, 0.0f)) *
                Matrix.CreateRotationZ(this.rotation) *
                Matrix.CreateScale(this.zoom, this.zoom, 1) *
                Matrix.CreateTranslation(new Vector3(this.origin, 0.0f));
        }
    }
}

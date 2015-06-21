namespace PerPixelTest.Levels
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using PerPixelTest.Characters;
    using PerPixelTest.Interfaces;
    using PerPixelTest.Sprites;
    using System;
    using System.Collections.Generic;
    public class Level
    {
        protected List<Layer> backgroundLayers;
        protected List<Layer> foregroundLayers;

        protected Player player;

        protected Sprite ground;

        public List<GameObject> Objects { get; set; }

        public bool Initialized { get; set; }
        public bool Loaded { get; set; }

        public virtual void Initialize(GraphicsDeviceManager graphics)
        {
            throw new NotImplementedException("Each unique level needs to have it's own override of Initialize() method");
        }

        public virtual void LoadContent(ContentManager Content, GraphicsDeviceManager graphics)
        {
            throw new NotImplementedException("Each unique level needs to have it's own override of LoadContent() method");
        }

        public virtual void Update(GameTime gameTime)
        {
            throw new NotImplementedException("Each unique level needs to have it's own override of Update() method");
        }

        public virtual void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            throw new NotImplementedException("Each unique level needs to have it's own override of Draw() method");
        }
    }
}

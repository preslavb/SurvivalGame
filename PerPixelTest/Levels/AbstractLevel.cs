namespace PerPixelTest.Levels
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using PerPixelTest.Characters;
    using PerPixelTest.Interfaces;
    using PerPixelTest.Sprites;

    public class AbstractLevel
    {
        private List<Layer> backgroundLayers;
        private List<Layer> foregroundLayers;

        private Player player;

        private Sprite ground;

        public List<GameObject> Objects { get; set; }

        public bool Initialized { get; set; }

        public bool Loaded { get; set; }

        protected List<Layer> BackgroundLayers 
        {
            get
            {
                return this.backgroundLayers;
            }

            set
            {
                this.backgroundLayers = value;
            }
        }

        protected List<Layer> ForegroundLayers
        {
            get
            {
                return this.foregroundLayers;
            }

            set
            {
                this.foregroundLayers = value;
            }
        }

        protected Player PlayerInLevel
        {
            get
            {
                return this.player;
            }

            set
            {
                this.player = value;
            }
        }

        protected Sprite Ground
        {
            get
            {
                return this.ground;
            }

            set
            {
                this.ground = value;
            }
        }

        public virtual void Initialize(GraphicsDeviceManager graphics)
        {
            throw new NotImplementedException("Each unique level needs to have it's own override of Initialize() method");
        }

        public virtual void LoadContent(ContentManager content, GraphicsDeviceManager graphics)
        {
            throw new NotImplementedException("Each unique level needs to have it's own override of LoadContent() method");
        }

        public virtual void Update(GameTime gameTime)
        {
            throw new NotImplementedException("Each unique level needs to have it's own override of Update() method");
        }

        public virtual void Draw()
        {
            throw new NotImplementedException("Each unique level needs to have it's own override of Draw() method");
        }
    }
}

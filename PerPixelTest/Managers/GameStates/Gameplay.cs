namespace PerPixelTest.Managers.GameStates
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework.Graphics;
    using PerPixelTest.Interfaces;
    using Microsoft.Xna.Framework;
    using PerPixelTest.Camera;
    using PerPixelTest.Sprites;
    using PerPixelTest.Characters;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Input;
    using PerPixelTest.Levels;

    public class Gameplay : IGameState
    {
        private bool paused;

        private Level currentLevel;

        public bool Initialized { get; set; }
        public bool Loaded { get; set; }

        public void Initialize(GraphicsDeviceManager graphics)
        {
            this.currentLevel = new Camp();
            this.currentLevel.Initialize(graphics);
            this.paused = false;

            this.Initialized = true;
        }

        public void LoadContent(ContentManager Content, GraphicsDeviceManager graphics)
        {
            this.currentLevel.LoadContent(Content, graphics);

            this.Loaded = true;
        }

        public void UnloadContent()
        {
        }

        public void Update(ContentManager Content, GraphicsDeviceManager graphics, GameTime gameTime)
        {
            if (!Initialized)
            {
                Initialize(graphics);
            }

            if (!Loaded)
            {
                LoadContent(Content, graphics);
            }

            if (!currentLevel.Initialized)
            {
                currentLevel.Initialize(graphics);
            }

            if (!currentLevel.Loaded)
            {
                currentLevel.LoadContent(Content, graphics);
            }

            checkPauseKey(InputHandler.PressedKeys, InputHandler.PressedKeysStates);

            if (!this.paused)
            {
                for (int i = 0; i < InputHandler.PressedKeys.Count; i++)
                {
                    if ((InputHandler.PressedKeys[i] == Keys.P && InputHandler.PressedKeysStates[i] == InputHandler.KeyState.Clicked) && !(GameStateHandler.currentGameState is SplashScreen))
                    {
                        GameStateHandler.currentGameState = new SplashScreen();
                    }
                }

                this.currentLevel.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            this.currentLevel.Draw(spriteBatch, graphics);
            
        }

        private void checkPauseKey(List<Keys> pressedKeys, List<InputHandler.KeyState> pressedKeysStates)
        {
            for (int i = 0; i < pressedKeys.Count; i++)
            {
                if (pressedKeys[i] == Keys.Escape && InputHandler.PressedKeysStates[i] == InputHandler.KeyState.Clicked)
                {
                    if (!paused)
                        paused = true;
                    else
                        paused = false;
                }
            }
        }

        //private static CollisionType IntersectsPixel(ICollidable firstObj, ICollidable secondObj)
        //{
        //    
        //    //ScanOctants(firstObj, secondObj);
        //
        //    return CollisionType.NoCollision;
        //}

        //private static void ScanOctants(ICollidable obj1, ICollidable obj2)
        //{
        //    double startSlope = 1.0;
        //    double endSlope = 0.0;

        //    int x = 0;
        //    int y = 0;

        //    if (obj2.Rect.Top <= obj1.Position.Y)
        //    {


        //        for (int i = 0; i < obj1.ActiveOctants.Length; i++)
        //        {
        //            switch (i)
        //            {
        //                case 0:
        //                    y = (int)obj1.Position.Y;
        //                    x = (int)obj1.Position.X;

        //                    while (GetSlope(x, y, obj1.Position.X + (obj1.Rect.Width / 2), obj1.Position.Y + (obj1.Rect.Height / 2), false) >= endSlope)
        //                    {
        //                        //Color color1 = obj1.Data[(x - obj1.Rect.Left) + ((y - obj1.Rect.Top) * obj1.Rect.Width)];
        //                        //Console.WriteLine((x - obj2.Rect.Left) + ((y - obj2.Rect.Top) * obj2.Rect.Width));
        //                        //Console.WriteLine(obj2.Rect.Top);
        //                        Color color2 = obj2.Data[(x - obj2.Rect.Left) + ((y - obj2.Rect.Top) * obj2.Rect.Width)];

        //                        if (color2.R != 0)
        //                        {
        //                            obj1.ActiveOctants[i].Active = true;
        //                        }
        //                        else
        //                        {
        //                            obj1.ActiveOctants[i].Active = false;
        //                        }
        //                        x++;
        //                    }
        //                    break;
        //                case 1:
        //                    y = (int)obj1.Position.Y;
        //                    x = (int)obj1.Position.X + (obj1.Rect.Width / 2) + Convert.ToInt32(startSlope * (obj1.Rect.Width / 2));

        //                    while (GetSlope(x, y, obj1.Position.X + (obj1.Rect.Width / 2), obj1.Position.Y + (obj1.Rect.Height / 2), false) <= endSlope)
        //                    {
        //                        //Color color1 = obj1.Data[(x - obj1.Rect.Left) + ((y - obj1.Rect.Top) * obj1.Rect.Width)];
        //                        Color color2 = obj2.Data[(x - obj2.Rect.Left) + ((y - obj2.Rect.Top) * obj2.Rect.Width)];

        //                        if (color2.R != 0)
        //                        {
        //                            obj1.ActiveOctants[i].Active = true;
        //                        }
        //                        else
        //                        {
        //                            obj1.ActiveOctants[i].Active = false;
        //                        }
        //                        x--;
        //                    }
        //                    break;
        //                case 2:
        //                    y = (int)obj1.Position.Y + (obj1.Rect.Height / 2) - Convert.ToInt32(startSlope * (obj1.Rect.Height / 2));
        //                    x = (int)obj1.Position.X + obj1.Rect.Width;

        //                    while (GetSlope(x, y, obj1.Position.X + (obj1.Rect.Width / 2), obj1.Position.Y + (obj1.Rect.Height / 2), true) <= endSlope)
        //                    {
        //                        //Color color1 = obj1.Data[(x - obj1.Rect.Left) + ((y - obj1.Rect.Top) * obj1.Rect.Width)];
        //                        Color color2 = obj2.Data[(x - obj2.Rect.Left) + ((y - obj2.Rect.Top) * obj2.Rect.Width)];

        //                        if (color2.R != 0)
        //                        {
        //                            obj1.ActiveOctants[i].Active = true;
        //                        }
        //                        else
        //                        {
        //                            obj1.ActiveOctants[i].Active = false;
        //                        }
        //                        y++;
        //                    }
        //                    break;
        //                case 3:
        //                    y = (int)obj1.Position.Y + (obj1.Rect.Height / 2) + Convert.ToInt32(startSlope * (obj1.Rect.Height / 2));
        //                    x = (int)obj1.Position.X + obj1.Rect.Width;

        //                    while (GetSlope(x, y, obj1.Position.X + (obj1.Rect.Width / 2), obj1.Position.Y + (obj1.Rect.Height / 2), true) >= endSlope)
        //                    {
        //                        //Color color1 = obj1.Data[(x - obj1.Rect.Left) + ((y - obj1.Rect.Top) * obj1.Rect.Width)];
        //                        Color color2 = obj2.Data[(x - obj2.Rect.Left) + ((y - obj2.Rect.Top) * obj2.Rect.Width)];

        //                        if (color2.R != 0)
        //                        {
        //                            obj1.ActiveOctants[i].Active = true;
        //                        }
        //                        else
        //                        {
        //                            obj1.ActiveOctants[i].Active = false;
        //                        }
        //                        y--;
        //                    }
        //                    break;
        //                case 4:
        //                    y = (int)obj1.Position.Y + obj1.Rect.Height;
        //                    x = (int)obj1.Position.X + (obj1.Rect.Width / 2) + Convert.ToInt32(startSlope * (obj1.Rect.Width / 2));

        //                    while (GetSlope(x, y, obj1.Position.X + (obj1.Rect.Width / 2), obj1.Position.Y + (obj1.Rect.Height / 2), false) >= endSlope)
        //                    {
        //                        //Color color1 = obj1.Data[(x - obj1.Rect.Left) + ((y - obj1.Rect.Top) * obj1.Rect.Width)];
        //                        Color color2 = obj2.Data[(x - obj2.Rect.Left) + ((y - obj2.Rect.Top) * obj2.Rect.Width)];

        //                        if (color2.R != 0)
        //                        {
        //                            obj1.ActiveOctants[i].Active = true;
        //                        }
        //                        else
        //                        {
        //                            obj1.ActiveOctants[i].Active = false;
        //                        }
        //                        x--;
        //                    }
        //                    break;
        //                case 5:
        //                    y = (int)obj1.Position.Y + obj1.Rect.Height;
        //                    x = (int)obj1.Position.X + (obj1.Rect.Width / 2) - Convert.ToInt32(startSlope * (obj1.Rect.Width / 2));

        //                    while (GetSlope(x, y, obj1.Position.X + (obj1.Rect.Width / 2), obj1.Position.Y + (obj1.Rect.Height / 2), false) <= endSlope)
        //                    {
        //                        //Color color1 = obj1.Data[(x - obj1.Rect.Left) + ((y - obj1.Rect.Top) * obj1.Rect.Width)];
        //                        Color color2 = obj2.Data[(x - obj2.Rect.Left) + ((y - obj2.Rect.Top) * obj2.Rect.Width)];

        //                        if (color2.R != 0)
        //                        {
        //                            obj1.ActiveOctants[i].Active = true;
        //                        }
        //                        else
        //                        {
        //                            obj1.ActiveOctants[i].Active = false;
        //                        }
        //                        x++;
        //                    }
        //                    break;
        //                case 6:
        //                    y = (int)obj1.Position.Y + obj1.Rect.Height;
        //                    x = (int)obj1.Position.X;

        //                    while (GetSlope(x, y, obj1.Position.X + (obj1.Rect.Width / 2), obj1.Position.Y + (obj1.Rect.Height / 2), true) <= endSlope)
        //                    {
        //                        //Color color1 = obj1.Data[(x - obj1.Rect.Left) + ((y - obj1.Rect.Top) * obj1.Rect.Width)];
        //                        Color color2 = obj2.Data[(x - obj2.Rect.Left) + ((y - obj2.Rect.Top) * obj2.Rect.Width)];

        //                        if (color2.R != 0)
        //                        {
        //                            obj1.ActiveOctants[i].Active = true;
        //                        }
        //                        else
        //                        {
        //                            obj1.ActiveOctants[i].Active = false;
        //                        }
        //                        y--;
        //                    }
        //                    break;
        //                case 7:
        //                    y = (int)obj1.Position.Y;
        //                    x = (int)obj1.Position.X;

        //                    while (GetSlope(x, y, obj1.Position.X + (obj1.Rect.Width / 2), obj1.Position.Y + (obj1.Rect.Height / 2), true) >= endSlope)
        //                    {
        //                        //Color color1 = obj1.Data[(x - obj1.Rect.Left) + ((y - obj1.Rect.Top) * obj1.Rect.Width)];
        //                        Color color2 = obj2.Data[(x - obj2.Rect.Left) + ((y - obj2.Rect.Top) * obj2.Rect.Width)];

        //                        if (color2.R != 0)
        //                        {
        //                            obj1.ActiveOctants[i].Active = true;
        //                        }
        //                        else
        //                        {
        //                            obj1.ActiveOctants[i].Active = false;
        //                        }
        //                        y++;
        //                    }
        //                    break;
        //            }
        //        }
        //    }
        //}

        private static double GetSlope(double pX1, double pY1, double pX2, double pY2, bool pInvert)
        {
            if (pInvert)
                return (pY1 - pY2) / (pX1 - pX2);
            else
                return (pX1 - pX2) / (pY1 - pY2);
        }

        private Color[] GetImageData(Color[] colorData, int width, Rectangle rectangle)
        {
            Color[] color = new Color[rectangle.Width * rectangle.Height];
            for (int x = 0; x < rectangle.Width; x++)
            {
                for (int y = 0; y < rectangle.Height; y++)
                {
                    color[x + (y * rectangle.Width)] = colorData[x + rectangle.X + ((y + rectangle.Y) * width)];
                }
            }

            return color;
        }
    }
}

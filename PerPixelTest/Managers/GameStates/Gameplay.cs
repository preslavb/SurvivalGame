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

    public class Gameplay : IGameState
    {
        private static Vector2 collisionPosition;

        private bool paused;

        private Camera2D camera;
        private List<Layer> backgroundLayers;
        private List<Layer> foregroundLayers;

        private Player player;
        private Color[] currentSpriteData;

        private Sprite groundCollMap;
        private Sprite ground;

        private Texture2D pixel;
        public SpriteFont font;

        public bool Initialized { get; set; }

        public List<Texture2D> Textures { get; private set; }

        private enum CollisionType
        {
            ImpassableCollision,
            FloorCollision,
            NoCollision
        }

        public void Initialize(GraphicsDeviceManager graphics)
        {
            this.camera = new Camera2D(graphics.GraphicsDevice.Viewport);
            this.player = new Player();

            this.player.Initialize();

            this.player.LeftRestricted = false;
            this.player.RightRestricted = false;

            this.player.Acceleration = new Vector2(0.0f, 0.0f);

            this.paused = false;

            Initialized = true;
        }

        public void LoadContent(ContentManager Content, GraphicsDeviceManager graphics)
        {
            this.player.LoadContent(Content);

            this.backgroundLayers = new List<Layer>()
            {
                new Layer(this.camera) { Parallax = new Vector2(0.2f, 1.0f) },
                new Layer(this.camera) { Parallax = new Vector2(0.4f, 1.0f) },
                new Layer(this.camera) { Parallax = new Vector2(1.0f, 1.0f) }
            };

            this.foregroundLayers = new List<Layer>()
            {
                new Layer(this.camera) { Parallax = new Vector2(1.5f, 1.0f) }
            };

            // Draw And Position Background
            this.backgroundLayers[0].Sprites.Add(new Sprite(Content.Load<Texture2D>("sky")));
            this.backgroundLayers[1].Sprites.Add(new Sprite(Content.Load<Texture2D>("mountain")));

            this.backgroundLayers[0].Sprites[0].Position = this.backgroundLayers[0].ScreenToWorld(new Vector2(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Y));
            this.backgroundLayers[1].Sprites[0].Position = this.backgroundLayers[1].ScreenToWorld(new Vector2(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Y - 200));

            // Draw And Position Foreground
            this.foregroundLayers[0].Sprites.Add(new Sprite(Content.Load<Texture2D>("floorSprite")));

            this.foregroundLayers[0].Sprites[0].Position = this.foregroundLayers[0].ScreenToWorld(new Vector2(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Y + 700));

            // Load And Position Player Character
            this.player.Position = this.backgroundLayers[2].ScreenToWorld(new Vector2(-this.player.Rect.Height / 2, 0));
            this.player.Data = new Color[this.player.SpriteSheet.Width * this.player.SpriteSheet.Height];
            this.player.SpriteSheet.GetData(this.player.Data);

            // Load And Position Collision Map
            this.groundCollMap = new Sprite(Content.Load<Texture2D>("StartingArea Prot"));
            this.groundCollMap.Position = this.backgroundLayers[2].ScreenToWorld(new Vector2(0, 0)) + new Vector2(-this.groundCollMap.SpriteSheet.Width / 2, this.player.Position.Y + this.player.Rect.Height);
            this.groundCollMap.Rect = new Rectangle((int)this.groundCollMap.Position.X, (int)this.groundCollMap.Position.Y, this.groundCollMap.SpriteSheet.Width, this.groundCollMap.SpriteSheet.Height);

            // Load And Position Ground Texture
            this.ground = new Sprite(Content.Load<Texture2D>("floorSprite"));
            this.ground.Position = this.backgroundLayers[2].ScreenToWorld(new Vector2(0, 0)) + new Vector2(-this.groundCollMap.SpriteSheet.Width / 2, this.player.Position.Y + this.player.Rect.Height);
            this.groundCollMap.Data = new Color[this.groundCollMap.SpriteSheet.Width * this.groundCollMap.SpriteSheet.Height];
            this.groundCollMap.SpriteSheet.GetData(this.groundCollMap.Data);

            this.pixel = new Texture2D(graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            this.pixel.SetData(new[] { Color.White });

            font = Content.Load<SpriteFont>("DebugFont");
        }

        public void UnloadContent()
        {
        }

        public void Update(GraphicsDeviceManager graphics, GameTime gameTime)
        {
            if (!Initialized)
            {
                Initialize(graphics);
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

                if (!((Keyboard.GetState().IsKeyDown(Keys.D) && !this.player.RightRestricted) || (Keyboard.GetState().IsKeyDown(Keys.A) && !this.player.LeftRestricted)))
                {
                    this.player.PlayerAnimation.SpritesPositionY = 271;
                    this.player.PlayerAnimation.AmountOfFramesX = 5;
                    this.player.PlayerAnimation.ApplyChanges();
                }

                

                // Get Collision Data for Current Frame of Player Animation
                this.currentSpriteData = this.GetImageData(this.player.Data, this.player.SpriteSheet.Width, this.player.PlayerAnimation.Source);

                // Focus Camera on Player
                this.camera.Focus(this.player.Position);

                // Collision Detection Between Player and Ground
                CheckCollision();

                this.player.Update(gameTime);
                //this.player.Texture = new Texture2D(graphics.GraphicsDevice, player.Rect.Width, player.Rect.Height);
                //this.player.Texture.SetData(this.currentSpriteData);
            }

            // Debug Area
            ////Console.WriteLine(backgroundLayers[0].ScreenToWorld(new Vector2(0,0)));
            ////Console.WriteLine(this.player.PlayerAnimation.FrameWidth);
            for (int i = 0; i < InputHandler.PressedKeys.Count; i++)
            {
                //Console.WriteLine("Key: {0}, State: {1}", InputHandler.PressedKeys[i], InputHandler.PressedKeysStates[i]);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            foreach (Layer layer in this.backgroundLayers)
            {
                layer.Draw(spriteBatch);
            }

            spriteBatch.Begin(
                SpriteSortMode.BackToFront,
                BlendState.AlphaBlend,
                null,
                null,
                null,
                null,
                this.camera.GetTransformation(graphics.GraphicsDevice));

            ////this.spriteBatch.Draw(this.groundCollMap.Texture, this.groundCollMap.Position);
            spriteBatch.Draw(this.ground.SpriteSheet, this.ground.Position, Color.White);
            ////spriteBatch.Draw(player.Texture, player.Position);
            ////this.DrawBorder(player.Rect, 5, Color.Red);
            this.player.Draw(spriteBatch);
            if (paused)
            {
                this.DrawBorder(player.Rect, 5, Color.Red, spriteBatch);
            }
            spriteBatch.End();

            foreach (Layer layer in this.foregroundLayers)
            {
                layer.Draw(spriteBatch);
            }
            //DEBUG
            spriteBatch.Begin();
            spriteBatch.DrawString(font, String.Format("X: {0} Y: {1}, Jumped: {2}", (int)player.Position.X, (int)player.Position.Y, player.Jumped), new Vector2(70, 70), Color.White);
            spriteBatch.End();
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

        private void CheckCollision()
        {
            //if (IntersectsPixel(this.player, this.groundCollMap) == CollisionType.FloorCollision || IntersectsPixel(this.player, this.groundCollMap) == CollisionType.ImpassableCollision)
            //{
            //    if (IntersectsPixel(this.player, this.groundCollMap) == CollisionType.FloorCollision)
            //    {
            //        this.player.Position = new Vector2(player.Position.X, collisionPosition.Y - this.player.Rect.Height + 1);
            //        this.player.Jumped = false;
            //    }
            //    else if (IntersectsPixel(this.player, this.groundCollMap) == CollisionType.ImpassableCollision)
            //    {
            //        if (collisionPosition.X < this.player.Position.X + this.player.Rect.Width + 3 && collisionPosition.X > this.player.Position.X + (this.player.Rect.Width / 2))
            //        {
            //            this.player.RightRestricted = true;
            //        }
            //        else
            //        {
            //            this.player.RightRestricted = false;
            //        }
            //
            //        if (collisionPosition.X > this.player.Position.X - 3 && collisionPosition.X < this.player.Position.X + (this.player.Rect.Width / 2))
            //        {
            //            this.player.LeftRestricted = true;
            //        }
            //        else
            //        {
            //            this.player.LeftRestricted = false;
            //        }
            //    }
            //
            //    if (!(collisionPosition.X < this.player.Position.X + this.player.Rect.Width + 3 && collisionPosition.X > this.player.Position.X + (this.player.Rect.Width / 2)))
            //    {
            //        this.player.RightRestricted = false;
            //    }
            //
            //    if (!(collisionPosition.X > this.player.Position.X - 3 && collisionPosition.X < this.player.Position.X + (this.player.Rect.Width / 2)))
            //    {
            //        this.player.LeftRestricted = false;
            //    }
            //}
            //else
            //{
            //    this.player.Acceleration = new Vector2(player.Acceleration.X, player.Acceleration.Y + Player.GRAVITY);
            //}
            //if (this.player.ActiveOctants[4].Active || this.player.ActiveOctants[5].Active) 
            //{
            //    if (this.player.Acceleration.Y > 0)
            //    {
            //        this.player.Acceleration = new Vector2(this.player.Acceleration.X, 0);
            //    }
            //    
            //    
            //}
        }

        private void UpdateSplashScreen()
        {
            for (int i = 0; i < InputHandler.PressedKeys.Count; i++)
            {
                if ((InputHandler.PressedKeys[i] == Keys.P && InputHandler.PressedKeysStates[i] == InputHandler.KeyState.Clicked) && GameStateHandler.currentGameState is SplashScreen)
                {
                    GameStateHandler.currentGameState = new Gameplay();
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

        // DebugClass!!! REMOVE BEFORE RELEASE!!!
        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor, SpriteBatch spriteBatch)
        {
            // Draw top line
            spriteBatch.Draw(this.pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            spriteBatch.Draw(this.pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

            // Draw right line
            spriteBatch.Draw(
                    this.pixel,
                    new Rectangle(
                                  rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder,
                                  rectangleToDraw.Y,
                                  thicknessOfBorder,
                                  rectangleToDraw.Height),
                                  borderColor);

            // Draw bottom line
            spriteBatch.Draw(
                    this.pixel,
                    new Rectangle(
                                  rectangleToDraw.X,
                                  rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                  rectangleToDraw.Width,
                                  thicknessOfBorder),
                                  borderColor);
        }
    }
}

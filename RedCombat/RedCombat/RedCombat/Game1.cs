using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RedCombat
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D RSlide, GSlide, BSlide;
        Rectangle RRect, RRect2, GRect, GRect2, BRect, BRect2;

        Rectangle Plane1, Plane2, Left1, Left2, Right1, Right2;
        Texture2D Plane1Text, Plane2Text, Left, Right;

        Rectangle Player1, Player2;
        Texture2D Player1Text, Player2Text;

        Rectangle Random1, Random2;
        Texture2D Random1Text;

        Rectangle Ready1, Ready2;
        Texture2D Ready1Text;

        Texture2D[] PlanesTexts = new Texture2D[3];

        Color Team1, Team2;

        Rectangle Team1Select, Team2Select;
        Texture2D TeamSelect;

        Rectangle R1Slider, R2Slider, G1Slider, G2Slider, B1Slider, B2Slider;
        //Texture2D Slider;

        enum PlaneMenu { Plane, Red, Green, Blue, Random, Ready};

        enum MenuScreen { start, plane, map, game, pause, gameend, options};
        enum PlaneSelect { Fighter, Stealth, Bomber};

        MenuScreen menuselect = MenuScreen.start;
        PlaneSelect currentplane1 = PlaneSelect.Fighter;
        PlaneSelect currentplane2 = PlaneSelect.Fighter;
        PlaneMenu Team1Menu = PlaneMenu.Plane;
        PlaneMenu Team2Menu = PlaneMenu.Plane;

        GamePadState oldgPlayer1Plane = GamePad.GetState(PlayerIndex.One);
        GamePadState oldgPlayer2Plane = GamePad.GetState(PlayerIndex.Two);

        int T1R, T1G, T1B, T2R, T2G, T2B;

        Texture2D ColorSlider;

        Color Ready1Color, Ready2Color;

        bool Ready1Bool, Ready2Bool;

        Texture2D StartLogo;
        SpriteFont Font1;
        Rectangle StartRect;

        Texture2D Bullet;

        Plane Player1Plane, Player2Plane;

        int Plane1Width, Plane1Height, Plane1Speed, Plane1Reload, Plane1Bullets;
        int Plane2Width, Plane2Height, Plane2Speed, Plane2Reload, Plane2Bullets;

        Cloud[] clouds;
        int CLOUD_AMOUNT;
        Texture2D cloudText;
        Texture2D heartText;
        Random rand = new Random();
        Color[] mapColors = { Color.SkyBlue, Color.DarkOrange, Color.MidnightBlue, new Color(220, 220, 220) };
        int mapColor;
        bool toggleClouds;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 720;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            RRect = new Rectangle(135, 290, 255, 60);
            GRect = new Rectangle(135, 360, 255, 60);
            BRect = new Rectangle(135, 430, 255, 60);
            Plane1 = new Rectangle(203, 120, 120, 120);
            Left1 = new Rectangle(135, 120, 67, 120);
            Right1 = new Rectangle(323, 120, 67, 120);
            Player1 = new Rectangle(135, 0, 255, 60);
            Random1 = new Rectangle(160, 540, 205, 60);
            Ready1 = new Rectangle(155, 620, 205, 60);

            T1R = 255;
            T1G = 255;
            T1B = 255;

            RRect2 = new Rectangle(690, 290, 255, 60);
            GRect2 = new Rectangle(690, 360, 255, 60);
            BRect2 = new Rectangle(690, 430, 255, 60);
            Plane2 = new Rectangle(758, 120, 120, 120);
            Left2 = new Rectangle(690, 120, 67, 120);
            Right2 = new Rectangle(878, 120, 67, 120);
            Player2 = new Rectangle(690, 0, 255, 60);
            Random2 = new Rectangle(715, 540, 205, 60);
            Ready2 = new Rectangle(710, 620, 205, 60);

            T2R = 255;
            T2G = 255;
            T2B = 255;

            R1Slider = new Rectangle(135 + T1R, 290, 5, 60);
            G1Slider = new Rectangle(135 + T1G, 360, 5, 60);
            B1Slider = new Rectangle(135 + T1B, 430, 5, 60);
            R2Slider = new Rectangle(690 + T2R, 290, 5, 60);
            G2Slider = new Rectangle(690 + T2G, 360, 5, 60);
            B2Slider = new Rectangle(690 + T2B, 430, 5, 60);

            Ready1Color = Color.Black;
            Ready2Color = Color.Black;

            Ready1Bool = false;
            Ready2Bool = false;

            StartRect = new Rectangle(340, 25, 400, 600);

            CLOUD_AMOUNT = 4;
            clouds = new Cloud[CLOUD_AMOUNT];
            mapColor = 0;
            toggleClouds = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            PlanesTexts[0] = Content.Load<Texture2D>("Fighter");
            PlanesTexts[1] = Content.Load<Texture2D>("Stealth");
            PlanesTexts[2] = Content.Load<Texture2D>("Bomber");

            RSlide = Content.Load<Texture2D>("R Slider");
            BSlide = Content.Load<Texture2D>("B Slider");
            GSlide = Content.Load<Texture2D>("G Slider");

            Plane1Text = PlanesTexts[(int)currentplane1];
            Plane2Text = PlanesTexts[(int)currentplane2];

            Random1Text = Content.Load<Texture2D>("RC Random");
            Ready1Text = Content.Load<Texture2D>("RC Ready");

            Player1Text = Content.Load<Texture2D>("RC Player1");
            Player2Text = Content.Load<Texture2D>("RC Player2");

            Left = Content.Load<Texture2D>("RC Left");
            Right = Content.Load<Texture2D>("RC Right");

            TeamSelect = Content.Load<Texture2D>("Selecter");

            ColorSlider = Content.Load<Texture2D>("ColorSlider");

            StartLogo = Content.Load<Texture2D>("Logo");
            Font1 = Content.Load<SpriteFont>("SpriteFont1");

            Bullet = Content.Load<Texture2D>("Ammo");

            cloudText = Content.Load<Texture2D>("Cloud");

            for(int i = 0; i <clouds.Length; i++)
            {
                clouds[i] = new Cloud(cloudText, new Rectangle(rand.Next(0, 1080), rand.Next(0, 720), rand.Next(200, 300), rand.Next(100, 150)));
            }


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            GamePadState gPlayer1Plane = GamePad.GetState(PlayerIndex.One);
            GamePadState gPlayer2Plane = GamePad.GetState(PlayerIndex.Two);

            if (Ready1Bool == false)
            {
                if (gPlayer1Plane.DPad.Up == ButtonState.Pressed && oldgPlayer1Plane.DPad.Up == ButtonState.Released && (int)Team1Menu > 0)
                {
                    Team1Menu--;
                }

                if (gPlayer1Plane.DPad.Down == ButtonState.Pressed && oldgPlayer1Plane.DPad.Down == ButtonState.Released && (int)Team1Menu < 5)
                {
                    Team1Menu++;
                }
            }

            if (Ready2Bool == false)
            {
                if (gPlayer2Plane.DPad.Up == ButtonState.Pressed && oldgPlayer2Plane.DPad.Up == ButtonState.Released && (int)Team2Menu > 0)
                {
                    Team2Menu--;
                }

                if (gPlayer2Plane.DPad.Down == ButtonState.Pressed && oldgPlayer2Plane.DPad.Down == ButtonState.Released && (int)Team2Menu < 5)
                {
                    Team2Menu++;
                }
            }

            switch (menuselect)
            {

                case ((MenuScreen)0):
                    {
                        if(gPlayer1Plane.Buttons.Start == ButtonState.Pressed || gPlayer2Plane.Buttons.Start == ButtonState.Pressed)
                        {
                            menuselect = MenuScreen.plane;
                            Initialize();
                            Team1Menu = PlaneMenu.Plane;
                            Team2Menu = PlaneMenu.Plane;
                        }
                    }
                    break;

                case ((MenuScreen)1):
                    {

                        if (((gPlayer1Plane.Buttons.B == ButtonState.Pressed && oldgPlayer1Plane.Buttons.B == ButtonState.Released) || (gPlayer2Plane.Buttons.B == ButtonState.Pressed && oldgPlayer2Plane.Buttons.B == ButtonState.Released)) && Ready1Bool == false && Ready2Bool == false)
                        {
                            menuselect--;
                        }

                        switch (Team1Menu)
                        {

                            case (PlaneMenu)0:

                                Team1Select = new Rectangle(130, 115, 265, 130);

                                if (gPlayer1Plane.DPad.Right == ButtonState.Pressed && oldgPlayer1Plane.DPad.Right == ButtonState.Released)
                                {
                                    currentplane1 += 1;
                                }

                                if (gPlayer1Plane.DPad.Left == ButtonState.Pressed && oldgPlayer1Plane.DPad.Left == ButtonState.Released)
                                {
                                    currentplane1 -= 1;
                                }

                                if ((int)currentplane1 > 2)
                                {
                                    currentplane1 = (PlaneSelect)0;
                                }

                                if ((int)currentplane1 < 0)
                                {
                                    currentplane1 = (PlaneSelect)2;
                                }

                                break;

                            case (PlaneMenu)1:
                                Team1Select = new Rectangle(130, 285, 265, 70);

                                if (gPlayer1Plane.ThumbSticks.Left.X > 0 && T1R < 255)
                                {
                                    T1R++;
                                }

                                if (gPlayer1Plane.ThumbSticks.Left.X < 0 && T1R > 0)
                                {
                                    T1R--;
                                }

                                break;

                            case (PlaneMenu)2:
                                Team1Select = new Rectangle(130, 355, 265, 70);

                                if (gPlayer1Plane.ThumbSticks.Left.X > 0 && T1G < 255)
                                {
                                    T1G++;
                                }

                                if (gPlayer1Plane.ThumbSticks.Left.X < 0 && T1G > 0)
                                {
                                    T1G--;
                                }

                                break;

                            case (PlaneMenu)3:
                                Team1Select = new Rectangle(130, 425, 265, 70);

                                if (gPlayer1Plane.ThumbSticks.Left.X > 0 && T1B < 255)
                                {
                                    T1B++;
                                }

                                if (gPlayer1Plane.ThumbSticks.Left.X < 0 && T1B > 0)
                                {
                                    T1B--;
                                }

                                break;

                            case (PlaneMenu)4:
                                Team1Select = new Rectangle(155, 535, 215, 70);

                                if (gPlayer1Plane.Buttons.A == ButtonState.Pressed && oldgPlayer1Plane.Buttons.A == ButtonState.Released)
                                {
                                    Random rand = new Random();

                                    T1R = rand.Next(256);
                                    T1G = rand.Next(256);
                                    T1B = rand.Next(256);
                                }

                                break;

                            case (PlaneMenu)5:
                                Team1Select = new Rectangle(150, 615, 215, 70);

                                if (gPlayer1Plane.Buttons.A == ButtonState.Pressed)
                                {
                                    Ready1Bool = true;
                                    Ready1Color = Team1;
                                    Player1Plane = new Plane(PlanesTexts[(int)currentplane1], new Rectangle(100, 100, Plane1Width, Plane1Height), Team1, new Vector2(Plane1Speed, Plane1Speed), Plane1Reload, Plane1Bullets, Bullet, PlayerIndex.One);
                                }

                                break;

                            default:
                                Console.WriteLine("Error 42069 Ryan's Code is Bad");
                                break;
                        }

                        if(Ready1Bool == true && gPlayer1Plane.Buttons.B == ButtonState.Pressed)
                        {
                            Ready1Bool = false;
                            Ready1Color = Color.Black;
                        }

                        switch (currentplane1)
                        {

                            case ((PlaneSelect)0):
                                {
                                    Plane1Width = 50;
                                    Plane1Height = 50;
                                    Plane1Speed = 7;
                                    Plane1Reload = 60;
                                    Plane1Bullets = 1;
                                }
                                break;

                            case ((PlaneSelect)1):
                                {
                                    Plane1Width = 60;
                                    Plane1Height = 60;
                                    Plane1Speed = 6;
                                    Plane1Reload = 60;
                                    Plane1Bullets = 1;
                                }
                                break;

                            case ((PlaneSelect)2):
                                {
                                    Plane1Width = 70;
                                    Plane1Height = 70;
                                    Plane1Speed = 5;
                                    Plane1Reload = 60;
                                    Plane1Bullets = 2;
                                }
                                break;

                            default:
                                break;
                        }

                        switch (Team2Menu)
                        {
                            case (PlaneMenu)0:
                                Team2Select = new Rectangle(685, 115, 265, 130);

                                if (gPlayer2Plane.DPad.Right == ButtonState.Pressed && oldgPlayer2Plane.DPad.Right == ButtonState.Released)
                                {
                                    currentplane2 += 1;
                                }

                                if (gPlayer2Plane.DPad.Left == ButtonState.Pressed && oldgPlayer2Plane.DPad.Left == ButtonState.Released)
                                {
                                    currentplane2 -= 1;
                                }

                                if ((int)currentplane2 > 2)
                                {
                                    currentplane2 = (PlaneSelect)0;
                                }

                                if ((int)currentplane2 < 0)
                                {
                                    currentplane2 = (PlaneSelect)2;
                                }

                                break;


                            case (PlaneMenu)1:
                                Team2Select = new Rectangle(685, 285, 265, 70);

                                if (gPlayer2Plane.ThumbSticks.Left.X > 0 && T2R < 255)
                                {
                                    T2R++;
                                }

                                if (gPlayer2Plane.ThumbSticks.Left.X < 0 && T2R > 0)
                                {
                                    T2R--;
                                }

                                break;

                            case (PlaneMenu)2:
                                Team2Select = new Rectangle(685, 355, 265, 70);

                                if (gPlayer2Plane.ThumbSticks.Left.X > 0 && T2G < 255)
                                {
                                    T2G++;
                                }

                                if (gPlayer2Plane.ThumbSticks.Left.X < 0 && T2G > 0)
                                {
                                    T2G--;
                                }

                                break;

                            case (PlaneMenu)3:
                                Team2Select = new Rectangle(685, 425, 265, 70);

                                if (gPlayer2Plane.ThumbSticks.Left.X > 0 && T2B < 255)
                                {
                                    T2B++;
                                }

                                if (gPlayer2Plane.ThumbSticks.Left.X < 0 && T2B > 0)
                                {
                                    T2B--;
                                }

                                break;

                            case (PlaneMenu)4:
                                Team2Select = new Rectangle(710, 535, 215, 70);

                                if (gPlayer2Plane.Buttons.A == ButtonState.Pressed && oldgPlayer2Plane.Buttons.A == ButtonState.Released)
                                {
                                    Random rand = new Random();

                                    T2R = rand.Next(256);
                                    T2G = rand.Next(256);
                                    T2B = rand.Next(256);
                                }

                                break;

                            case (PlaneMenu)5:
                                Team2Select = new Rectangle(705, 615, 215, 70);

                                if (gPlayer2Plane.Buttons.A == ButtonState.Pressed)
                                {
                                    Ready2Bool = true;
                                    Ready2Color = Team2;
                                    Player2Plane = new Plane(PlanesTexts[(int)currentplane2], new Rectangle(100, 100, Plane2Width, Plane2Height), Team2, new Vector2(Plane2Speed, Plane2Speed), Plane2Reload, Plane2Bullets, Bullet, PlayerIndex.Two);

                                }
                                break;

                            default:
                                Console.WriteLine("Error 42069 Ryan's Code is Bad");
                                break;
                        }

                        switch (currentplane2)
                        {

                            case ((PlaneSelect)0):
                                {
                                    Plane2Width = 50;
                                    Plane2Height = 50;
                                    Plane2Speed = 7;
                                    Plane2Reload = 60;
                                    Plane2Bullets = 1;
                                }
                                break;

                            case ((PlaneSelect)1):
                                {
                                    Plane2Width = 60;
                                    Plane2Height = 60;
                                    Plane2Speed = 6;
                                    Plane2Reload = 60;
                                    Plane2Bullets = 1;
                                }
                                break;

                            case ((PlaneSelect)2):
                                {
                                    Plane2Width = 70;
                                    Plane2Height = 70;
                                    Plane2Speed = 5;
                                    Plane2Reload = 60;
                                    Plane2Bullets = 2;
                                }
                                break;

                            default:
                                break;
                        }

                        if (Ready2Bool == true && gPlayer2Plane.Buttons.B == ButtonState.Pressed)
                        {
                            Ready2Bool = false;
                            Ready2Color = Color.Black;
                        }

                        if (Ready1Bool == true && Ready2Bool == true)
                        {
                            menuselect = MenuScreen.game;
                        }
                    }
                    break;


                case ((MenuScreen)2):
                    {


                    }
                    break;

                case ((MenuScreen)3):
                    {
                        if (Player1Plane != null)
                        {
                            Player1Plane.Update();
                        }
                        if (Player2Plane != null)
                        {
                            Player2Plane.Update();
                        }

                        for (int i = 0; i < Player1Plane.bullets.Count; i++)
                        {
                            if (Player1Plane.bullets[i].rect().Intersects(Player2Plane.rect()))
                            {
                                Player2Plane.lives--;
                                if (!Player2Plane.isInvulnerable)
                                    Player2Plane.isDead = true;
                                Player1Plane.bullets.Remove(Player1Plane.bullets[i]);
                            }
                        }
                        for (int i = 0; i < Player2Plane.bullets.Count; i++)
                        {
                            if (Player2Plane.bullets[i].rect().Intersects(Player1Plane.rect()))
                            {
                                Player1Plane.lives--;
                                if (!Player1Plane.isInvulnerable)
                                    Player1Plane.isDead = true;
                                Player2Plane.bullets.Remove(Player2Plane.bullets[i]);
                            }
                        }

                        for (int i = 0; i < clouds.Length; i++)
                        {
                            clouds[i].CloudUpdate();
                        }


                    }
                    break;

                default:
                    Console.WriteLine("God Dammit");
                    break;
            }

            

            oldgPlayer1Plane = gPlayer1Plane;
            oldgPlayer2Plane = gPlayer2Plane;

            Team1 = new Color(T1R, T1G, T1B);
            Team2 = new Color(T2R, T2G, T2B);

            R1Slider.X = 135 + T1R;
            G1Slider.X = 135 + T1G;
            B1Slider.X = 135 + T1B;

            R2Slider.X = 690 + T2R;
            G2Slider.X = 690 + T2G;
            B2Slider.X = 690 + T2B;
            

            Plane1Text = PlanesTexts[(int)currentplane1];
            Plane2Text = PlanesTexts[(int)currentplane2];


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            // TODO: Add your drawing code here


            switch (menuselect)
            {

                case ((MenuScreen)0):
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(StartLogo, StartRect, Color.White);
                        spriteBatch.DrawString(Font1, "Press Start To Begin", new Vector2(420, 680), Color.White);
                        spriteBatch.End();
                    }
                    break;

                case ((MenuScreen)1):
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(Player1Text, Player1, Team1);
                        spriteBatch.Draw(Plane1Text, Plane1, Team1);
                        spriteBatch.Draw(Left, Left1, Color.Black);
                        spriteBatch.Draw(Right, Right1, Color.Black);
                        spriteBatch.Draw(RSlide, RRect, Color.White);
                        spriteBatch.Draw(GSlide, GRect, Color.White);
                        spriteBatch.Draw(BSlide, BRect, Color.White);
                        spriteBatch.Draw(Random1Text, Random1, Color.Black);
                        spriteBatch.Draw(Ready1Text, Ready1, Ready1Color);

                        spriteBatch.Draw(Player2Text, Player2, Team2);
                        spriteBatch.Draw(Plane2Text, Plane2, Team2);
                        spriteBatch.Draw(Left, Left2, Color.Black);
                        spriteBatch.Draw(Right, Right2, Color.Black);
                        spriteBatch.Draw(RSlide, RRect2, Color.White);
                        spriteBatch.Draw(GSlide, GRect2, Color.White);
                        spriteBatch.Draw(BSlide, BRect2, Color.White);
                        spriteBatch.Draw(Random1Text, Random2, Color.Black);
                        spriteBatch.Draw(Ready1Text, Ready2, Ready2Color);

                        spriteBatch.Draw(TeamSelect, Team1Select, Color.White);
                        spriteBatch.Draw(TeamSelect, Team2Select, Color.White);

                        spriteBatch.Draw(ColorSlider, R1Slider, Color.White);
                        spriteBatch.Draw(ColorSlider, G1Slider, Color.White);
                        spriteBatch.Draw(ColorSlider, B1Slider, Color.White);
                        spriteBatch.Draw(ColorSlider, R2Slider, Color.White);
                        spriteBatch.Draw(ColorSlider, G2Slider, Color.White);
                        spriteBatch.Draw(ColorSlider, B2Slider, Color.White);

                        spriteBatch.End();
                    }
                    break;

                case ((MenuScreen)2):
                    {

                    }
                    break;

                case ((MenuScreen)3):
                    {
                        spriteBatch.Begin();
                        Player1Plane.Draw(spriteBatch);
                        Player2Plane.Draw(spriteBatch);
                        if (toggleClouds)
                        {
                            for(int i = 0; i < clouds.Length; i++)
                            {
                                spriteBatch.Draw(clouds[i].CloudText, clouds[i].CloudRect, Color.White);

                            }
                        }
                        spriteBatch.End();
                    }
                    break;

                default:
                    Console.WriteLine("Ok This Doesnt Work");
                    break;

            }

            

            base.Draw(gameTime);
        }
    }
}

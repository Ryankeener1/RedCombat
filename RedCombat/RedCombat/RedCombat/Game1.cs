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
        Rectangle RRect, RRect2, GRect, GRect2, BRect, BRect2, SelectSlide, SelectSlide2;

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
        Texture2D Slider;

        enum PlaneMenu { Plane, Red, Green, Blue, Random, Ready};

        enum MenuScreen { start, plane, options, game, pause, gameend};
        enum PlaneSelect { Fighter, Stealth, Bomber};

        MenuScreen menuselect = MenuScreen.start;
        PlaneSelect currentplane1 = PlaneSelect.Fighter;
        PlaneSelect currentplane2 = PlaneSelect.Fighter;
        PlaneMenu Team1Menu = PlaneMenu.Plane;
        PlaneMenu Team2Menu = PlaneMenu.Plane;

        GamePadState oldgp1 = GamePad.GetState(PlayerIndex.One);
        GamePadState oldgp2 = GamePad.GetState(PlayerIndex.Two);

        int T1R, T1G, T1B, T2R, T2G, T2B;

        Texture2D ColorSlider;

        Color Ready1Color, Ready2Color;

        bool Ready1Bool, Ready2Bool;

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

            GamePadState gp1 = GamePad.GetState(PlayerIndex.One);
            GamePadState gp2 = GamePad.GetState(PlayerIndex.Two);

            if (Ready1Bool == false)
            {
                if (gp1.DPad.Up == ButtonState.Pressed && oldgp1.DPad.Up == ButtonState.Released && (int)Team1Menu > 0)
                {
                    Team1Menu--;
                }

                if (gp1.DPad.Down == ButtonState.Pressed && oldgp1.DPad.Down == ButtonState.Released && (int)Team1Menu < 5)
                {
                    Team1Menu++;
                }
            }

            if (Ready2Bool == false)
            {
                if (gp2.DPad.Up == ButtonState.Pressed && oldgp2.DPad.Up == ButtonState.Released && (int)Team2Menu > 0)
                {
                    Team2Menu--;
                }

                if (gp2.DPad.Down == ButtonState.Pressed && oldgp2.DPad.Down == ButtonState.Released && (int)Team2Menu < 5)
                {
                    Team2Menu++;
                }
            }

            switch (menuselect)
            {

                case ((MenuScreen)0):
                    {
                        if(gp1.Buttons.Start == ButtonState.Pressed)
                        {
                            menuselect = MenuScreen.plane;
                        }
                    }
                    break;

                case ((MenuScreen)1):
                    {
                        switch (Team1Menu)
                        {
                            case (PlaneMenu)0:
                                Team1Select = new Rectangle(130, 115, 265, 130);

                                if (gp1.DPad.Right == ButtonState.Pressed && oldgp1.DPad.Right == ButtonState.Released)
                                {
                                    currentplane1 += 1;
                                }

                                if (gp1.DPad.Left == ButtonState.Pressed && oldgp1.DPad.Left == ButtonState.Released)
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

                                if (gp1.ThumbSticks.Left.X > 0 && T1R < 255)
                                {
                                    T1R++;
                                }

                                if (gp1.ThumbSticks.Left.X < 0 && T1R > 0)
                                {
                                    T1R--;
                                }

                                break;

                            case (PlaneMenu)2:
                                Team1Select = new Rectangle(130, 355, 265, 70);

                                if (gp1.ThumbSticks.Left.X > 0 && T1G < 255)
                                {
                                    T1G++;
                                }

                                if (gp1.ThumbSticks.Left.X < 0 && T1G > 0)
                                {
                                    T1G--;
                                }

                                break;

                            case (PlaneMenu)3:
                                Team1Select = new Rectangle(130, 425, 265, 70);

                                if (gp1.ThumbSticks.Left.X > 0 && T1B < 255)
                                {
                                    T1B++;
                                }

                                if (gp1.ThumbSticks.Left.X < 0 && T1B > 0)
                                {
                                    T1B--;
                                }

                                break;

                            case (PlaneMenu)4:
                                Team1Select = new Rectangle(155, 535, 215, 70);

                                if (gp1.Buttons.A == ButtonState.Pressed && oldgp1.Buttons.A == ButtonState.Released)
                                {
                                    Random rand = new Random();

                                    T1R = rand.Next(256);
                                    T1G = rand.Next(256);
                                    T1B = rand.Next(256);
                                }

                                break;

                            case (PlaneMenu)5:
                                Team1Select = new Rectangle(150, 615, 215, 70);

                                if (gp1.Buttons.A == ButtonState.Pressed)
                                {
                                    Ready1Bool = true;
                                    Ready1Color = Team1;
                                }

                                break;

                            default:
                                Console.WriteLine("Error 42069 Ryan's Code is Bad");
                                break;
                        }

                        if(Ready1Bool == true && gp1.Buttons.B == ButtonState.Pressed)
                        {
                            Ready1Bool = false;
                            Ready1Color = Color.Black;
                        }

                        switch (Team2Menu)
                        {
                            case (PlaneMenu)0:
                                Team2Select = new Rectangle(685, 115, 265, 130);

                                if (gp2.DPad.Right == ButtonState.Pressed && oldgp2.DPad.Right == ButtonState.Released)
                                {
                                    currentplane2 += 1;
                                }

                                if (gp2.DPad.Left == ButtonState.Pressed && oldgp2.DPad.Left == ButtonState.Released)
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

                                if (gp2.ThumbSticks.Left.X > 0 && T2R < 255)
                                {
                                    T2R++;
                                }

                                if (gp2.ThumbSticks.Left.X < 0 && T2R > 0)
                                {
                                    T2R--;
                                }

                                break;

                            case (PlaneMenu)2:
                                Team2Select = new Rectangle(685, 355, 265, 70);

                                if (gp2.ThumbSticks.Left.X > 0 && T2G < 255)
                                {
                                    T2G++;
                                }

                                if (gp2.ThumbSticks.Left.X < 0 && T2G > 0)
                                {
                                    T2G--;
                                }

                                break;

                            case (PlaneMenu)3:
                                Team2Select = new Rectangle(685, 425, 265, 70);

                                if (gp2.ThumbSticks.Left.X > 0 && T2B < 255)
                                {
                                    T2B++;
                                }

                                if (gp2.ThumbSticks.Left.X < 0 && T2B > 0)
                                {
                                    T2B--;
                                }

                                break;

                            case (PlaneMenu)4:
                                Team2Select = new Rectangle(710, 535, 215, 70);

                                if (gp2.Buttons.A == ButtonState.Pressed && oldgp2.Buttons.A == ButtonState.Released)
                                {
                                    Random rand = new Random();

                                    T2R = rand.Next(256);
                                    T2G = rand.Next(256);
                                    T2B = rand.Next(256);
                                }

                                break;

                            case (PlaneMenu)5:
                                Team2Select = new Rectangle(705, 615, 215, 70);

                                if (gp2.Buttons.A == ButtonState.Pressed)
                                {
                                    Ready2Bool = true;
                                    Ready2Color = Team2;
                                }
                                break;

                            default:
                                Console.WriteLine("Error 42069 Ryan's Code is Bad");
                                break;
                        }

                        if (Ready2Bool == true && gp2.Buttons.B == ButtonState.Pressed)
                        {
                            Ready2Bool = false;
                            Ready2Color = Color.Black;
                        }
                    }
                    break;

                default:
                    Console.WriteLine("God Dammit");
                    break;
            }

            

            oldgp1 = gp1;
            oldgp2 = gp2;

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

                default:
                    Console.WriteLine("Ok This Doesnt Work");
                    break;

            }

            

            base.Draw(gameTime);
        }
    }
}

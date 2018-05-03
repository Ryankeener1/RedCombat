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
        Cloud[] clouds;
        int CLOUD_AMOUNT;
        Texture2D cloudText;
        Random rand = new Random();
        Color[] mapColors = { Color.SkyBlue, Color.DarkOrange, Color.MidnightBlue, new Color(220,220,220) };
        int mapColor;
        Rectangle background;
        Texture2D blank;
        bool toggleClouds;
        GamePadState gamepad = GamePad.GetState(PlayerIndex.One);
        Plane p1;
        int i = 0;

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
            CLOUD_AMOUNT = 3;
            clouds = new Cloud[CLOUD_AMOUNT];
            mapColor = 0; //button
            background = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            toggleClouds = true; //button
            Texture2D text = Content.Load<Texture2D>("Fighter");
            Texture2D text1 = Content.Load<Texture2D>("Bomber");
            Texture2D text2 = Content.Load<Texture2D>("Stealth");
            Texture2D bull = Content.Load<Texture2D>("Ammo");
            p1 = new Plane(text, new Rectangle(100, 100, 50, 50), Color.White, new Vector2(5, 5), 5, 5, bull, PlayerIndex.One);
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
            cloudText = Content.Load<Texture2D>("Cloud");
            blank = Content.Load<Texture2D>("Blank");
            for (int i = 0; i < clouds.Length; i++)
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
            for (int i = 0; i < clouds.Length; i++)
            {
                clouds[i].CloudUpdate();
            }
            i++;
            if (p1 != null)
            {
                p1.Update();
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            spriteBatch.Draw(blank, background, mapColors[mapColor]);
            if (toggleClouds)
            for (int i = 0; i < clouds.Length; i++)
            {
                spriteBatch.Draw(clouds[i].CloudText, clouds[i].CloudRect, Color.White);
            }
            p1.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

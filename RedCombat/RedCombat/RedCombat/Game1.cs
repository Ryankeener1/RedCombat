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
        GamePadState gamepad = GamePad.GetState(PlayerIndex.One);
        Plane p1, p2, p3;

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
            Texture2D text = Content.Load<Texture2D>("Fighter");
            Texture2D text1 = Content.Load<Texture2D>("Bomber");
            Texture2D text2 = Content.Load<Texture2D>("Stealth");
            Texture2D bull = Content.Load<Texture2D>("Ammo");

            p1 = new Plane(text1, new Rectangle(100, 100, 70, 70), Color.Blue, new Vector2(3, 3), 60, 2, bull, PlayerIndex.One); //Bomber
            //p3 = new Plane(text, new Rectangle(100, 200, 50, 50), Color.Red, new Vector2(5, 5), 60, 1, bull, PlayerIndex.One); //Fighter
            p2 = new Plane(text2, new Rectangle(100, 300, 60, 60), Color.Green, new Vector2(4, 4), 60, 1, bull, PlayerIndex.Two); //Stealth
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
            i++;
            if (p1 != null)
            {
                p1.Update();
            }
            if (p2 != null)
            {
                p2.Update();
            }

            for (int i = 0; i < p1.bullets.Count; i++)
            {
                if (p1.bullets[i].rect().Intersects(p2.rect()))
                {
                    p2.lives--;
                    if (!p2.isInvulnerable)
                        p2.isDead = true;
                    p1.bullets.Remove(p1.bullets[i]);
                }
            }
            for (int i = 0; i < p2.bullets.Count; i++)
            {
                if (p2.bullets[i].rect().Intersects(p1.rect()))
                {
                    p1.lives--;
                    if (!p1.isInvulnerable)
                        p1.isDead = true;
                    p2.bullets.Remove(p2.bullets[i]);
                }
            }
            // TODO: Add your update logic here

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
            spriteBatch.Begin();
            p1.Draw(spriteBatch);
            p2.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

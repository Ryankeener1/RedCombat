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
    class Plane
    {
        GamePadState gs = GamePad.GetState(PlayerIndex.One);
        PlayerIndex Index = 0;
        public Texture2D Text2D;

        public Rectangle Rect;

        Vector2 Velocity;
        Vector2 vel;

        int ReloadTime = 0;
        int MAXReloadTime;
        int NumOfBulltes;
        int RespawnTime = 0;
        int InvulnerableTime = 0;
        public Color PlaneColor;
        public int rotation;

        public bool Reloading = false;
        public bool isDead = false;
        public bool isInvulnerable = true;

        List<Bullet> bullets = new List<Bullet>();

        Texture2D bulletTex;


        public Plane(Texture2D t, Rectangle r, Color c, Vector2 v, int reloadT, int BulletNum, Texture2D bulletT, PlayerIndex g)
        {
            Text2D = t;
            Rect = r;
            PlaneColor = c;
            Velocity = v;
            MAXReloadTime = reloadT;
            NumOfBulltes = BulletNum;
            bulletTex = bulletT;
            Index = g;
        }

        public void Update()
        {
            gs = GamePad.GetState(Index);
            //Shooting
            if (gs.Triggers.Right == 1)
                Shoot();



            //Reloading
            if (Reloading)
            {
                ReloadTime++;
                if (ReloadTime >= MAXReloadTime)
                {
                    Reloading = false;


                }
            }

            //DEAD
            if (isDead)
            {
                RespawnTime++;
                if (RespawnTime >= 120)
                {
                    isDead = false;
                    InvulnerableTime = 0;
                    isInvulnerable = true;
                }
            }

            //Invulnerable
            if (isInvulnerable)
            {
                InvulnerableTime++;
                if (InvulnerableTime >= 120)
                {
                    isInvulnerable = false;

                }
            }




            if (((rotation + 5 * (gs.ThumbSticks.Right.X)) % 360) >= 0)
            {
                rotation = (int)((rotation + 5 * (gs.ThumbSticks.Right.X)) % 360);
            }
            else
            {
                rotation = 360;
            }



            Rect.X += (int)(Velocity.X * (Math.Cos(rotation * (Math.PI / 180))));
            Rect.Y += (int)(Velocity.Y * (Math.Sin(rotation * (Math.PI / 180))));

            if (Rect.X > 1105)
                Rect.X = -25;
            if (Rect.X < -25)
                Rect.X = 1080;
            if (Rect.Y > 745)
                Rect.Y = -25;
            if (Rect.Y < -25)
                Rect.Y = 720;
        }



        public void Shoot()
        {
            if (!Reloading)
            {
                double rot = rotation * (Math.PI / 180);
                bullets.Add(new Bullet(bulletTex, new Rectangle(Rect.X + (int)(25 * Math.Cos(rot)), Rect.Y + (int)(25 * Math.Sin(rot)), 20, 10), PlaneColor, rot, 8));
                ReloadTime = 0;
                Reloading = true;
            }
        }

        public void IsShot()
        {
            isDead = true;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet b in bullets)
            {
                b.Update();
                b.Draw(spriteBatch);
            }
            spriteBatch.Draw(Text2D, Rect, null, PlaneColor, (float)(rotation * (Math.PI / 180)), new Vector2(Text2D.Width / 2, Text2D.Height / 2), SpriteEffects.None, 0);
        }


    }
}
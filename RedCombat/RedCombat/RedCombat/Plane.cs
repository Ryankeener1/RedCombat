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

        Rectangle Rect;

        Vector2 Velocity;
        //Vector2 vel;

        int ReloadTime = 0;
        int MAXReloadTime;
        int NumOfBullets;
        int RespawnTime = 0;
        int InvulnerableTime = 0;
        public Color PlaneColor;
        Color temp;
        public int rotation;

        public bool Reloading = false;
        public bool isDead = false;
        public bool isInvulnerable = true;
        public int lives;

        public List<Bullet> bullets = new List<Bullet>();

        Texture2D bulletTex;

        int count = 0;

        public Plane(Texture2D t, Rectangle r, Color c, Vector2 v, int reloadT, int BulletNum, Texture2D bulletT, PlayerIndex g)
        {
            Text2D = t;
            Rect = r;
            PlaneColor = c;
            Velocity = v;
            MAXReloadTime = reloadT;
            NumOfBullets = BulletNum;
            bulletTex = bulletT;
            Index = g;
            lives = 3;
            temp = Color.Transparent;
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

            //Shot



            //DEAD
            if (isDead && !isInvulnerable)
            {
                if (RespawnTime == 0)
                {
                    temp = PlaneColor;
                    PlaneColor = Color.Transparent;
                }
                RespawnTime++;
                if (RespawnTime >= 120)
                {
                    PlaneColor = temp;
                    isDead = false;
                    InvulnerableTime = 0;
                    isInvulnerable = true;
                    Rect = new Rectangle(100, 100, Rect.Width, Rect.Height);
                    rotation = 0;
                    RespawnTime = 0;
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


            if (Rect.X % 1080 < -50)
            {
                Rect.X = 1080;
            }
            else
            {
                Rect.X = Rect.X % 1080;
            }

            if (Rect.Y % 720 < -50)
            {
                Rect.Y = 720;
            }
            else
            {
                Rect.Y = Rect.Y % 720;
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].rect().X > 1080 || bullets[i].rect().Y > 720)
                    bullets.Remove(bullets[i]);
            }
        }

        public Rectangle rect()
        {
            return new Rectangle(Rect.X - Rect.Width / 2, Rect.Y - Rect.Height / 2, Rect.Width, Rect.Height);
        }

        public void Shoot()
        {
            if (!Reloading)
            {
                double rot = rotation * (Math.PI / 180);
                if (NumOfBullets == 1)
                    bullets.Add(new Bullet(bulletTex, new Rectangle(Rect.X + (int)(25 * Math.Cos(rot)), Rect.Y + (int)(25 * Math.Sin(rot)), 20, 10), PlaneColor, rot, 8));
                if (NumOfBullets == 2)
                {
                    bullets.Add(new Bullet(bulletTex, new Rectangle(Rect.X + (int)(20 * Math.Sin(-rot)), Rect.Y + (int)(20 * Math.Cos(-rot)), 20, 10), PlaneColor, rot, 8));
                    bullets.Add(new Bullet(bulletTex, new Rectangle(Rect.X + (int)(-20 * Math.Sin(-rot)), Rect.Y + (int)(-20 * Math.Cos(-rot)), 20, 10), PlaneColor, rot, 8));
                }
                if (NumOfBullets == 4)
                {
                    bullets.Add(new Bullet(bulletTex, new Rectangle(Rect.X + (int)(30 * Math.Sin(-rot)), Rect.Y + (int)(30 * Math.Cos(-rot)), 20, 10), PlaneColor, rot, 8));
                    bullets.Add(new Bullet(bulletTex, new Rectangle(Rect.X + (int)(10 * Math.Sin(-rot)), Rect.Y + (int)(10 * Math.Cos(-rot)), 20, 10), PlaneColor, rot, 8));
                    bullets.Add(new Bullet(bulletTex, new Rectangle(Rect.X + (int)(-10 * Math.Sin(-rot)), Rect.Y + (int)(-10 * Math.Cos(-rot)), 20, 10), PlaneColor, rot, 8));
                    bullets.Add(new Bullet(bulletTex, new Rectangle(Rect.X + (int)(-30 * Math.Sin(-rot)), Rect.Y + (int)(-30 * Math.Cos(-rot)), 20, 10), PlaneColor, rot, 8));
                }
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
            count++;
            foreach (Bullet b in bullets)
            {
                b.Update();
                b.Draw(spriteBatch);
            }
            if (!isInvulnerable)
                spriteBatch.Draw(Text2D, Rect, null, PlaneColor, (float)(rotation * (Math.PI / 180)), new Vector2(Text2D.Width / 2, Text2D.Height / 2), SpriteEffects.None, 0);
            else if (count % 20 < 10)
                spriteBatch.Draw(Text2D, Rect, null, PlaneColor, (float)(rotation * (Math.PI / 180)), new Vector2(Text2D.Width / 2, Text2D.Height / 2), SpriteEffects.None, 0);
        }


    }
}

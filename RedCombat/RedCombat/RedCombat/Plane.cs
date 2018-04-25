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
        Texture2D Text2D;
        Rectangle Rect;

        Vector2 Velocity;
        int ReloadTime = 0;
        int MAXReloadTime;
        int NumOfBulltes;
        bool Reloading;

        Texture2D bulletTex;

        public Plane(Texture2D t, Rectangle r, Vector2 v, int reloadT, int BulletNum, Texture2D bulletT)
        {
            Text2D = t;
            Rect = r;
            Velocity = v;
            MAXReloadTime = reloadT;
            NumOfBulltes = BulletNum;
            bulletTex = bulletT;
        }

        public void Update()
        {
            if (Reloading)
            {
                ReloadTime++;
                if(ReloadTime >= MAXReloadTime)
                {
                    Reloading = false;
                    ReloadTime = 0;
                }
            }
        }


        public void Shoot()
        {
            if (!Reloading)
            {
                Bullet b = new Bullet(bulletTex, new Rectangle(200, 200, 20, 10), Color.Blue, Math.PI, 3);
                Reloading = true;
            }
        }

        public void Respawn()
        {

        }


    }
}

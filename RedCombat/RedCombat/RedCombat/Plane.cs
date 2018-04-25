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
        int RespawnTime = 0;
        int InvulnerableTime = 0;
        Color PlaneColor;

        public bool Reloading = false;
        public bool isDead = false;
        public bool isInvulnerable = true;


        public Plane(Texture2D t, Rectangle r, Color c, Vector2 v, int reloadT, int BulletNum)
        {
            Text2D = t;
            Rect = r;
            PlaneColor = c;
            Velocity = v;
            MAXReloadTime = reloadT;
            NumOfBulltes = BulletNum;
        }

        public void Update()
        {
            //Reloading

            if (Reloading)
            {
                ReloadTime++;
                if(ReloadTime >= MAXReloadTime)
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
                if(InvulnerableTime >= 120)
                {
                    isInvulnerable = false;
                }
            }
        }


        public void Shoot()
        {
            if (!Reloading)
            {
                Bullet b = new Bullet();
                ReloadTime = 0;
                Reloading = true;
            }
        }

        public void IsShot()
        {
            isDead = true;
        }


    }
}

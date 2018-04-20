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
        int ReloadTime;
        int NumOfBulltes;
        bool Reloading;


        public Plane(Texture2D t, Rectangle r, Vector2 v, int reloadT, int BulletNum)
        {
            Text2D = t;
            Rect = r;
            Velocity = v;
            ReloadTime = reloadT;
            NumOfBulltes = BulletNum;
        }



    }
}

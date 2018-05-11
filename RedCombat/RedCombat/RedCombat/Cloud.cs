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
    class Cloud
    {

        public Texture2D CloudText;
        public Rectangle CloudRect;
        double CLOUD_VELOCITY;
        double cloudX;
        double cloudY;
        public Cloud(Texture2D t, Rectangle r)
        {
            Random rand = new Random();
            CloudText = t;
            CloudRect = r;
            cloudX = CloudRect.X;
            cloudY = CloudRect.Y;
            CLOUD_VELOCITY = rand.Next(2, 8);
            CLOUD_VELOCITY /= 4;
        }
        public void CloudUpdate()
        {
            cloudX += CLOUD_VELOCITY;
            CloudRect.X = (int)cloudX;
            CloudRect.Y = (int)cloudY;
            //Cloud off screen
            if (CloudRect.X > 1080)
            {
                Random rand = new Random();
                CloudRect.Width = rand.Next(200, 300);
                CloudRect.Height = rand.Next(100, 150);
                CLOUD_VELOCITY = rand.Next(2, 8);
                CLOUD_VELOCITY /= 4;
                cloudX = -1 * CloudRect.Width;
                cloudY = rand.Next(CloudRect.Height, 720) - CloudRect.Height;
            }
        }
    }
}

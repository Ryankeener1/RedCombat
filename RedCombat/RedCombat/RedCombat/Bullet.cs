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
    class Bullet
    {
        Texture2D texture;
        Rectangle Rect;
        Color color;
        double rotation;
        Vector2 velocity;

        public Bullet(Texture2D tex, Rectangle rec, Color col, double rot, int speed)
        {
            texture = tex;
            Rect = rec;
            color = col;
            rotation = rot;
            speed = 10;
            velocity = new Vector2(speed * (float)Math.Cos(rotation), speed * (float)Math.Sin(rotation));
        }

        public void Update()
        {
            Rect.X += (int)velocity.X;
            Rect.Y += (int)velocity.Y;
        }

        public Rectangle rect()
        {
            return new Rectangle(Rect.X - Rect.Width / 2, Rect.Y - Rect.Height / 2, Rect.Width, Rect.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rect, null, color, (float)rotation, new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 1);
        }



    }
}

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
        Rectangle rect;
        Color color;
        double rotation;
        Vector2 velocity;

        public Bullet(Texture2D tex, Rectangle rec, Color col, double rot, int speed)
        {
            texture = tex;
            rect = rec;
            color = col;
            rotation = rot;
            velocity = new Vector2(speed * (float)Math.Cos(rotation), speed * (float)Math.Sin(rotation));
        }

        public void Update()
        {
            rect.X += (int)velocity.X;
            rect.Y += (int)velocity.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, null, color, (float)rotation, new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 1);
        }

    }
}

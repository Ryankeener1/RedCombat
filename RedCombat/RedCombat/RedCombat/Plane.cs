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
        public enum Abilities { Invisibilty, QuickReload, DoubleShot };
        Abilities CurrentAbility;
        public Texture2D Text2D;

        public Rectangle Rect;

        Vector2 Velocity;
        Vector2 vel;

        int ReloadTime = 0;
        int MAXReloadTime;
        int oldMXReloadTime;
        int NumOfBullets;
        int RespawnTime = 0;
        int InvulnerableTime = 0;
        int MaxAbilityCooldownTime;
        int MaxAbilityActiveTime;
        int AbilityCooldownTimer = 0;
        int AbilityActiveTimer = 0;
        public int lives;

        public Color PlaneColor;
        Color oldColor;
        public int rotation;

        public bool Reloading = false;
        public bool isDead = false;
        public bool isInvulnerable = true;
        public bool AbilityActive;
        public bool AbilityCooldown;

        int SpawnPointX, SpawnPointY;


        public List<Bullet> bullets = new List<Bullet>();

        Texture2D bulletTex;

        int count = 0;


        public Plane(Texture2D t, Rectangle r, Color c, Vector2 v, int reloadT, int BulletNum, Texture2D bulletT, PlayerIndex g, int ActiveLength, int CooldownLength, Abilities Ability)
        {
            Text2D = t;
            Rect = r;
            PlaneColor = c;
            oldColor = c;
            Velocity = v;
            MAXReloadTime = reloadT;
            oldMXReloadTime = reloadT;
            NumOfBullets = BulletNum;
            bulletTex = bulletT;
            Index = g;
            MaxAbilityCooldownTime = CooldownLength;
            MaxAbilityActiveTime = ActiveLength;

            SpawnPointX = Rect.X;
            SpawnPointY = Rect.Y;

            CurrentAbility = Ability;

            lives = 3;
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
                if (RespawnTime == 0)
                {
                    oldColor = PlaneColor;
                    PlaneColor = Color.Transparent;
                }
                RespawnTime++;
                if (RespawnTime >= 120)
                {
                    PlaneColor = oldColor;
                    isDead = false;
                    InvulnerableTime = 0;
                    isInvulnerable = true;
                    Rect = new Rectangle(SpawnPointX, SpawnPointY, Rect.Width, Rect.Height);
                    rotation = 0;
                    RespawnTime = 0;
                }


            }
                //Invulnerable
                if (isInvulnerable)
                {
                    InvulnerableTime++;
                                    }
            if (InvulnerableTime >= 120)
            {
                isInvulnerable = false;

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


                switch (CurrentAbility)
                {
                    case Abilities.Invisibilty:
                        if (gs.Buttons.RightShoulder == ButtonState.Pressed && !AbilityActive && !AbilityCooldown)
                        {
                            AbilityActive = true;
                            PlaneColor = Color.Transparent;

                        }
                        break;

                    case Abilities.QuickReload:
                        if (gs.Buttons.RightShoulder == ButtonState.Pressed && !AbilityActive && !AbilityCooldown)
                        {
                            AbilityActive = true;
                            MAXReloadTime = 30;
                        }
                        break;

                    case Abilities.DoubleShot:

                        if (gs.Buttons.RightShoulder == ButtonState.Pressed && !AbilityActive && !AbilityCooldown)
                        {
                            AbilityActive = true;
                            NumOfBullets = 4;
                        }
                        break;

                    default:
                        break;
                }

                if (AbilityActive)
                {
                    AbilityActiveTimer++;

                }
                if (AbilityActiveTimer >= MaxAbilityActiveTime)
                {
                    AbilityActive = false;
                    AbilityCooldown = true;

                    switch (CurrentAbility)
                    {
                        case Abilities.Invisibilty:
                            PlaneColor = oldColor;


                            break;

                        case Abilities.QuickReload:
                            MAXReloadTime = oldMXReloadTime;
                            break;

                        case Abilities.DoubleShot:
                            NumOfBullets = 2;

                            break;

                        default:
                            break;
                    }
                    AbilityActiveTimer = 0;

                }
                if (AbilityCooldown)
                {
                    AbilityCooldownTimer++;
                }
                if (AbilityCooldownTimer >= MaxAbilityCooldownTime)
                {
                    AbilityCooldown = false;
                    AbilityCooldownTimer = 0;
                }




            }
        



        public void Shoot()
        {
            if (!Reloading)
            {
                double rot = rotation * (Math.PI / 180);
                if (NumOfBullets == 1)
                    bullets.Add(new Bullet(bulletTex, new Rectangle(Rect.X + (int)(25 * Math.Cos(rot)), Rect.Y + (int)(25 * Math.Sin(rot)), 20, 10), PlaneColor, rot, 10));
                if (NumOfBullets == 2)
                {
                    bullets.Add(new Bullet(bulletTex, new Rectangle(Rect.X + (int)(20 * Math.Sin(-rot)), Rect.Y + (int)(20 * Math.Cos(-rot)), 20, 10), PlaneColor, rot, 10));
                    bullets.Add(new Bullet(bulletTex, new Rectangle(Rect.X + (int)(-20 * Math.Sin(-rot)), Rect.Y + (int)(-20 * Math.Cos(-rot)), 20, 10), PlaneColor, rot, 10));
                }
                if (NumOfBullets == 4)
                {
                    bullets.Add(new Bullet(bulletTex, new Rectangle(Rect.X + (int)(20 * Math.Sin(-rot)), Rect.Y + (int)(20 * Math.Cos(-rot)), 20, 10), PlaneColor, rot, 10));
                    bullets.Add(new Bullet(bulletTex, new Rectangle(Rect.X + (int)(-20 * Math.Sin(-rot)), Rect.Y + (int)(-20 * Math.Cos(-rot)), 20, 10), PlaneColor, rot, 10));
                    bullets.Add(new Bullet(bulletTex, new Rectangle(Rect.X, Rect.Y, 20, 10), PlaneColor, rot - Math.PI / 2, 8));
                    bullets.Add(new Bullet(bulletTex, new Rectangle(Rect.X, Rect.Y, 20, 10), PlaneColor, rot + Math.PI / 2, 8));
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


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenzin_Lote_Game.Weapons;

namespace Tenzin_Lote_Game.Character
{
    class Player: Character
    {
        private Controller buttons;
        private bool hasJumped = false;

        public Player(Texture2D _texture,Texture2D newBulletTexure)
        {
            texture = _texture;
            bulletTexture = newBulletTexure;
            buttons = new ControllerArrows();
            healthPosition = new Vector2(position.X, position.Y-20);
            
            int pixelwidth = 0;
            FiveFramesAnimation = new Animation();
            for (int i = 0; i < 5; i++)
            {
                FiveFramesAnimation.AddFrame(new Rectangle(pixelwidth, 0, 75, 64));
                pixelwidth = i * 75;
            }
            EightFramesAnimation = new Animation();
            for (int i = 0; i < 8; i++)
            {
                EightFramesAnimation.AddFrame(new Rectangle(pixelwidth, 0, 75, 64));
                pixelwidth = i * 75;
            }
        }

        public void BulletCollision(Rectangle newRectangle)
        {
            foreach (Bullets bullet in bullets)
            {
                if (bullet.BulletRectangle.Intersects(newRectangle))
                {
                    bullet.isVisible = false;

                }
            }
            
            
        }

        public void UpdateBullets(GameTime gameTime)
        {
            foreach(Bullets bullet in bullets)
            {
                bullet.Update(gameTime);
                bullet.position += bullet.velocity;
                
                if (bullet.position.X < 0)
                    bullet.isVisible = false;
                
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].isVisible)
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }
        }
        public void ShootBullets()
        {
           
            Bullets newBullet = new Bullets(bulletTexture);
            if (playerTurnRight)
                newBullet.velocity.X = velocity.X + 6f;
            else newBullet.velocity.X = -(velocity.X + 6f);

            newBullet.position = new Vector2(position.X + newBullet.velocity.X, position.Y + (texture.Height / 2) - (bulletTexture.Height / 2));
            newBullet.isVisible = true;
            if (buttons.Space)
                bullets.Add(newBullet);
           
        }

        public override void Load(ContentManager Content)
        {
            CharacterTexture.Add(Content.Load<Texture2D>("john_idle"));
            CharacterTexture.Add(Content.Load<Texture2D>("john_run"));
            CharacterTexture.Add(Content.Load<Texture2D>("john_idle(left)"));
            CharacterTexture.Add(Content.Load<Texture2D>("john_run(left)"));
            healthTexture = Content.Load<Texture2D>("greenbar (1)");
            healthRectangle = new Rectangle(0, 0, healthTexture.Width, healthTexture.Height);
        }


        float shoot = 0;
        public override void Update(GameTime gameTime)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 75, 64);
            buttons.Update();
            if (buttons.Right||buttons.Left)
            {
                animation = EightFramesAnimation;
            }
            else 
            {
                animation = FiveFramesAnimation;
            }
            animation.Update(gameTime);
            AnimationRichting();
            Input(gameTime);

            healthPosition.Y = position.Y - 30;
            healthPosition.X = position.X -20;

            if (velocity.Y <10)
                velocity.Y += 0.4f;

            shoot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (shoot>0.3)
            {
                shoot = 0;
                ShootBullets();
            }
            
            UpdateBullets(gameTime);
        }

        private  void AnimationRichting()
        {
            if (buttons.Right)
            {
                texture = CharacterTexture[1];
                playerTurnRight = true;
            }
            else if (buttons.Left)
            {
                texture = CharacterTexture[3];
                playerTurnRight = false;
            }
            else if (!buttons.Right && !buttons.Left && playerTurnRight)
            {
                texture = CharacterTexture[0];
            }
            else if (!buttons.Right && !buttons.Left && !playerTurnRight)
            {
                texture = CharacterTexture[2];
            }
        }

        private  void Input(GameTime gameTime)
        {
            if (buttons.Right)
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            else if (buttons.Left)
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            else velocity.X = 0f;
            if (buttons.Up&&!hasJumped)
            {
                position.Y -= 5f;
                velocity.Y = -9f;
                hasJumped = true;
            }
        }

        public override void Collision(Rectangle newRectangle,int xOffset, int yOffset)
        {
             
            
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height; 
                velocity.Y = 0f;
                hasJumped = false;
            }

            if (rectangle.TouchLeftOf(newRectangle))
                position.X = newRectangle.X - rectangle.Width - 2;
                
            if (rectangle.TouchRightOf(newRectangle))
                position.X = newRectangle.X + rectangle.Width + 2;
            if (rectangle.TouchBottomOf(newRectangle))
                velocity.Y = 1f;

            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
            if (position.Y < 0) velocity.Y = 1f;
            if (position.Y > yOffset - rectangle.Height) position.Y = yOffset - rectangle.Height;

            BulletCollision(newRectangle);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullets bullet in bullets)
                bullet.Draw(spriteBatch);
            spriteBatch.Draw(texture, rectangle, animation.CurrentFrame.SourceRectangle, Color.AliceBlue);
            spriteBatch.Draw(healthTexture, healthPosition, healthRectangle, Color.White);
        }

        public override int DamageTaken()
        {
            if (HealthPoint<=0)
            {
                Console.WriteLine("Hero is dood");
                HealthPoint = 0;
            }
            else if (HealthPoint > 100)
            {
                HealthPoint = 100;
            }
            healthRectangle.Width -= 1;
            return HealthPoint = healthRectangle.Width;
        }

     
    }
}

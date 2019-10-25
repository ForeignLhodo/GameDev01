using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenzinLote_Gamedev.Animations;
using TenzinLote_Gamedev.Weapons;

namespace TenzinLote_Gamedev.Characters
{
    class Enemy : Character
    {

        private Animation deathFramesAnimation, idleFramesAnimation, runFramesAnimation;
        private int mode;
        bool keerLinks = false;
        public Enemy(Texture2D _texture, Vector2 position, int _mode,Texture2D newbulletTexure)
        {
            this.position = position;
            texture = _texture;
            LoadingAnimation();
            mode = _mode;
            bulletTexture = newbulletTexure;
            healthPosition = new Vector2();
        }
        private void LoadingAnimation()
        {
            idleFramesAnimation = new Animation();
            int pixelwidth = 0;
            for (int i = 0; i < 2; i++)
            {
                idleFramesAnimation.AddFrame(new Rectangle(pixelwidth, 0, 75, 64));
                pixelwidth = i * 75;
            }

            deathFramesAnimation = new Animation();
            for (int i = 0; i < 12; i++)
            {
                deathFramesAnimation.AddFrame(new Rectangle(pixelwidth, 0, 75, 64));
                pixelwidth = i * 75;
            }

            runFramesAnimation = new Animation();
            for (int i = 0; i < 10; i++)
            {
                runFramesAnimation.AddFrame(new Rectangle(pixelwidth, 0, 75, 64));
                pixelwidth = i * 75;
            }
            if (mode == 1)
            {
                animation = runFramesAnimation;
            }
            else
            {
                animation = idleFramesAnimation;

            }


        }
        public override void Load(ContentManager Content)
        {
            CharacterTexture.Add(Content.Load<Texture2D>("grunt_idle"));
            CharacterTexture.Add(Content.Load<Texture2D>("grunt_death"));
            CharacterTexture.Add(Content.Load<Texture2D>("grunt_run"));
            CharacterTexture.Add(Content.Load<Texture2D>("grunt_run(left)"));
            CharacterTexture.Add(Content.Load<Texture2D>("grunt_idle(left)"));
            healthTexture = Content.Load<Texture2D>("greenbar (5)");
            healthRectangle = new Rectangle(0, 0, healthTexture.Width, healthTexture.Height);
        }
        float death = 0;
        float shoot = 0;
        public override void Update(GameTime gameTime)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 75, 64);
            healthPosition.Y = position.Y - 20;
            healthPosition.X = position.X - 20;
            if (mode == 1)
            {
                if (!keerLinks && life)
                    position.X += 1;

                if (keerLinks && life)
                    position.X -= 1;
            }
            if (!life)
            {
                animation = deathFramesAnimation;
                texture = CharacterTexture[1];
                death += (float)gameTime.ElapsedGameTime.TotalSeconds;

            }

            animation.Update(gameTime);
            if (!life && death > 1)
            {
                //buiten de scherm zetten zodat de enemy zogezegd verdwenen is
                position.X = -500;
                position.Y = -500;
            }
            shoot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (shoot > 2.5)
            {
                shoot = 0;
                ShootBullets();
            }
            UpdateBullets(gameTime);
        }
        public override void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (mode == 1)
            {
                if (rectangle.TouchTopOf(newRectangle))
                {
                    rectangle.Y = newRectangle.Y - rectangle.Height;
                    velocity.Y = 0f;
                }
                if (rectangle.TouchLeftOf(newRectangle))
                {
                    keerLinks = true;
                    texture = CharacterTexture[3];
                }

                if (rectangle.TouchRightOf(newRectangle))
                {
                    keerLinks = false;
                    texture = CharacterTexture[2];
                }

                if (rectangle.TouchBottomOf(newRectangle))
                    velocity.Y = 1f;
            }
            else if (mode == 2 && life)
            {
                texture = CharacterTexture[0];
            }
            else if (mode == 3 && life)
            {
                texture = CharacterTexture[4];
            }
            BulletCollision(newRectangle);
        }
        public override int DamageTaken(int damage)
        {
            if (HealthPoint <= 0)
            {
                HealthPoint = 0;
                life = false;
            }
            else if (HealthPoint > 100)
            {
                HealthPoint = 100;
            }
            healthRectangle.Width -= damage;
            return HealthPoint -= 30;
        }
        private void BulletCollision(Rectangle newRectangle)
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
            foreach (Bullets bullet in bullets)
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
            if (mode == 2 || mode == 1 && !keerLinks)
                newBullet.velocity.X = velocity.X + 3f;
            else if (mode == 3 || mode == 1 && keerLinks)
                newBullet.velocity.X = -(velocity.X + 3f);
            
                
            newBullet.position = new Vector2(position.X + newBullet.velocity.X, position.Y + (texture.Height / 2) - (bulletTexture.Height / 2));
            newBullet.isVisible = true;
            bullets.Add(newBullet);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullets bullet in bullets)
                bullet.Draw(spriteBatch);
            spriteBatch.Draw(texture, rectangle, animation.CurrentFrame.SourceRectangle, Color.AliceBlue);
            spriteBatch.Draw(healthTexture, healthPosition, healthRectangle, Color.White);
        }
    }
}

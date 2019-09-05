using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tenzin_Lote_Game.Character
{
    class Enemy : Character
    {
         
        private Animation deathFramesAnimation, idleFramesAnimation,runFramesAnimation;
        private int mode;
        bool keerLinks = false;
        public Enemy(Texture2D _texture,Vector2 position,int _mode)
        {
            this.position = position;
            texture = _texture;
            LoadingAnimation();
            mode = _mode;
            healthPosition = new Vector2();
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
            else if(mode ==2 && life)
            {
                texture = CharacterTexture[0];
            }
            else if (mode ==3 && life)
            {
                texture = CharacterTexture[4];
            }
            
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
            if (!life &&death>1)
            {
                //buiten de scherm zetten zodat de enemy zogezegd verdwenen is
                position.X = -500;
                position.Y = -500;
            }
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, animation.CurrentFrame.SourceRectangle, Color.AliceBlue);
            spriteBatch.Draw(healthTexture, healthPosition, healthRectangle, Color.White);
        }


        public override int DamageTaken()
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
            healthRectangle.Width -= 20;
            return HealthPoint -= 30;
        }
    }
}

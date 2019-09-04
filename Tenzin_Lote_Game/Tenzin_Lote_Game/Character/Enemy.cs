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
        private Animation twoFramesAnimation;
        public Enemy(Texture2D _texture)
        {
            
            texture = _texture;
            
            twoFramesAnimation = new Animation();
            int pixelwidth = 0;
            for (int i = 0; i < 2; i++)
            {
                twoFramesAnimation.AddFrame(new Rectangle(pixelwidth, 0, 75, 64));
                pixelwidth = i * 75;
            }

        }
        public override void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {

            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
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
            
        }

        

        public override void Load(ContentManager Content)
        {
            CharacterTexture.Add(Content.Load<Texture2D>("grunt_idle"));
            //CharacterTexture.Add(Content.Load<Texture2D>("grunt_idle(left)"));
            //CharacterTexture.Add(Content.Load<Texture2D>("grunt_run"));
            //CharacterTexture.Add(Content.Load<Texture2D>("grunt_run(left)"));
        }

        public override void Update(GameTime gameTime, List<Character> characters)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 75, 64);
            animation = twoFramesAnimation;
            animation.Update(gameTime);
           
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, animation.CurrentFrame.SourceRectangle, Color.AliceBlue);
        }
    }
}

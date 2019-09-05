using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenzin_Lote_Game.Weapons
{
    class Bullets
    {
        public Texture2D texture;
        public Animation Animation;
        public Animation FiveFramesAnimation;
        public Vector2 position;
        public Vector2 velocity;
        public Rectangle BulletRectangle;
        public bool isVisible;

        public Bullets(Texture2D newTexture)
        {
            texture = newTexture;
            isVisible = false;
            BulletRectangle = new Rectangle((int)position.X, (int)position.Y, 33, 30);
            Animation = new Animation();
            FiveFramesAnimation = new Animation();
            int pixelwidth = 0;
            for (int i = 0; i < 7; i++)
            {
                FiveFramesAnimation.AddFrame(new Rectangle(pixelwidth, 0,33,30));
                pixelwidth = i * 33;
            }
        }
       
        public void Update(GameTime gameTime)
        {
            Animation = FiveFramesAnimation;
            Animation.Update(gameTime);
            BulletRectangle.X =(int) position.X;
            BulletRectangle.Y =(int) position.Y;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,position, Animation.CurrentFrame.SourceRectangle, Color.White);
        }
    }
}

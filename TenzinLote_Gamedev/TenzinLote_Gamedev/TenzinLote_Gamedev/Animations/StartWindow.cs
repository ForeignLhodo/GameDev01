using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenzinLote_Gamedev.Animations
{
    class StartWindow
    {
        Texture2D texture;
        Animation animation = new Animation();
        Animation StartScreenAnimation;
        Vector2 position = new Vector2(0, 0);
        Rectangle rectangle;
        public StartWindow(Texture2D texture)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, 800, 510);
            StartScreenAnimation = new Animation();
            this.texture = texture;
            int pixelwidth = 0;
            for (int i = 0; i < 3; i++)
            {
                StartScreenAnimation.AddFrame(new Rectangle(pixelwidth, 0,800, 510));
                pixelwidth = i * 800;
            }
            animation = StartScreenAnimation;
        }
        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, animation.CurrentFrame.SourceRectangle, Color.White);
        }
    }
}

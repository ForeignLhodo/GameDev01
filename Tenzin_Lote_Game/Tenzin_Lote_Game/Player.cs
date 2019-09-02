using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenzin_Lote_Game
{
    class Player
    {
        public Texture2D texture;
        private Vector2 position = new Vector2(100, 370);
        private Vector2 velocity;
        private Animation animation;
        protected Animation EightFramesAnimation;
        protected Animation FiveFramesAnimation;

        private Rectangle rectangle;
        public Controller buttons;
        private bool hasJumped = false;

        public Vector2 Postition
        {
            get { return position; }
        } 

        public Player(Texture2D _texture) {
            texture = _texture;
            buttons = new ControllerArrows();
            animation = new Animation();
            
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

       

        public void Update(GameTime gameTime)
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
            
            Input(gameTime);
            

            if (velocity.Y <10)
                velocity.Y += 0.4f;
        }

        private void Input(GameTime gameTime)
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

        public void Collision(Rectangle newRectangle,int xOffset, int yOffset)
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
          spriteBatch.Draw(texture, rectangle, animation.CurrentFrame.SourceRectangle, Color.AliceBlue);
        }
    }
}

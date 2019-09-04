using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenzin_Lote_Game.Weapons;

namespace Tenzin_Lote_Game.Character
{
  abstract  class Character
    {
        public Texture2D texture;
        protected Vector2 position = new Vector2(100, 370);
        protected Vector2 velocity;
        protected Animation animation = new Animation();
        protected Animation EightFramesAnimation;
        protected Animation FiveFramesAnimation;
        protected List<Texture2D> CharacterTexture = new List<Texture2D>();
        public Rectangle rectangle;
        protected bool playerTurnRight;
        public List<Bullets> bullets = new List<Bullets>();
        protected Texture2D bulletTexture;

        public Vector2 Postition
        {
            get { return position; }
        }
        abstract public void Load(ContentManager Content);
        abstract public void Update(GameTime gameTime,List<Character> characters);
        abstract public void Collision(Rectangle newRectangle, int xOffset, int yOffset);
        abstract public void Draw(SpriteBatch spriteBatch);
    }
}

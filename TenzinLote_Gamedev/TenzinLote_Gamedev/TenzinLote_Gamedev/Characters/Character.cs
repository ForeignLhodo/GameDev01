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
    abstract class Character
    {
        public Texture2D texture, healthTexture, bulletTexture;
        public Rectangle rectangle, healthRectangle;
        protected Vector2 position = new Vector2(100, 370);
        protected Vector2 velocity, healthPosition;
        protected Animation animation = new Animation();
        protected Animation EightFramesAnimation, FiveFramesAnimation;
        protected List<Texture2D> CharacterTexture = new List<Texture2D>();
        public List<Bullets> bullets = new List<Bullets>();
        public int HealthPoint = 100;
        public bool life = true;
        protected bool playerTurnRight;

        public Vector2 Postition
        {
            get { return position; }
            set { position = value; }
        }
        abstract public void Load(ContentManager Content);
        abstract public void Update(GameTime gameTime);
        abstract public void Collision(Rectangle newRectangle, int xOffset, int yOffset);
        abstract public void Draw(SpriteBatch spriteBatch);
        abstract public int DamageTaken(int damage);
        

    }
}

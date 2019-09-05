using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Tenzin_Lote_Game.Weapons;

namespace Tenzin_Lote_Game.Character
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D idle, bullet, gruntidle,health;
        Map map;
        Character player;
        List<Character> enemy = new List<Character>();
        Camera camera;
        
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            map = new Map();
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
           // health = Content.Load<Texture2D>("greenbar(1)");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            idle = Content.Load<Texture2D>("john_idle");
            bullet = Content.Load<Texture2D>("john_bullet");
            player = new Player(idle,bullet);
            player.Load(Content);
            gruntidle = Content.Load<Texture2D>("grunt_run");
            enemy.Add(new Enemy(gruntidle, new Vector2(80, 390),1));
            enemy.Add(new Enemy(gruntidle, new Vector2(200, 132),2));
            enemy.Add(new Enemy(gruntidle, new Vector2(300, 132), 3));
            foreach (Enemy enemy in enemy)
            {
                enemy.Load(Content);
            }
            Tiles.Content = Content;
            camera = new Camera(GraphicsDevice.Viewport);
            map.GenerateWorld1();
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (Keyboard.GetState().IsKeyDown(Keys.B))
            {
               
            } 
           
            player.Update(gameTime);
            foreach (Enemy enemy in enemy)
            {
                enemy.Update(gameTime);
                if (enemy.rectangle.Intersects(player.rectangle))
                {
                    player.DamageTaken();
                }
            }
            foreach (CollisionTiles tile in map.CollisionTiles)
            { 
                player.Collision(tile.Rectangle, map.Width, map.Height);
                foreach (Enemy enemy in enemy)
                {
                    enemy.Collision(tile.Rectangle, map.Width, map.Height);
                }
                camera.Update(player.Postition,  map.Width, map.Height);
            }
            
            foreach (Bullets bullet  in player.bullets)
            {
                foreach (Enemy enemy in enemy)
                {
                    if (bullet.BulletRectangle.Intersects(enemy.rectangle))
                    {
                        enemy.DamageTaken();
                        bullet.isVisible = false;
                    }
                    
                }
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,null,null,null,null,camera.Transform);
            
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            
            foreach (Enemy enemy in enemy)
            {
                enemy.Draw(spriteBatch);
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

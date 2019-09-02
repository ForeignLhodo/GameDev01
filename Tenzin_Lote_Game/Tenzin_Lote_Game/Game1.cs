using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Tenzin_Lote_Game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Texture2D> HeroTexture = new List<Texture2D>();
        Map map;
        Player player;
        Camera camera;
        bool Lastpressed;
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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            HeroTexture.Add(Content.Load<Texture2D>("john_idle"));
            HeroTexture.Add(Content.Load<Texture2D>("john_run"));
            HeroTexture.Add(Content.Load<Texture2D>("john_idle(left)"));
            HeroTexture.Add(Content.Load<Texture2D>("john_run(left)"));
            
            player = new Player(HeroTexture[0]);
            Tiles.Content = Content;
            camera = new Camera(GraphicsDevice.Viewport);
            map.Generate(new int[,]
            {
                {1,0,0,0,0,0,0,0,0,0  ,0,0,0,0,0,0,0,0,0,0,0,0 },
                {2,0,0,0,0,0,0,0,0,0  ,0,0,0,0,0,0,0,0,0,0,1,1 },
                {2,1,0,0,0,0,0,0,0,0  ,0,0,1,1,1,0,0,0,1,1,2,2 },
                {2,2,1,1,1,0,0,0,0,1  ,1,1,2,2,2,1,0,0,0,0,2,2 },

                {2,2,0,0,0,0,0,0,1,2  ,2,2,2,2,2,2,1,0,0,0,2,2 },
                {2,0,0,0,0,0,1,1,2,2  ,2,2,2,2,2,2,2,1,1,1,2,2 },
                {2,0,0,0,1,1,2,2,2,2  ,2,2,2,2,2,2,2,2,2,2,2,2 },
                {2,1,1,1,2,2,2,2,2,2  ,2,2,2,2,2,2,2,2,2,2,2,2 },

            }, 64);
            
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
           
            
            if (player.buttons.Right )
            {
                player.texture = HeroTexture[1];
                Lastpressed = true;
            }
            else if(player.buttons.Left)
            {
                player.texture = HeroTexture[3];
                Lastpressed = false;
            }
            else if (!player.buttons.Right && !player.buttons.Left && Lastpressed)
            {
                player.texture = HeroTexture[0];
            }
            else if (!player.buttons.Right && !player.buttons.Left && !Lastpressed)
            {
                player.texture = HeroTexture[2];
            }
            player.Update(gameTime);
            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                player.Collision(tile.Rectangle, map.Width, map.Height);
                camera.Update(player.Postition, map.Width, map.Height);
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
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

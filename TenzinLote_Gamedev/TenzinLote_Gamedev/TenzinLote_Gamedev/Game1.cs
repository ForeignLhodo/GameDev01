using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TenzinLote_Gamedev.Animations;
using TenzinLote_Gamedev.Characters;
using TenzinLote_Gamedev.Weapons;
using TenzinLote_Gamedev.World;

namespace TenzinLote_Gamedev
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector2 playerPos = new Vector2(100, 370);
        Vector2 enemy1Pos = new Vector2(2600, 64);
        Vector2 enemy2Pos = new Vector2(1664, 132);
        Vector2 enemy3Pos = new Vector2(1664, 385);
        Vector2 enemy4Pos = new Vector2(1152, 256);
        Vector2 enemy5Pos = new Vector2(140, 132);
        Vector2 enemy6Pos = new Vector2(850, 64);
        

        Texture2D idle, bullet, gruntidle, startingScreenTexture, lostScreenTexture, endingScreenTexture, backGroundWoudsTexture;
        Character player;
        List<Character> enemy = new List<Character>();
        Camera camera;
        Map map;
        StartWindow startingScreen;
        enum GameState
        { 
            MainMenu,
            Level1,
            Death,
            EndGame
        }
        GameState CurrentGameState = GameState.MainMenu;
        // screen adjustments
         
        int screenWidth = 800, screenHeight = 510;
        cButton btnPlay, btnRestart;
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
          
         //   state = new MenuState(this, GraphicsDevice, Content);
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
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            // graphics.IsFullScreen = true;
            graphics.ApplyChanges();


            // For the buttons that is used to navigate through the gameState
            IsMouseVisible = true;
            btnRestart = new cButton(Content.Load<Texture2D>("RestartButton"), GraphicsDevice);
            btnPlay = new cButton(Content.Load<Texture2D>("StartButton"), GraphicsDevice);
            btnPlay.setPosition(new Vector2(310, 450));
            btnRestart.setPosition(new Vector2(310, 300));
            endingScreenTexture = Content.Load<Texture2D>("CreditsScreen");
            lostScreenTexture = Content.Load<Texture2D>("GameOverScreen-still");
            startingScreenTexture = Content.Load<Texture2D>("title_screen");
            startingScreen = new StartWindow(startingScreenTexture);
            backGroundWoudsTexture = Content.Load<Texture2D>("backGround");
            //Loading the World Folder
            map = new Map();
            Tiles.Content = Content;
            camera = new Camera(GraphicsDevice.Viewport);
            map.GenerateWorld1();

            // Load for the Characters
            idle = Content.Load<Texture2D>("john_idle");
            bullet = Content.Load<Texture2D>("john_bullet");
            player = new Player(idle, bullet);
            player.Load(Content);
            gruntidle = Content.Load<Texture2D>("grunt_run");
            enemy.Add(new Enemy(gruntidle, enemy1Pos, 3, bullet));
            enemy.Add(new Enemy(gruntidle, enemy2Pos, 2, bullet));
            enemy.Add(new Enemy(gruntidle, enemy3Pos, 1, bullet));
            enemy.Add(new Enemy(gruntidle, enemy4Pos, 1, bullet));
            enemy.Add(new Enemy(gruntidle, enemy5Pos, 2, bullet));
            enemy.Add(new Enemy(gruntidle, enemy6Pos, 3, bullet));
            foreach (Enemy enemy in enemy)
            {
                enemy.Load(Content);
            }
            
            
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
            MouseState mouse = Mouse.GetState();
            
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    if (btnPlay.isClicked == true) CurrentGameState = GameState.Level1;
                    btnPlay.Update(mouse);
                    startingScreen.Update(gameTime);
                    break;
                case GameState.Death:
                    if (btnRestart.isClicked == true)
                    {
                        CurrentGameState = GameState.Level1;
                        player.Postition = playerPos;
                        player.healthRectangle.Width = 100;
                        enemy[0].Postition = enemy1Pos;
                        enemy[1].Postition = enemy2Pos;
                        enemy[2].Postition = enemy3Pos;
                        enemy[3].Postition = enemy4Pos;
                        enemy[4].Postition = enemy5Pos;
                        enemy[5].Postition = enemy6Pos;
                        foreach (Enemy enemy in enemy)
                        {
                            enemy.healthRectangle.Width = 100;
                            enemy.HealthPoint = 100;
                            enemy.life = true;
                        }
                    }
                    btnRestart.Update(mouse);

                    break;
                case GameState.Level1:
                    if (player.Postition.X > 2600 && player.Postition.Y <= 2)
                    {
                        // Checking if all enemies have been killed so you can pass through the endGame State
                        int TotalHealth = 0;
                        for (int i = 0; i < enemy.Count; i++)
                        {
                            TotalHealth += enemy[i].HealthPoint;
                        }
                        if (TotalHealth<=0)
                        {
                            CurrentGameState = GameState.EndGame;
                        }
                    }
                    // checking if the player is death or not
                    if (player.healthRectangle.Width <=0)
                    {
                        CurrentGameState = GameState.Death;
                    }

                    break;
                case GameState.EndGame:
                    
                    break;
                default:
                    break;
            }
            // TODO: Add your update logic here

            player.Update(gameTime);
            // if the player touches the enemy, Player will take damage
            foreach (Enemy enemy in enemy)
            {
                enemy.Update(gameTime);
                if (enemy.rectangle.Intersects(player.rectangle))
                {
                    player.DamageTaken(1);
                }
            }

            // Collision detection for the enemy and hero. Making sure that the Camera also follows the Player
            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                player.Collision(tile.Rectangle, map.Width, map.Height);
                foreach (Enemy enemy in enemy)
                {
                    enemy.Collision(tile.Rectangle, map.Width, map.Height);
                }
                camera.Update(player.Postition, map.Width, map.Height);
            }
            // if the player bullets hits the enemy, Enemy will take Damage
            foreach (Bullets bullet in player.bullets)
            {
                foreach (Enemy enemy in enemy)
                {
                    if (bullet.BulletRectangle.Intersects(enemy.rectangle))
                    {
                        enemy.DamageTaken(20);
                        bullet.isVisible = false;
                    }
                }
            }

            foreach (Enemy enemy in enemy)
            {
                foreach (Bullets bullet in enemy.bullets)
                {
                    if (bullet.BulletRectangle.Intersects(player.rectangle))
                    {
                        player.DamageTaken(10);
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
            // 
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Begin();
                    startingScreen.Draw(spriteBatch);
                    btnPlay.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
                case GameState.Death:
                    spriteBatch.Begin();
                    spriteBatch.Draw(lostScreenTexture, new Rectangle(0, 0, 800, 510), Color.White);
                    btnRestart.Draw(spriteBatch);
                    
                    spriteBatch.End();
                    break;
                case GameState.Level1:
                    
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
                    spriteBatch.Draw(backGroundWoudsTexture, new Rectangle(0,0, 3000, 510),Color.White);
                    map.Draw(spriteBatch);
                    foreach (Enemy enemy in enemy)
                    {
                        enemy.Draw(spriteBatch);
                    }
                    player.Draw(spriteBatch);
                    spriteBatch.End();
                    
                    break;
                case GameState.EndGame:
                    spriteBatch.Begin();
                    spriteBatch.Draw(endingScreenTexture, new Rectangle(0, 0, 800, 510), Color.White);
                    spriteBatch.End();
                    break;
                default:
                    break;
            }
            
         
                    
          
            base.Draw(gameTime);
        }
    }
}

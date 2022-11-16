using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Linq;

namespace PirateMan
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        static Tile[,] tiles;
        int tileSize;
        Texture2D hitBoxTexture;
        PacMan pacman;
        
        int oranges;
        int lives = 3;
        List<Orange> orangeList;
        List<Enemy> enemyList;
        List<SuperOrange> superOrangesList;
        public Rectangle screenSize;
        Texture2D backgroundTexture;
        Texture2D winTexture;
        Texture2D loseTexture;
        SpriteFont scorefont;
        Song song;
        SoundEffect nomSound;
        SoundEffect deathSound;
        
        enum GameState
        {
            Start,
            Playing,
            GameOver,
            Win
        }
        GameState currenGameState;
        
       

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            tileSize = 32;
            _graphics.PreferredBackBufferWidth = tileSize * 28;
            _graphics.PreferredBackBufferHeight = tileSize * 18;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            currenGameState = GameState.Start;
            hitBoxTexture = Content.Load<Texture2D>("water");
            Texture2D crateTexture = Content.Load<Texture2D>("crate");
            Texture2D waterTexture = Content.Load<Texture2D>("Water");
            Texture2D playerTexture = Content.Load<Texture2D>("classic-pegleg");
            Texture2D orangeTexture = Content.Load<Texture2D>("Orange");
            Texture2D enemyTexure = Content.Load<Texture2D>("Pirate1 (Walk)");
            
            StreamReader sr = new StreamReader("../../../Content/Map.txt");
            scorefont = Content.Load<SpriteFont>("ScoreFont");
            backgroundTexture = Content.Load<Texture2D>("PirateBackground");
            winTexture = Content.Load<Texture2D>("YouWin");
            loseTexture = Content.Load<Texture2D>("YouLose");
            song = Content.Load<Song>("Song");
            nomSound = Content.Load<SoundEffect>("Nom");
            deathSound = Content.Load<SoundEffect>("1yell3");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            List<string> strings = new List<string>();
            enemyList = new List<Enemy>();
            superOrangesList = new List<SuperOrange>();
            


            orangeList = new List<Orange>();

            
            while (!sr.EndOfStream)
            {
                strings.Add(sr.ReadLine());
            }
            sr.Close();

            tiles = new Tile[strings[0].Length, strings.Count];

            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (strings[j][i] == '.')
                    {
                        tiles[i, j] = new Tile(new Vector2(crateTexture.Width * i, crateTexture.Height * j), crateTexture, false);
                        orangeList.Add(new Orange(new Vector2(waterTexture.Width * i, waterTexture.Width * j), orangeTexture));
                        
                    }
                    else if (strings[j][i] == 'W')
                    {
                        tiles[i, j] = new Tile(new Vector2(waterTexture.Width * i, waterTexture.Height * j), waterTexture, true);
                    }
                    else if (strings[j][i] == 'P')
                    {
                        tiles[i, j] = new Tile(new Vector2(crateTexture.Width * i, crateTexture.Height * j), crateTexture, false);
                        pacman = new PacMan(new Vector2(crateTexture.Width * i, crateTexture.Width * j), playerTexture);
                    }
                    else if (strings[j][i] == 'E')
                    {
                        tiles[i, j] = new Tile(new Vector2(crateTexture.Width * i, crateTexture.Height * j), crateTexture, false);
                        enemyList.Add(new Enemy(new Vector2(crateTexture.Width * i, crateTexture.Height * j), enemyTexure));
                        orangeList.Add(new Orange(new Vector2(waterTexture.Width * i, waterTexture.Width * j), orangeTexture));

                     
                    }
                    else if (strings[j][i] == 'F')
                    {
                        tiles[i, j] = new Tile(new Vector2(crateTexture.Width * i, crateTexture.Height * j), crateTexture, false);
                        
                        superOrangesList.Add(new SuperOrange(new Vector2(waterTexture.Width * i, waterTexture.Width * j), orangeTexture));


                    }


                    oranges = orangeList.Count;
                }
            }

            

        }
        

        


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            switch (currenGameState)
            {
                case GameState.Start:
                    
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        
                        currenGameState = GameState.Playing;
                    }
                    break;
                case GameState.Playing:
                    if (orangeList.Count==0)
                    {
                        currenGameState = GameState.Win;
                        
                    }
                    break;
                case GameState.GameOver:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        Exit();
                    }
                        break;
                     case GameState.Win:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        Exit();
                    }

                        break;
            }
            foreach (Enemy enemy in enemyList)
            {
                enemy.Update(gameTime);
            }


            if (currenGameState == GameState.Playing)
            {
                pacman.Update(gameTime);
                
                foreach(Enemy enemy in enemyList)
                {
                    if (enemy.dmgHitBox.Intersects(pacman.dmgHitBox))
                    {

                        enemyList.Remove(enemy);
                        lives--;
                        deathSound.Play();

                        
                        


                        
                      break;
                        
                    }

                    if (lives == 0)
                    {
                        currenGameState = GameState.GameOver;
                    }    

                    
                }
              
                foreach (Orange ornage in orangeList)
                {
                    

                    
                    if (pacman.dmgHitBox.Intersects(ornage.hitBox))
                    {

                        orangeList.Remove(ornage);
                        
                        oranges--;
                        nomSound.Play();
                        break;
                    }

                    
                }

                foreach (SuperOrange superOrange in superOrangesList)
                {

                    if (pacman.hitBox.Intersects(superOrange.hitBox))
                    {
                        superOrangesList.Remove(superOrange);
                        lives++;
                        
                        break;
                    }



                }


            }




            




                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if (currenGameState == GameState.Start)
            {
                _spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);

            }

            if (currenGameState == GameState.Playing)
            {
                for (int i = 0; i < tiles.GetLength(0); i++)
                {
                    for (int j = 0; j < tiles.GetLength(1); j++)
                    {

                        tiles[i, j].Draw(_spriteBatch);




                    }
                }

                foreach (Orange orange in orangeList)
                {
                    orange.Draw(_spriteBatch);
                }
                foreach(SuperOrange superOrange in superOrangesList)
                {
                    superOrange.Draw(_spriteBatch);
                }
                foreach (Enemy enemy in enemyList)
                {
                    
                    enemy.Draw(_spriteBatch);
                }

                _spriteBatch.DrawString(scorefont,"Oranges Left: "+oranges.ToString(),Vector2.Zero, Color.DarkOrange);
                _spriteBatch.DrawString(scorefont,"Lives: "+lives.ToString(),new Vector2(0,50), Color.DarkOrange);
                pacman.Draw(_spriteBatch);

                
                
            }

            if (currenGameState == GameState.GameOver)
            {
                _spriteBatch.Draw(loseTexture, Vector2.Zero, Color.White);
            }
            if (currenGameState == GameState.Win)
            {
                _spriteBatch.Draw(winTexture, Vector2.Zero, Color.White);
            }


            

            _spriteBatch.End();
            base.Draw(gameTime);
        }
        public static bool GetTileAtPosition(Vector2 vec)
        {
            return tiles[(int)vec.X / 32, (int)vec.Y / 32].walkable;
        }


    }
}

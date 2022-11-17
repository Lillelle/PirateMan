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
        
        
        int lives = 3;
        List<Orange> orangeList;
        List<Enemy> enemyList;
        List<SuperOrange> superOrangesList;
        public Rectangle screenSize;
        
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

            LoadAssets.LoadContent(Content);

            LevelManager lvm = new LevelManager();
            lvm.LoadLevel();
            currenGameState = GameState.Start;

            enemyList = LevelManager.enemyList;
            superOrangesList = LevelManager.superOrangesList;
            orangeList = LevelManager.orangeList;
            pacman=LevelManager.pacman;
            tiles = LevelManager.tiles;
            
            
            
            
            
           
            



            
        

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
            foreach (Enemy enemy in LevelManager.enemyList)
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
                        LoadAssets.deathSound.Play();

                        
                        


                        
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
                        
                        LevelManager.oranges--;
                        LoadAssets.nomSound.Play();
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
                _spriteBatch.Draw(LoadAssets.backgroundTexture, Vector2.Zero, Color.White);

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

                _spriteBatch.DrawString(LoadAssets.scorefont,"Oranges Left: "+LevelManager.oranges.ToString(),Vector2.Zero, Color.DarkOrange);
                _spriteBatch.DrawString(LoadAssets.scorefont,"Lives: "+lives.ToString(),new Vector2(0,50), Color.DarkOrange);
                pacman.Draw(_spriteBatch);

                
                
            }

            if (currenGameState == GameState.GameOver)
            {
                _spriteBatch.Draw(LoadAssets.loseTexture, Vector2.Zero, Color.White);
            }
            if (currenGameState == GameState.Win)
            {
                _spriteBatch.Draw(LoadAssets.winTexture, Vector2.Zero, Color.White);
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

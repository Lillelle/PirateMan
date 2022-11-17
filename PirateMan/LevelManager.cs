using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirateMan
{
     class LevelManager 
    {


        public static Tile[,] tiles;
        public static PacMan pacman;
        public static List<Enemy> enemyList;
        public static List<Orange> orangeList;
        public static List<SuperOrange> superOrangesList;
        public static int oranges;
        LoadAssets loadAssets;
        StreamReader sr;
        
        public void LoadLevel()
        {


            enemyList = new List<Enemy>();
            superOrangesList = new List<SuperOrange>();
            orangeList = new List<Orange>();

            sr = new StreamReader("../../../Content/Map.txt");
            List<string> strings = new List<string>();
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
                        tiles[i, j] = new Tile(new Vector2(LoadAssets.crateTexture.Width * i,LoadAssets.crateTexture.Height * j), LoadAssets.crateTexture, false);
                        orangeList.Add(new Orange(new Vector2(LoadAssets.waterTexture.Width * i, LoadAssets.waterTexture.Width * j), LoadAssets.orangeTexture));

                    }
                    else if (strings[j][i] == 'W')
                    {
                        tiles[i, j] = new Tile(new Vector2(LoadAssets.waterTexture.Width * i, LoadAssets.waterTexture.Height * j), LoadAssets.waterTexture, true);
                    }
                    else if (strings[j][i] == 'P')
                    {
                        tiles[i, j] = new Tile(new Vector2(LoadAssets.crateTexture.Width * i, LoadAssets.crateTexture.Height * j), LoadAssets.crateTexture, false);
                        pacman = new PacMan(new Vector2(LoadAssets.crateTexture.Width * i, LoadAssets.crateTexture.Width * j), LoadAssets.playerTexture);
                    }
                    else if (strings[j][i] == 'E')
                    {
                        tiles[i, j] = new Tile(new Vector2(LoadAssets.crateTexture.Width * i, LoadAssets.crateTexture.Height * j), LoadAssets.crateTexture, false);
                        enemyList.Add(new Enemy(new Vector2(LoadAssets.crateTexture.Width * i, LoadAssets.crateTexture.Height * j), LoadAssets.enemyTexure));
                        orangeList.Add(new Orange(new Vector2(LoadAssets.waterTexture.Width * i, LoadAssets.waterTexture.Width * j), LoadAssets.orangeTexture));


                    }
                    else if (strings[j][i] == 'F')
                    {
                        tiles[i, j] = new Tile(new Vector2(LoadAssets.crateTexture.Width * i, LoadAssets.crateTexture.Height * j), LoadAssets.crateTexture, false);

                        superOrangesList.Add(new SuperOrange(new Vector2(LoadAssets.waterTexture.Width * i, LoadAssets.waterTexture.Width * j), LoadAssets.orangeTexture));


                    }



                }
            }

            oranges = orangeList.Count;

        }


    }
}

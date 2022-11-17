using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirateMan
{
    public class LoadAssets
    {
        public static SpriteFont scorefont;
        public static SoundEffect nomSound;
        public static Texture2D backgroundTexture;
        public static Texture2D winTexture;
        public static Texture2D loseTexture;
        public static Song song;
        public static SoundEffect deathSound;
        public static Texture2D hitBoxTexture;
        public static Texture2D crateTexture;
        public static Texture2D waterTexture;
        public static Texture2D playerTexture;
        public static Texture2D orangeTexture;
        public static Texture2D enemyTexure;

        public static void LoadContent(ContentManager content)
        {
            crateTexture = content.Load<Texture2D>("crate");
            waterTexture = content.Load<Texture2D>("Water");
            playerTexture = content.Load<Texture2D>("classic-pegleg");
            orangeTexture = content.Load<Texture2D>("Orange");
            enemyTexure = content.Load<Texture2D>("Pirate1 (Walk)");
            scorefont = content.Load<SpriteFont>("ScoreFont");
            backgroundTexture = content.Load<Texture2D>("PirateBackground");
            winTexture = content.Load<Texture2D>("YouWin");
            loseTexture = content.Load<Texture2D>("YouLose");
            song = content.Load<Song>("Song");
            hitBoxTexture = content.Load<Texture2D>("water");
            nomSound = content.Load<SoundEffect>("Nom");
            deathSound = content.Load<SoundEffect>("1yell3");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;


        }

    }
}

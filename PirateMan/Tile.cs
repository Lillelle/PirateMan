using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirateMan
{
    class Tile : GameObject
    {

        public bool walkable;



        public Tile(Vector2 drawPos, Texture2D texture, bool walkable) : base(drawPos, texture)
        {

            this.walkable = walkable;


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, drawPos, Color.LightBlue);
        }
    }
}

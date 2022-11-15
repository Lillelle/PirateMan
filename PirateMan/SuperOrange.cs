using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirateMan
{
    
    internal class SuperOrange:GameObject
    {
        public Rectangle hitBox;

        public SuperOrange(Vector2 drawPos, Texture2D texture) : base(drawPos, texture)
        {

            hitBox = new Rectangle((int)drawPos.X, (int)drawPos.Y, texture.Width, texture.Height);

        }



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, drawPos, Color.Red);
            //spriteBatch.Draw(texture, hitBox, Color.Red);

        }
    }
}


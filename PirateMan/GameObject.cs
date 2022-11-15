using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirateMan
{
    class GameObject
    {
        protected Vector2 drawPos;
        public Texture2D texture;

        public GameObject(Vector2 drawPos, Texture2D texture)
        {
            this.drawPos = drawPos;
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, drawPos, Color.White);
        }
    }
}

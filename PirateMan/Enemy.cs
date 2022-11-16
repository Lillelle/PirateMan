using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PirateMan
{
    internal class Enemy : GameObject
    {

        Random rnd = new Random();

        float speed;
        

        public Rectangle hitBox;
        public Rectangle dmgHitBox;
        AnimationClip walkClip;
        AnimationClip currentClip;
        Vector2 startPos;
        
        int tileSize = 32;
        double timer;
        int randomNr;
        enum EnemyState
        {
            walkingRight,
            walikingLeft,
            
            
        }
        EnemyState currentEnemyState;
        Rectangle[] walkRects = new Rectangle[12];



        public Enemy(Vector2 drawPos, Texture2D texture) : base(drawPos, texture)
        {
            hitBox = new Rectangle((int)drawPos.X, (int)drawPos.Y, 32,32);
            dmgHitBox = new Rectangle((int)drawPos.X, (int)drawPos.Y, 20, 20);
            startPos = new Vector2(drawPos.X, drawPos.Y);
            walkRects = new Rectangle[]
            {
                new Rectangle(32*0,0,32,32),
                new Rectangle(32*1,0,32,32),
                new Rectangle(32*2,0,32,32),
                new Rectangle(32*3,0,32,32),
                new Rectangle(32*4,0,32,32),
                new Rectangle(32*5,0,32,32),
                new Rectangle(32*6,0,32,32),
                new Rectangle(32*7,0,32,32),
                new Rectangle(32*8,0,32,32),
                new Rectangle(32*9,0,32,32),
                new Rectangle(32*10,0,32,32),
                new Rectangle(32*11,0,32,32),
            };

            walkClip = new AnimationClip(walkRects, 7.5f);
            currentEnemyState= EnemyState.walkingRight;
            currentClip = walkClip;
            speed = rnd.Next();
            randomNr = rnd.Next();
            timer = 0;
            
        }

        public void Update(GameTime gameTime)
        {
            hitBox.Y = (int)drawPos.Y;
            hitBox.X = (int)drawPos.X;
            dmgHitBox.X = (int)drawPos.X+6;
            dmgHitBox.Y = (int)drawPos.Y+8;
            speed = rnd.Next(1,3);
            randomNr = rnd.Next(1, 200);
            timer = gameTime.ElapsedGameTime.TotalSeconds;
           
            switch (currentEnemyState)
            {
                case EnemyState.walkingRight:
                    drawPos.X = drawPos.X + speed;

                   

                    if (drawPos.X >= startPos.X + tileSize * 10 || randomNr == 5)
                    {
                        currentEnemyState=EnemyState.walikingLeft;
                        

                    }
                    
                    break;
                    case EnemyState.walikingLeft:


                    drawPos.X = drawPos.X - speed;
                    if (drawPos.X <= startPos.X)
                    {

                        currentEnemyState = EnemyState.walkingRight;


                    }
                    break;
                   
                    






            }

            

            currentClip = walkClip;

            currentClip.SetPlay();

            currentClip.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destRect = new Rectangle(hitBox.X, hitBox.Y, 32, 32);

            if (currentEnemyState == EnemyState.walkingRight)
            {
                spriteBatch.Draw(texture, destRect, currentClip.GetCurrentSourceRectangle(), Color.White);
            }


            if (currentEnemyState == EnemyState.walikingLeft)
            {
                spriteBatch.Draw(texture, destRect, currentClip.GetCurrentSourceRectangle(), Color.White,0,Vector2.Zero, SpriteEffects.FlipHorizontally, 0.5f);
            }

            


        }
    }
}

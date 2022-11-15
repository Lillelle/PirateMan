using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirateMan
{
    internal class PacMan : GameObject
    {
        
        Vector2 direction, destination;
        bool moving;
        float speed = 100.0f;
        public Rectangle hitBox;
        public Vector2 startPos;
        Rectangle[] walkRects = new Rectangle[6];
        AnimationClip walkClip;
        AnimationClip currentClip;
        
        
        
        public PacMan(Vector2 drawPos, Texture2D texture) : base(drawPos, texture)
        {
            hitBox = new Rectangle((int)drawPos.X-16, (int)drawPos.Y, 16, 16);

            walkRects = new Rectangle[]
            {
                new Rectangle(40+2,30*0+1,40,29),
                new Rectangle(40+2,30*1+1,40,29),
                new Rectangle(40+2,30*2+1,40,29),
                new Rectangle(40+2,30*3+1,40,29),
                new Rectangle(40+2,30*4+1,40,29),
                new Rectangle(40+2,30*5+1,40,29),
            };
            
            walkClip = new AnimationClip(walkRects, 7.5f);
            currentClip = walkClip;
            startPos=drawPos;
        }
           

        public void ChangeDirection(Vector2 dir)
        {
            direction = dir;
            Vector2 newDestination = drawPos + direction * 32.0f;

            if (!Game1.GetTileAtPosition(newDestination) && (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Down)))
            {

                destination = newDestination;
                moving = true;
            }
        }









        public void Update(GameTime gameTime)
        {

            

            if (!moving)
            {

                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    ChangeDirection(new Vector2(-1, 0));
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    ChangeDirection(new Vector2(1, 0));
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    ChangeDirection(new Vector2(0, -1));
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    ChangeDirection(new Vector2(0, 1));
                }
            }
            else
            {
                drawPos += direction * speed *
                (float)gameTime.ElapsedGameTime.TotalSeconds;


                currentClip = walkClip;

                currentClip.SetPlay();

                currentClip.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

             

                if (Vector2.Distance(drawPos, destination) < 1)
                {
                    drawPos = destination;
                    moving = false;
                }
            }

            hitBox.Y = (int)drawPos.Y;
            hitBox.X = (int)drawPos.X;

        }
           
            



        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destRect = new Rectangle(hitBox.X,hitBox.Y, 40, 29);

            
            if(!Keyboard.GetState().IsKeyDown(Keys.Right) && (!Keyboard.GetState().IsKeyDown(Keys.Left) && (!Keyboard.GetState().IsKeyDown(Keys.Up)&& (!Keyboard.GetState().IsKeyDown(Keys.Down)))))
            {
                spriteBatch.Draw(texture, destRect, currentClip.GetCurrentSourceRectangle(), Color.White);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left)&& (!Keyboard.GetState().IsKeyDown(Keys.Up)&&(!Keyboard.GetState().IsKeyDown(Keys.Down))))
            {

                spriteBatch.Draw(texture, destRect, currentClip.GetCurrentSourceRectangle(), Color.White,0,Vector2.Zero,SpriteEffects.FlipHorizontally,0.5f);

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right)| Keyboard.GetState().IsKeyDown(Keys.Up)| Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                spriteBatch.Draw(texture, destRect, currentClip.GetCurrentSourceRectangle(), Color.White);

            }
            


        }
    }
}
            
            

        













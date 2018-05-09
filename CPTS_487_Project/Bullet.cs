using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CPTS_487_Project
{
    public class SingleBullet : Entity
    {
        Vector2 playerPosition;
        public bool fireSignal = false;
        public string bulletType;
        public float timeFrame;
        private int current = 1;
        private List<int> passes;
        public List<string> leftOrRight;
        private int time = 0;
        private int offset = 7;
        private int offset1 = 4;

        public SingleBullet()
        {
            Velocity = Vector2.Zero;
            playerPosition = Vector2.Zero;
            passes = new List<int>();
            leftOrRight = new List<string>();
            for (int i = 0; i < 23; i++)
            {
                leftOrRight.Add(string.Empty);
            }
            for (int i = 0; i < 23; i++)
            {
                passes.Add(1);
            }
        }

        public void setTargetPosition(Vector2 player)
        {
            playerPosition = player;
        }

        public override void LoadContent()
        {
            foreach (Image im in image)
            {
                im.LoadContent();
                im.deactivateEffect("Fade");
            }
        }
        public override void UnloadContent()
        {
            foreach (Image im in image)
            {
                im.UnloadContent();
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (fireSignal == true)
            {
                for (int i = 0; i < current; i++)
                {
                   
                    normalHorizontalLineShoot(gameTime, i);
                    
                }
            }
        }


        private void normalHorizontalLineShoot(GameTime gameTime, int i)
        {

            if (i == 0 && image[i].Position.Y > 180)
            {
                image[i].Position.Y += Velocity.Y = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[i + 1].Position.Y += Velocity.Y = MoveSpeed / (offset1-0.2f) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[i + 2].Position.Y += Velocity.Y = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                spiralMove(gameTime, i,5);
                spiralMove(gameTime, i + 1, 5);
                spiralMove(gameTime, i + 2, 5);
                current = current + 3;
            }
            else if ((i == 3 || i == 7 || i == 11 || i == 15 || i == 19) && image[i].Position.Y > 180)
            {
                
                image[i + 1].Position.Y += Velocity.Y = MoveSpeed / (offset1) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[i + 2].Position.Y += Velocity.Y = MoveSpeed / (offset1) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (image[i+1].Position.Y > 200)
                {
                    image[i].Position.Y += Velocity.Y = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[i + 3].Position.Y += Velocity.Y = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                spiralMove(gameTime, i, 20);
                spiralMove(gameTime, i + 1, 20);
                spiralMove(gameTime, i + 2, 20);
                spiralMove(gameTime, i + 3, 20);
            }
            if(i < 23 && image[i].Position.Y > 650)
            {
                if (i + 3 == 22)
                {
                    for(int j = 0; j < 23; j++)
                    {
                        image[j].Position = new Vector2(-50, -50);
                        leftOrRight[j] = string.Empty;
                        passes[j] = 1;
                    }
                    current = 1;
                }
            }
        }

        private void spiralMove(GameTime gameTime, int i, int radius)
        {
            if (passes[i] == 1)
            {
                if (image[i].Position.X < playerPosition.X && leftOrRight[i] == "left")
                {
                    image[i].Position.X += MoveSpeed / offset * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (image[i].Position.X > playerPosition.X)
                    {
                        passes[i] = 2;
                    }
                }
                else if (image[i].Position.X > playerPosition.X && leftOrRight[i] == "right")
                {
                    image[i].Position.X -= MoveSpeed / offset * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (image[i].Position.X < playerPosition.X)
                    {
                        passes[i] = 2;
                    }
                }
            }
            else if (passes[i] == 2)
            {
                if (image[i].Position.X < playerPosition.X + radius && leftOrRight[i] == "left")
                {
                    image[i].Position.X += MoveSpeed / offset * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (image[i].Position.X > playerPosition.X + radius)
                    {
                        passes[i] = 3;
                    }
                }
                else if (image[i].Position.X > playerPosition.X - radius && leftOrRight[i] == "right")
                {
                    image[i].Position.X -= MoveSpeed / offset * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (image[i].Position.X < playerPosition.X - radius)
                    {
                        passes[i] = 3;
                    }
                }
            }
            else if (passes[i] == 3)
            {
                if (image[i].Position.X > playerPosition.X && leftOrRight[i] == "left")
                {
                    image[i].Position.X -= MoveSpeed / offset * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (image[i].Position.X < playerPosition.X)
                    {
                        passes[i] = 4;
                    }
                }
                else if (image[i].Position.X < playerPosition.X && leftOrRight[i] == "right")
                {
                    image[i].Position.X += MoveSpeed / offset * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (image[i].Position.X > playerPosition.X)
                    {
                        passes[i] = 4;
                    }
                }
            }
            else if (passes[i] == 4)
            {
                if (image[i].Position.X > playerPosition.X - radius && leftOrRight[i] == "left")
                {
                    image[i].Position.X -= MoveSpeed / offset * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (image[i].Position.X < playerPosition.X - radius)
                    {
                        passes[i] = 1;
                    }
                }
                else if (image[i].Position.X < playerPosition.X + radius && leftOrRight[i] == "right")
                {
                    image[i].Position.X += MoveSpeed / offset * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (image[i].Position.X > playerPosition.X + radius)
                    {
                        passes[i] = 1;
                    }
                }
            }
        }
        /*
        if (passes[i] == false && passes[i + 2] == false)
        {
            if (image[i].Position.X > playerPosition.X)
            {
                image[i].Position.X -= MoveSpeed / 16 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (image[i].Position.X < playerPosition.X)
            {
                image[i].Position.X += MoveSpeed / 16 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (image[i].Position.X < playerPosition.X)
            {
                image[i].Position.X += MoveSpeed / 16 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (image[i + 1].Position.X > playerPosition.X)
            {
                image[i + 1].Position.X -= MoveSpeed / 16 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (image[i + 1].Position.X < playerPosition.X)
            {
                image[i + 1].Position.X += MoveSpeed / 16 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (image[i + 2].Position.X > playerPosition.X)
            {
                image[i + 2].Position.X -= MoveSpeed / 16 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (image[i + 2].Position.X < playerPosition.X)
            {
                image[i + 2].Position.X += MoveSpeed / 16 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            /*
            if (image[1].Position.X > image[i+2].Position.X)
            {
                passes[i] = true;
                passes[i + 2] = true;
            }
        }*/
        /*
        else
        {
            if (image[i].Position.X < playerPosition.X + 50)
            {
                image[i].Position.X += MoveSpeed / 16 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if(image[i+2].Position.X > playerPosition.X - 50)
            {
                image[i+1].Position.X -= MoveSpeed / 16 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            /*
            if(image[i].Position.X == playerPosition.X + 50)
            {
                passes[i] = false;
                passes[i + 2] = false;
            }
        }

        if (current < 23 && image[current - 1].Position.Y > 200)
        {
            current = current + 3;
        }


    }
    /*
    else if( (i == 3 || i == 7 || i == 11 || i == 15 || i == 19) && image[i].Position.Y > 180)
    {
        image[i].Position.Y += Velocity.Y = MoveSpeed/4 * (float)gameTime.ElapsedGameTime.TotalSeconds; 
        image[i + 1].Position.Y += Velocity.Y = MoveSpeed/4 * (float)gameTime.ElapsedGameTime.TotalSeconds; 
        image[i + 2].Position.Y += Velocity.Y = MoveSpeed/4 * (float)gameTime.ElapsedGameTime.TotalSeconds; 
        image[i + 3].Position.Y += Velocity.Y = MoveSpeed/4 * (float)gameTime.ElapsedGameTime.TotalSeconds; 
        if (image[i].Position.X > playerPosition.X)
        {
            image[i].Position.X -= MoveSpeed / 16 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else if (image[i].Position.X < playerPosition.X)
        {
            image[i].Position.X += MoveSpeed / 16 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        if (image[i + 1].Position.X > playerPosition.X)
        {
            image[i + 1].Position.X -= MoveSpeed / 16 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else if (image[i + 1].Position.X < playerPosition.X)
        {
            image[i + 1].Position.X += MoveSpeed / 16 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        if (image[i + 2].Position.X > playerPosition.X)
        {
            image[i + 2].Position.X -= MoveSpeed / 16 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else if (image[i + 2].Position.X < playerPosition.X)
        {
            image[i + 2].Position.X += MoveSpeed / 16 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        if (image[i + 3].Position.X > playerPosition.X)
        {
            image[i + 3].Position.X -= MoveSpeed / 16 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else if (image[i + 3].Position.X < playerPosition.X)
        {
            image[i + 3].Position.X += MoveSpeed / 16 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        if (current < 24 && image[current - 1].Position.Y > 200)
        {
            current = current + 4;
        }
    }
   */
        //}

        private void enterScreen(GameTime gameTime, int i)
        {
            if (i == 0)
            {
                image[i].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[i + 1].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[i + 2].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                image[i].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[i + 1].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[i + 2].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[i + 3].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Image im in image)
            {
                im.Draw(spriteBatch);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CPTS_487_Project
{
    public class TriangleBullet : Entity
    {
        Vector2 playerPosition;
        public bool fireSignal = false;
        public string bulletType;
        public float timeFrame;
        private int current = 6;
        private int offset = 50;
        private int offset1 = 2;
        public bool setted = false;
        public int tCurrent;

        public TriangleBullet()
        {
            Velocity = Vector2.Zero;
            playerPosition = Vector2.Zero;
            tCurrent = 6;
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
            if (fireSignal == true && setted == true)
            {
                for (int i = 0; i < current; i++)
                {
                    shoot(gameTime, i);

                }
            }
        }

        private void shoot(GameTime gameTime, int i)
        {
            if (i == 0 && image[5].Position.Y < 650 || i != 0 && image[i - 1].Position.Y - image[i].Position.Y > 20)
            {
                if (i == 0)
                {
                    image[i].Position.Y += Velocity.Y = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (i == 1)
                {
                    image[i].Position.Y += Velocity.Y = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[i + 1].Position.Y += Velocity.Y = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (image[i].Position.X > image[i - 1].Position.X - 10)
                    {
                        image[i].Position.X -= Velocity.X = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (image[i].Position.X < image[i - 1].Position.X - 10)
                    {
                        image[i].Position.X += Velocity.X = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    if (image[i + 1].Position.X < image[i - 1].Position.X + 10)
                    {
                        image[i + 1].Position.X += Velocity.X = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (image[i + 1].Position.X > image[i - 1].Position.X + 10)
                    {
                        image[i + 1].Position.X -= Velocity.X = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
                else if (i == 3)
                {
                    image[i].Position.Y += Velocity.Y = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[i + 1].Position.Y += Velocity.Y = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[i + 2].Position.Y += Velocity.Y = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (image[i].Position.X > image[0].Position.X - 20)
                    {
                        image[i].Position.X -= Velocity.X = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (image[i].Position.X < image[0].Position.X - 20)
                    {
                        image[i].Position.X += Velocity.X = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    if (image[i + 1].Position.X < image[0].Position.X)
                    {
                        image[i + 1].Position.X += Velocity.X = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (image[i + 1].Position.X > image[0].Position.X)
                    {
                        image[i + 1].Position.X -= Velocity.X = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    if (image[i + 2].Position.X < image[0].Position.X + 20)
                    {
                        image[i + 2].Position.X += Velocity.X = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (image[i + 2].Position.X > image[0].Position.X + 20)
                    {
                        image[i + 2].Position.X -= Velocity.X = MoveSpeed / offset1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
                if (image[0].Position.X > playerPosition.X)
                {
                    image[0].Position.X -= Velocity.X = MoveSpeed / 30 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[4].Position.X -= Velocity.X = MoveSpeed / 30 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (image[0].Position.X < playerPosition.X)
                {
                    image[0].Position.X += Velocity.X = MoveSpeed / 30 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[4].Position.X += Velocity.X = MoveSpeed / 30 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
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

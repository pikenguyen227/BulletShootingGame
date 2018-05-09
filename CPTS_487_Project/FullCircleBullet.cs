using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Project
{
    public class FullCircleBullet : Entity
    {
        Vector2 playerPosition;

        double angle = 0;

        public bool fireSignal = false;
        public string bulletType;
        public float timeFrame;
        private int current = 1;
        public bool setted = false;
        private int iterator = 0;
        public FullCircleBullet()
        {
            Velocity = Vector2.Zero;
            playerPosition = Vector2.Zero;
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
            if (setted == true)
            {
                shoot(gameTime);
            }

        }

        private void shoot(GameTime gameTime)
        {

            while ((float)angle < (float)(2 * Math.PI))
            {
                float x = (float)(5 * Math.Cos(angle));
                float y = (float)(5 * Math.Sin(angle));
                image[iterator].Position.X += Velocity.X = x * MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[iterator].Position.Y -= Velocity.Y = y * MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                iterator++;
                angle = angle + Math.PI / 8;
            }
            iterator = 0;
            angle = 0;
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


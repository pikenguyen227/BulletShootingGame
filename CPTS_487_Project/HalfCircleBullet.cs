using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace CPTS_487_Project
{
    public class HalfCircleBullet : Entity
    {
        Vector2 playerPosition;
        
        double angle = 13 * Math.PI / 12;
        double tempangle = 5 * Math.PI / 4;
        public bool fireSignal = false;
        public string bulletType;
        public float timeFrame;
        private int current = 1;
        public bool setted = false;
        private int iterator = 0;
        public HalfCircleBullet()
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
            while ((float)angle <= (float)(23 * Math.PI / 12))
            {
                float x = (float)(5 * Math.Cos(angle));
                float y = (float)(5 * Math.Sin(angle));
                image[iterator].Position.X += Velocity.X = x * MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                image[iterator].Position.Y -= Velocity.Y = y * MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                iterator++;
                angle = angle + Math.PI / 6;
            }
            iterator = 0;
            angle = 13 * Math.PI / 12;
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

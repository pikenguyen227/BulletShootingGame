using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace CPTS_487_Project
{
    public class MidBoss : Entity
    {
        // add left/right/lower/upper bounds later
        private float timer = 0;
        private bool enter = false;
        private bool isDown = false;
        private bool isLeft = false;
        private bool isRight = false;
        private bool goDown = false;
        private bool goLeft = false;
        private bool goRight = false;
        private bool exit = false;

        public MidBoss()
        {
            Velocity = Vector2.Zero;
        }

        public Vector2 getPosition(int i)
        {
            return image[0].Position;
        }

        public override void LoadContent()
        {
            if (image.Count != 0)
            {
                image[0].LoadContent();
                image[0].deactivateEffect("Fade");
            }
        }

        public override void UnloadContent()
        {
            image[0].UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // update timer
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // decides when to move midboss
            if (timer <= 2 && isRight == false)
                enter = true;
            // cycle 1
            if (timer > 2 && timer <= 4 && isDown == false)
                goDown = true;
            if (timer > 4 && timer <= 6 && isLeft == false)
                goLeft = true;
            if (timer > 6 && timer <= 8 && isRight == false)
                goRight = true;
            // cycle 2
            if (timer > 8 && timer <= 10 && isDown == false)
                goDown = true;
            if (timer > 10 && timer <= 12 && isLeft == false)
                goLeft = true;
            if (timer > 12 && timer <= 14 && isRight == false)
                goRight = true;
            // cycle 3
            if (timer > 14 && timer <= 16 && isDown == false)
                goDown = true;
            if (timer > 16 && timer <= 18 && isLeft == false)
                goLeft = true;
            if (timer > 18 && timer <= 20 && isRight == false)
                goRight = true;
            // cycle 4
            if (timer > 20 && timer <= 22 && isDown == false)
                goDown = true;
            if (timer > 22 && timer <= 24 && isLeft == false)
                goLeft = true;
            if (timer > 24 && timer <= 26 && isRight == false)
                goRight = true;
            if (timer > 26 && isRight == true)
                exit = true;

            // call enter function
            if (enter == true)
            {
                enterMove(gameTime);
            }
            // call downMove function
            if (goDown == true)
            {
                isRight = false;
                downMove(gameTime);
            }
            // call leftMove function
            else if (goLeft == true)
            {
                isDown = false;
                leftMove(gameTime);
            }
            // call rightMove function
            else if (goRight == true)
            {
                isLeft = false;
                rightMove(gameTime);
            }
            else if (exit == true)
            {
                isRight = false;
                exitMove(gameTime);
            }
            // update image
            image[0].Update(gameTime);
        }
        // moves the midboss to the lower middle position from the upper right
        private void downMove(GameTime gameTime)
        {
            image[0].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            image[0].Position.X -= 3;
            if (image[0].Position.X <= 250)
            {
                goDown = false;
                isDown = true;
            }
        }
        // moves the midboss to the upper left position from the lower middle
        private void leftMove(GameTime gameTime)
        {
            image[0].Position.Y += Velocity.Y = -(MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            image[0].Position.X -= 3;
            if (image[0].Position.X <= 150)
            {
                goLeft = false;
                isLeft = true;
            }
        }
        // moves the midboss to the upper right position from the upper left
        private void rightMove(GameTime gameTime)
        {
            image[0].Position.X += Velocity.X = 2.3f * MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (image[0].Position.X >= 350)
            {
                goRight = false;
                isRight = true;
            }
        }
        // initial function for midboss entering the screen
        private void enterMove(GameTime gameTime)
        {
            image[0].Position.Y += Velocity.Y = 2 * MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            image[0].Position.X += 3;
            if (image[0].Position.X >= 350)
            {
                enter = false;
                isRight = true;
            }
        }
        // final function for midboss leaving the screen once time runs out
        private void exitMove(GameTime gameTime)
        {
            image[0].Position.Y += Velocity.Y = -2 * MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            image[0].Position.X -= 3;
            if (image[0].Position.Y <= -100)
            {
                exit = false;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            image[0].Draw(spriteBatch);
        }
    }
}
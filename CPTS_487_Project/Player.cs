using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CPTS_487_Project
{
    public class Player : Entity
    {
        public Player()
        {
            Velocity = Vector2.Zero;
    
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
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (image[0].Position.Y < ScreenManager.Instance.Dimensions.Y - image[0].SourceRect.Height)
                {
                    image[0].Position.Y += Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (image[0].Position.Y >= image[0].SourceRect.Height)
                {
                    image[0].Position.Y += Velocity.Y = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            else
            {
                Velocity.Y = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (image[0].Position.X <= ScreenManager.Instance.Dimensions.X - image[0].SourceRect.Width)
                {
                    image[0].Position.X += Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[0].SpriteSheetEffect.CurrentFrame.Y = 2;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (image[0].Position.X >= image[0].SourceRect.Width)
                {
                    image[0].Position.X += Velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image[0].SpriteSheetEffect.CurrentFrame.Y = 1;
                }
            }
            else
            {
                Velocity.X = 0;
                image[0].SpriteSheetEffect.CurrentFrame.Y = 0;
            }

            /**
            if (Velocity.X == 0 && Velocity.Y == 0)
            {
                Image.IsActice = false;
            }*/

            image[0].Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            image[0].Draw(spriteBatch);
        }
        public Vector2 getPosition(int i)
        {
            return image[i].Position;
        }
    }
}

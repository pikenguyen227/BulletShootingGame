﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace CPTS_487_Project.Effect
{
    public class SpriteSheetEffectEnemy : ImageEffect
    {
        public int FrameCounter;
        public int SwitchFrame;
        public Vector2 CurrentFrame;
        public Vector2 AmountOfFrames;
        public int FrameWidth
        {
            get
            {
                if (image.Texture != null)
                {
                    return image.Texture.Width / (int)AmountOfFrames.X;
                }
                return 0;
            }
        }

        public int FrameHeight
        {
            get
            {
                if (image.Texture != null)
                {
                    return image.Texture.Height / (int)AmountOfFrames.Y;
                }
                return 0;
            }
        }

        public SpriteSheetEffectEnemy()
        {
            AmountOfFrames = new Vector2(3, 4);
            CurrentFrame = new Vector2(0, 0);
            SwitchFrame = 100;
            FrameCounter = 0;
        }

        public override void LoadContent(ref Image Image)
        {
            base.LoadContent(ref Image);
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (image.IsActice)
            {
                FrameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (FrameCounter >= SwitchFrame)
                {
                    FrameCounter = 0;
                    CurrentFrame.X++;
                    if (CurrentFrame.X * FrameWidth >= image.Texture.Width)
                    {
                        CurrentFrame.X = 0;
                    }
                }

            }
            else
            {
                CurrentFrame.X = 0;
            }
            image.SourceRect = new Rectangle((int)CurrentFrame.X * FrameWidth, (int)CurrentFrame.Y * FrameHeight, FrameWidth, FrameHeight);

        }
    }
}


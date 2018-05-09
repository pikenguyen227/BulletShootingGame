﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace CPTS_487_Project.Effect
{
    public class Fade : ImageEffect
    {
        public float fadeSpeed;
        public bool Increase;

        public Fade()
        {
            fadeSpeed = 50;
            Increase = false;
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
            if(image != null && image.IsActice)
            {
                if(!Increase)
                {
                    image.Alpha -= fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    image.Alpha += fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                if(image.Alpha < 0.0f)
                {
                    Increase = true;
                    image.Alpha = 0.0f;
                }
                else if(image.Alpha > 1.0f)
                {
                    Increase = false;
                    image.Alpha = 1.0f;
                }
            }
            else if(!image.IsActice)
            {
                Increase = false;
                image.Alpha = 1.0f;
            }
        }
    }
}

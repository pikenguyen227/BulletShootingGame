using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Microsoft.Xna.Framework.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CPTS_487_Project
{
     public class SplashScreen: GameScreen
    {
        public Image Image;
        private float timer = 0;
        public override void LoadContent()
        {
            base.LoadContent();
            Image.LoadContent();
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
            Image.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
            Image.Update(gameTime);
           
            if(timer > 3 && !ScreenManager.Instance.IsTransitioning)
            {
                ScreenManager.Instance.ChangeScreens("TitleScreen");
                timer = 0;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            /**
            Vector2 location = new Vector2(40f, 241f);
            spriteBatch.DrawString(content.Load<SpriteFont>("Font/logo"),"Team No Name",location,Color.Red);
            location = new Vector2(40f, 250f);
            spriteBatch.DrawString(content.Load<SpriteFont>("Font/logo"), "_________________", location, Color.White);
            location = new Vector2(40f, 190f);
            spriteBatch.DrawString(content.Load<SpriteFont>("Font/logo"), "_________________", location, Color.White);*/
            Image.Draw(spriteBatch);
        }
    }
}

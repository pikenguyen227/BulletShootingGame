using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Timers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CPTS_487_Project
{
    public class ScreenManager
    {
        public Image Image;
        public string Effects;

        private static ScreenManager instance;
        [XmlIgnore]
        public Vector2 Dimensions { private set; get; }
        [XmlIgnore]
        public ContentManager Content { private set; get; }
        XMLManager<GameScreen> xmlGameScreenManager;

        GameScreen currentScreen, newScreen;
        [XmlIgnore]
        public GraphicsDevice GraphicsDevice;
        [XmlIgnore]
        public SpriteBatch SpriteBatch;

        [XmlIgnore]
        public bool IsTransitioning { get; private set; }
        [XmlIgnore]
        public Effect.Fade fadeEffect;

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    XMLManager<ScreenManager> xml = new XMLManager<ScreenManager>();
                    instance = xml.Load("Content/Load/ScreenManager.xml");
                }
                return instance;
            }
        }

        public void ChangeScreens(string screenName)
        {
            newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("CPTS_487_Project." + screenName));
            Image.IsActice = true;
            Image.fadeEffect.Increase = true;
            Image.Alpha = 0.0f;
            IsTransitioning = true;
        }

        private void Transition(GameTime gameTime)
        {
            if(IsTransitioning)
            {
                Image.Update(gameTime);
                if(Image.Alpha == 1.0f)
                {
                    currentScreen.UnloadContent();
                    currentScreen = newScreen;
                    xmlGameScreenManager.Type = currentScreen.Type;
                    if (File.Exists(currentScreen.XmlPath))
                    {
                        currentScreen = xmlGameScreenManager.Load(currentScreen.XmlPath);
                    }
                    currentScreen.LoadContent();
                }
                else if(Image.Alpha == 0.0f)
                {
                    Image.IsActice = false;
                    IsTransitioning = false;
                }
            }
        }

        public ScreenManager()
        {
            Dimensions = new Vector2(500, 600);
            currentScreen = new SplashScreen();
            xmlGameScreenManager = new XMLManager<GameScreen>();
            xmlGameScreenManager.Type = currentScreen.Type;
            currentScreen = xmlGameScreenManager.Load("Content/Load/XMLSplashScreen.xml");
        }

        public void LoadContent(ContentManager content)
        {
            this.Content = new ContentManager(content.ServiceProvider, "Content");
            currentScreen.LoadContent();
            Image.LoadContent();
            
        }
        public void UnloadContent()
        {
            currentScreen.UnloadContent();
            Image.UnloadContent();
        }
        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
    
            Transition(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
            if(IsTransitioning)
            {
                Image.Draw(spriteBatch);
            }
        }
    }
}

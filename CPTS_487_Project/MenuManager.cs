using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CPTS_487_Project
{
    public class MenuManager
    {
        Menu menu;
        bool isTransitioning;

        void Transition(GameTime gameTime)
        {
            if (isTransitioning)
            {
                for (int i = 0; i < menu.Items.Count; i++)
                {
                    menu.Items[i].Image.Update(gameTime);
                    float first = menu.Items[0].Image.Alpha;
                    float last = menu.Items[menu.Items.Count - 1].Image.Alpha;
                    if (first == 0.0f && last == 0.0f)
                    {
                        menu.ID = menu.Items[menu.ItemNumer].LinkID;
                        break;
                    }
                    else if (first == 1.0f && last == 1.0f)
                    {
                        isTransitioning = false;
                        /**
                        foreach (MenuItem item in menu.Items)
                        {
                            item.Image.RestoreEffect();
                        }*/
                    }

                }
            }
        }
        public MenuManager()
        {
            menu = new Menu();
            menu.OnMenuChange += Menu_OnMenuChange; 
        }

        private void Menu_OnMenuChange(object sender, EventArgs e)
        {
            XMLManager<Menu> xmlMenuManager = new XMLManager<Menu>();
            menu.UnloadContent();
            // TODO Transition effect
            menu = xmlMenuManager.Load(menu.ID);
            menu.LoadContent();
            menu.OnMenuChange += Menu_OnMenuChange;
            menu.Transition(0.0f);
            /**
            foreach (MenuItem item in menu.Items)
            {
                item.Image.StoreEffect();
                item.Image.activateEffect("Fade");
            }*/
        }

        public void LoadContent(string menuPath)
        {
            if(menuPath != string.Empty)
            {
                menu.ID = menuPath;
            }
        }

        public void UnloadContent()
        {
            menu.UnloadContent();
        }
        public void Update(GameTime gameTime)
        {
            if (!isTransitioning)
            {
                menu.Update(gameTime);
            }
                if (InputManager.Instance.KeyPressed(Keys.Enter) && !isTransitioning)
                {
                    if (menu.Items[menu.ItemNumer].LinkType == "Screen")
                    {
                        ScreenManager.Instance.ChangeScreens(menu.Items[menu.ItemNumer].LinkID);
                    }
                    else
                    {
                        isTransitioning = true;
                        menu.Transition(1.0f);
                    /**
                        foreach(MenuItem item in menu.Items)
                        {
                            item.Image.StoreEffect();
                            item.Image.activateEffect("Fade");
                        }*/
                    }
            }
            Transition(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
    }
}

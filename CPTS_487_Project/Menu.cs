using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Xml.Serialization;

namespace CPTS_487_Project
{
    public class Menu
    {
        public event EventHandler OnMenuChange;

        public string Axis;
        public string Effects;

        [XmlElement("Item")]
        public List<MenuItem> Items;
        int itemNumber;
        string id;

        public int ItemNumer
        {
            get { return itemNumber; }
        }

        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                OnMenuChange(this, null);
            }
        }

        public void Transition(float alpha)
        {
            foreach(MenuItem item in Items)
            {
                item.Image.IsActice = true;
                item.Image.Alpha = alpha;
                if(alpha == 0.0f)
                {
                    item.Image.fadeEffect.Increase = true;
                }
                else
                {
                    item.Image.fadeEffect.Increase = false;
                }
            }
        }

        void AlignMenuItem()
        {
            Vector2 dimensions = Vector2.Zero;
            Vector2 drawLocation = Vector2.Zero;
            float startingX = 10;
            float startingY = 10;
            float offset = 150;

            foreach (MenuItem item in Items)
            {
                for (int i = 0; i < item.Image.text.Count; i++)
                {
                    dimensions = item.Image.getSize(i);
                    drawLocation = new Vector2((ScreenManager.Instance.Dimensions.X / 2 - dimensions.X / 2), startingY + offset);
                    item.Image.setPos(i,drawLocation);
                    offset = offset + 80;
                }
            }
        }

        public Menu()
        {
            id = string.Empty;
            itemNumber = 0;
            Effects = string.Empty;
            Axis = "Y";
            Items = new List<MenuItem>();
        }

        public void LoadContent()
        {
            string[] split = Effects.Split(':');
            foreach(MenuItem item in Items)
            {
                item.Image.LoadContent();
                foreach(string s in split)
                {
                    item.Image.activateEffect(s);
                }
                AlignMenuItem();
            }
        }
        public void UnloadContent()
        {
            foreach(MenuItem item in Items)
            {
                item.Image.UnloadContent();
            }
        }
        public void Update(GameTime gameTime)
        {
            if(Axis == "X")
            {
                if (InputManager.Instance.KeyPressed(Keys.Right))
                {
                    itemNumber++;
                }
                else if (InputManager.Instance.KeyPressed(Keys.Left))
                {
                    itemNumber--;
                }
            }
            else if(Axis == "Y")
            {
                if (InputManager.Instance.KeyPressed(Keys.Down))
                {
                    itemNumber++;
                }
                else if (InputManager.Instance.KeyPressed(Keys.Up))
                {
                    itemNumber--;
                }
                if(itemNumber < 0)
                {
                    itemNumber = 0;
                }
                else if(itemNumber > Items.Count - 1)
                {
                    itemNumber = Items.Count - 1;
                }

                for(int i = 0; i< Items.Count; i++)
                {
                    if(i == itemNumber)
                    {
                        Items[i].Image.IsActice = true;
                    }
                    else
                    {
                        Items[i].Image.IsActice = false;
                    }
                    Items[i].Image.Update(gameTime);
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(MenuItem item in Items)
            {
                item.Image.Draw(spriteBatch);
            }
        }
    }
}

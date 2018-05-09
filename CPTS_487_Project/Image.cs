using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CPTS_487_Project
{
    public class Image
    {
        public string Path;
        public Vector2 Position;
        public string Effects;
        public bool IsActice;
        public Vector2 Scale;
        public string Name;
        public string Type;

        [XmlElement("Text")]
        public List<string> text;
        [XmlElement("TextPos")]
        public List<Vector2> textPos;
        [XmlElement("TColor")]
        public List<string> tColor;
        [XmlElement("SpinningLoc")]
        public List<Vector2> sloc;

        [XmlIgnore]
        public float Alpha;
        [XmlIgnore]
        public string FontName;
        [XmlIgnore]
        public Texture2D Texture;
        [XmlIgnore]
        public Rectangle SourceRect;
        [XmlIgnore]
        public Effect.Fade fadeEffect;
        [XmlIgnore]
        public Effect.SpriteSheetEffect SpriteSheetEffect;
        [XmlIgnore]
        public Effect.SpriteSheetEffectEnemy SpriteSheetEffectEnemy;
        [XmlIgnore]
        public SpriteFont font;



        Vector2 origin;
        ContentManager content;
        RenderTarget2D renderTarget;
        Dictionary<string, ImageEffect> effectList;
        List<Tuple<string, Vector2, Color>> listText;
        int curLoc;


        public void StoreEffect()
        {
            Effects = string.Empty;
            foreach (var effect in effectList)
            {
                if (effect.Value.IsActive)
                {
                    Effects += effect.Key + ":";

                }
                if (Effects != string.Empty)
                {
                    Effects.Remove(Effects.Length - 1);
                }
            }
        }

        public void RestoreEffect()
        {
            foreach (var effect in effectList)
            {
                deactivateEffect(effect.Key);
            }
            string[] split = Effects.Split(':');
            foreach (string s in split)
            {
                activateEffect(s);
            }
        }

        public void setTextAndPos(String value, Vector2 pos, Color color)
        {
            this.listText.Add(Tuple.Create<string, Vector2, Color>(value, pos, color));
        }

        public Vector2 getSize(int i)
        {
            if (i < listText.Count)
            {
                return font.MeasureString(listText[i].Item1);
            }
            return Vector2.Zero;
        }

        public void setPos(int i, Vector2 value)
        {
            if (i < listText.Count)
            {
                this.listText[i] = Tuple.Create<string, Vector2, Color>(listText[i].Item1, value, listText[i].Item3);
            }
        }

        public void setEffect<T>(ref T effect)
        {
            if (effect == null)
            {
                effect = (T)Activator.CreateInstance(typeof(T));
            }
            else
            {
                (effect as ImageEffect).IsActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }
            string temp = effect.GetType().ToString().Replace("CPTS_487_Project.Effect.", "");
            if (!effectList.ContainsKey(temp))
            {
                effectList.Add(temp, (effect as ImageEffect));
            }

        }
        public void activateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = true;
                var obj = this;
                effectList[effect].LoadContent(ref obj);
            }
        }
        public void deactivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = false;
                effectList[effect].UnloadContent();
            }
        }

        public Image()
        {
            Path = Effects = String.Empty;
            FontName = "Font/logo";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            SourceRect = Rectangle.Empty;
            fadeEffect = new Effect.Fade();
            effectList = new Dictionary<string, ImageEffect>();
            text = new List<string>();
            textPos = new List<Vector2>();
            tColor = new List<string>();
            listText = new List<Tuple<string, Vector2, Color>>();
            sloc = new List<Vector2>();
            curLoc = 0;
            Name = string.Empty;
            Type = string.Empty;
        }

        public Image(Image copy)
        {
            Path = copy.Path;
            Effects = copy.Effects;
            FontName = copy.FontName;
            Position = copy.Position;
            Scale = copy.Scale;
            Alpha = copy.Alpha;
            SourceRect = copy.SourceRect;
            fadeEffect = copy.fadeEffect;
            effectList = copy.effectList;
            text = copy.text;
            textPos = copy.textPos;
            tColor = copy.tColor;
            listText = copy.listText;
            sloc = copy.sloc;
            curLoc = copy.curLoc;
            Name = copy.Name;
            Type = copy.Type;
        }

        public void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            if (Path != String.Empty)
            {
                Texture = content.Load<Texture2D>(Path);
            }
            if (Name == "Counter")
            {
                font = content.Load<SpriteFont>("Font/counter");
            }
            else
            {
                font = content.Load<SpriteFont>(FontName);
            }
            Vector2 dimensionsTexture = Vector2.Zero;
            Vector2 dimensionsText = Vector2.Zero;

            if (text.Count != 0)
            {
                for (int i = 0; i < text.Count; i++)
                {
                    this.listText.Add(Tuple.Create<string, Vector2, Color>(text[i], Vector2.Zero, Color.White));
                }
            }

            if (textPos.Count != 0)
            {
                for (int i = 0; i < textPos.Count; i++)
                {
                    listText[i] = Tuple.Create<string, Vector2, Color>(listText[i].Item1, textPos[i], Color.White);
                }
            }

            if (tColor.Count != 0)
            {
                for (int i = 0; i < tColor.Count; i++)
                {
                    Color curColor = new Color();
                    bool found = false;
                    PropertyInfo[] colorProperties = typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.Public);
                    foreach (PropertyInfo info in colorProperties)
                    {
                        if (info.Name == tColor[i])
                        {
                            curColor = (Color)info.GetValue(null, null);
                            found = true;
                            break;
                        }
                    }
                    if (found == true)
                    {
                        listText[i] = Tuple.Create<string, Vector2, Color>(listText[i].Item1, textPos[i], curColor);
                    }
                }
            }

            if (Texture != null)
            {
                dimensionsTexture.X += Texture.Width;
                dimensionsTexture.Y += Texture.Height;
                if (SourceRect == Rectangle.Empty)
                {
                    SourceRect = new Rectangle(0, 0, (int)dimensionsTexture.X, (int)dimensionsTexture.Y);
                }

                renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, (int)dimensionsTexture.X, (int)dimensionsTexture.Y);
                ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
                ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
                ScreenManager.Instance.SpriteBatch.Begin();
                ScreenManager.Instance.SpriteBatch.Draw(Texture, Vector2.Zero, Color.White);
                ScreenManager.Instance.SpriteBatch.End();
                Texture = renderTarget;
                ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);
            }

            // Effect
            setEffect<Effect.Fade>(ref fadeEffect);
            setEffect<Effect.SpriteSheetEffect>(ref SpriteSheetEffect);
            setEffect<Effect.SpriteSheetEffectEnemy>(ref SpriteSheetEffectEnemy);

            if (Effects != null && Effects != string.Empty)
            {
                string[] split = Effects.Split(':');
                foreach (string item in split)
                {
                    activateEffect(item);
                    IsActice = true;
                }
            }
        }


        public void UnloadContent()
        {
            if (content != null)
            {
                content.Unload();
            }
            foreach (var effect in effectList)
            {
                deactivateEffect(effect.Key);
            }
        }
        public void Update(GameTime gameTime)
        {
            foreach (var effect in effectList)
            {
                if (effect.Value.IsActive)
                {
                    effect.Value.Update(gameTime);
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(SourceRect.Width / 2, SourceRect.Height / 2);
            if (Texture != null)
            {
                spriteBatch.Draw(Texture, Position, SourceRect, Color.White * Alpha, 0.0f, origin, Scale, SpriteEffects.None, 0.0f);
            }

            if (listText.Count != 0)
            {
                for (int i = 0; i < listText.Count; i++)
                {
                    spriteBatch.DrawString(font, listText[i].Item1, listText[i].Item2, (Color)listText[i].Item3 * Alpha);
                }
                if (sloc.Count != 0)
                {
                    curLoc++;
                    if (curLoc == 8)
                    {
                        curLoc = 0;
                    }
                    Vector2 newLocation = new Vector2(sloc[curLoc].X, sloc[curLoc].Y);
                    listText[listText.Count - 1] = Tuple.Create<string, Vector2, Color>(listText[listText.Count - 1].Item1, newLocation, listText[listText.Count - 1].Item3);
                }

            }

        }
    }
}

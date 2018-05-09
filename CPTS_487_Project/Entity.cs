using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CPTS_487_Project
{
     public class Entity
    {
        [XmlElement("Image")]
        public List<Image> image;
        [XmlIgnore]
        public Image newImage;
        public Vector2 Velocity;
        public float MoveSpeed;

        public Entity()
        {
            newImage = new Image();
            image = new List<Image>();
            Velocity = Vector2.Zero;
            MoveSpeed = 0;
        }

        public virtual void LoadContent()
        {
            

        }
        public virtual void UnloadContent()
        {
         
        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}

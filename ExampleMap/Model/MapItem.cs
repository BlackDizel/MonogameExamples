using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleMap.Model
{
    class MapItem
    {
        private string textureName;
        private Rectangle visibleRectangle;
        private Texture2D texture;

        public Texture2D Texture { set { texture = value; } get { return texture; } }
        public string TextureName { get { return textureName; } set { textureName = value; } }
        public Rectangle VisibleRectangle { get { return visibleRectangle; } set { visibleRectangle = value; } }

    }
}

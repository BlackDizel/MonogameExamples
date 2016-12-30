using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleMap.Model
{
    class MapItemsCollection
    {
        private const string path_data_cache = "\\Content\\MapItems.json";

        private static MapItemsCollection instance;
        public static MapItemsCollection Instance { get { if (instance == null) instance = new MapItemsCollection(); return instance; } }

        private List<MapItem> items;

        private MapItemsCollection()
        {
            String json = readJson();
            items = JsonConvert.DeserializeObject<List<MapItem>>(json);
        }

        private string readJson()
        {
            String path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return File.ReadAllText(path + path_data_cache);
        }

        public void loadTextures(ContentManager content)
        {
            if (items == null) return;
            items.ForEach(o => o.Texture = content.Load<Texture2D>(o.TextureName));

        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            if (items == null) return;
            items.ForEach(o=> spriteBatch.Draw(texture:o.Texture,destinationRectangle:o.VisibleRectangle));
        }
    }
}

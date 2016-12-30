using Engine.Model;
using ExampleMap.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleMap.View
{
    class MapScene : Screen
    {
        public void LoadScreenContent(ContentManager Content)
        {
            Model.MapItemsCollection.Instance.loadTextures(Content);
        }
        public void Input(KeyboardState keyboardState
            , MouseState mouseState
            , GamePadState[] gamepadStates
            , TouchCollection touchCollection
            , GestureSample g
            , GameTime gameTime) {
        
            
        
        }
        public void UpdateScreen(GameTime gameTime) { }
        public void DrawScreen(SpriteBatch spriteBatch) {
            MapItemsCollection.Instance.Draw(spriteBatch);        
        }
    }
}

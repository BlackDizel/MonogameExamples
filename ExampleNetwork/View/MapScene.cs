using Engine.Model;
using ExampleNetwork.View;
using GameHologram.classes.xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleNetwork
{
    class MapScene : Screen
    {

        public void LoadScreenContent(ContentManager Content)
        {
            PlayerModel.Instance.Spritesheet = Content.Load<Texture2D>("professor_walk_cycle_no_hat");
        }

        public void Input(KeyboardState keyboardState
            , MouseState mouseState
            , GamePadState[] gamepadStates
            , TouchCollection touchCollection
            , GestureSample g
            , GameTime gameTime)
        { }

        public void UpdateScreen(GameTime gameTime)
        {
            NetworkManager.Instance.Update();
            PlayerModel.Instance.Update(gameTime);
        }
        public void DrawScreen(SpriteBatch spriteBatch)
        {
            PlayerModel.Instance.Draw(spriteBatch);
        }
    }
}

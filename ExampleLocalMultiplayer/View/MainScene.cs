using Engine.Model;
using ExampleLocalMultiplayer.Controller;
using ExampleLocalMultiplayer.Model;
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

namespace ExampleLocalMultiplayer.View
{
    class MainScene : Screen
    {
        public void LoadScreenContent(ContentManager Content)
        {
            ControllerTanks.Instance.LoadContent(Content);
        }

        public void Input(KeyboardState keyboardState
            , MouseState mouseState
            , GamePadState[] gamepadStates
            , TouchCollection touchCollection
            , GestureSample g
            , GameTime gameTime)
        {
            ControllerTanks.Instance.inputPlayerOne(gameTime, keyboardState);
            ControllerTanks.Instance.inputPlayerTwo(gameTime, keyboardState);

            for (int i = 0; i < gamepadStates.Length; ++i)
                ControllerTanks.Instance.inputPlayerGamepad(gameTime, gamepadStates[i], i);
        }

        public void UpdateScreen(GameTime gameTime)
        {
            ControllerTanks.Instance.Update(gameTime);
        }

        public void DrawScreen(SpriteBatch spriteBatch)
        {
            ControllerTanks.Instance.Draw(spriteBatch);
        }
    }
}

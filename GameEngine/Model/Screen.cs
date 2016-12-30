using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using System.IO;

namespace Engine.Model
{
    /// <summary>
    /// base game screen interface
    /// </summary>
    public interface Screen
    {
        void LoadScreenContent(ContentManager Content);
        void Input(KeyboardState keyboardState
            , MouseState mouseState
            , GamePadState[] gamepadStates
            , TouchCollection touchCollection
            , GestureSample g
            , GameTime gameTime);
        void UpdateScreen(GameTime gameTime);
        void DrawScreen(SpriteBatch spriteBatch);
    }

}

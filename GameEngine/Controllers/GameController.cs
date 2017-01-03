using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Engine.Model
{
    public class GameController
    {
        private static GameController instance;
        public Random Random { get; private set; }

        public static GameController Instance
        {
            get
            {
                if (instance == null) instance = new GameController();
                return instance;
            }
        }

        private GameController()
        {
            Random = new Random();
            isExit = false;
        }

        public MouseState SavedMouseState;
        public KeyboardState SavedKeyboardState;
        public GamePadState[] SavedGamepadStates;

        public int ScreenWidth;
        public int ScreenHeight;

        public List<Screen> Screens;

        /// <summary>
        /// current displayed screen
        /// </summary>
        public Screen CurrentScreen;
        public GraphicsDevice GraphicDevice { get; set; }
        public ContentManager ContentManager { set; private get; }

        /// <summary>
        /// game start screen
        /// </summary>
        public Screen StartScreen { set; internal get; }

        public bool isGamepadButtonPressed(GamePadState[] gamepadStates, Buttons btn)
        {
            return gamepadStates != null
                && gamepadStates.Length > 0
                && SavedGamepadStates != null
                && SavedGamepadStates.Length > 0
                && SavedGamepadStates[0].IsButtonUp(btn)
                && (gamepadStates[0].IsButtonDown(btn));
        }

        public bool isKeyboardButtonPressed(KeyboardState keyboardState, Keys btn)
        {
            return keyboardState.IsKeyUp(btn)
                && SavedKeyboardState.IsKeyDown(btn);
        }

        internal void Init()
        {
            CurrentScreen = StartScreen;
            StartScreen.LoadScreenContent(ContentManager);
        }

        public void NavigateScreen<T>() where T : Screen
        {
            Screen s = Activator.CreateInstance<T>();
            ContentManager.Unload();
            s.LoadScreenContent(ContentManager);
            CurrentScreen = s;
        }

        public Microsoft.Xna.Framework.Matrix View { get; set; }

        public bool IsExit { get { return isExit; } }

        private bool isExit;
        public void exit()
        {
            isExit = true;
        }
    }
}

using Engine.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Engine
{
    public class GameMain : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState keyboardState;
        GestureSample gestureSample;
        TouchCollection touchCollection;
        MouseState mouseState;
        GamePadState[] gamepadStates;
        public static Matrix _view;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GameController.Instance.ContentManager = Content;
            GameController.Instance.View = _view;

            //IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            //GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height; //return display size, not window size
            GameController.Instance.ScreenHeight = graphics.GraphicsDevice.Viewport.Height;
            GameController.Instance.ScreenWidth = graphics.GraphicsDevice.Viewport.Width;
            GameController.Instance.Init();
        }

        protected override void LoadContent()
        {
            _view = Matrix.Identity;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameController.Instance.GraphicDevice = GraphicsDevice;

            //            //поддерживаемые в приложении жесты
            //            TouchPanel.EnabledGestures = GestureType.Tap | GestureType.FreeDrag | GestureType.DragComplete;
            //            //windows
            //#if !WINDOWS_PHONE
            //            IsMouseVisible = true;
            //#endif
            //            //MY LOAD END Block
        }

        protected override void UnloadContent()
        { }

        protected override void Update(GameTime gameTime)
        {
            GameController.Instance.ScreenWidth = graphics.GraphicsDevice.Viewport.Width;
            GameController.Instance.ScreenHeight = graphics.GraphicsDevice.Viewport.Height;

            if (GameController.Instance.CurrentScreen != null)
                GameController.Instance.CurrentScreen.UpdateScreen(gameTime);
            userInput(gameTime);

            if (GameController.Instance.IsExit) Exit();
            base.Update(gameTime);
        }

        private void userInput(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            touchCollection = TouchPanel.GetState();
            gestureSample = TouchPanel.IsGestureAvailable ? TouchPanel.ReadGesture() : new GestureSample();

            gamepadStates = null;
            if (GamePad.MaximumGamePadCount > 0)
            {
                gamepadStates = new GamePadState[GamePad.MaximumGamePadCount];
                for (int i = 0; i < GamePad.MaximumGamePadCount; ++i)
                    gamepadStates[i] = GamePad.GetState(i);
            }

            if (GameController.Instance.CurrentScreen != null)
                GameController.Instance.CurrentScreen.Input(keyboardState
                    , mouseState
                    , gamepadStates
                    , touchCollection
                    , gestureSample
                    , gameTime);

            GameController.Instance.SavedMouseState = mouseState;
            GameController.Instance.SavedKeyboardState = keyboardState;
            GameController.Instance.SavedGamepadStates = gamepadStates;
            GameController.Instance.SavedGamepadStates = gamepadStates;

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            if (GameController.Instance.CurrentScreen != null)
                GameController.Instance.CurrentScreen.DrawScreen(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

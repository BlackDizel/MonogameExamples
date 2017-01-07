using Engine.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using ExampleMinimap.Controller;

namespace ExampleMinimap.View
{
    class SceneMinimap : Screen
    {
        private Texture2D[] tObjects;
        private Texture2D[] tIcons;
        private CameraMinimap camera;

        Viewport viewportOrigin;

        private const int objects_num = 4;
        private const string texture_path_player = "objects\\player";
        private const string texture_path_object1 = "objects\\object1";
        private const string texture_path_object2 = "objects\\object2";
        private const string texture_path_object3 = "objects\\object3";

        private const string texture_icon_path_player = "icons\\icon_player";
        private const string texture_icon_path_object1 = "icons\\icon_object1";
        private const string texture_icon_path_object2 = "icons\\icon_object2";
        private const string texture_icon_path_object3 = "icons\\icon_object3";
        private static readonly Color colorMinimapBG = new Color(1, 1, 1, 0.5f);

        public void DrawScreen(SpriteBatch spriteBatch)
        {
            drawPlayer(spriteBatch);
            drawObjects(spriteBatch);
            drawMinimap(spriteBatch);
        }

        private void drawPlayer(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tObjects[0], ControllerPlayer.Instance.Position, Color.White);
        }

        private void drawObjects(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tObjects[1], ControllerObjects.Instance.position(0) - ControllerObjects.Instance.ObjectSizes[0], Color.White);
            spriteBatch.Draw(tObjects[2], ControllerObjects.Instance.position(1) - ControllerObjects.Instance.ObjectSizes[1], Color.White);
            spriteBatch.Draw(tObjects[3], ControllerObjects.Instance.position(2) - ControllerObjects.Instance.ObjectSizes[2], Color.White);
        }

        private void drawMinimap(SpriteBatch spriteBatch)
        {
            spriteBatch.End();

            viewportOrigin = GameController.Instance.GraphicDevice.Viewport;
            GameController.Instance.GraphicDevice.Viewport = camera.ViewportCamera;

            spriteBatch.Begin(blendState: BlendState.Additive);
            spriteBatch.Draw(tIcons[0], new Rectangle(0, 0, camera.ViewportCamera.Width, camera.ViewportCamera.Height), colorMinimapBG);
            spriteBatch.End();

            spriteBatch.Begin(transformMatrix: camera.ViewMatrix);

            spriteBatch.Draw(tIcons[0], camera.Position(ControllerPlayer.Instance.Position) - ControllerPlayer.Instance.PlayerIconSize / 2f, Color.White);
            spriteBatch.Draw(tIcons[1], camera.Position(ControllerObjects.Instance.position(0)) - ControllerObjects.Instance.ObjectIconsSizes[0] / 2f, Color.White);
            spriteBatch.Draw(tIcons[2], camera.Position(ControllerObjects.Instance.position(1)) - ControllerObjects.Instance.ObjectIconsSizes[1] / 2f, Color.White);
            spriteBatch.Draw(tIcons[3], camera.Position(ControllerObjects.Instance.position(2)) - ControllerObjects.Instance.ObjectIconsSizes[2] / 2f, Color.White);

            spriteBatch.End();
            GameController.Instance.GraphicDevice.Viewport = viewportOrigin;
        }

        public void Input(KeyboardState keyboardState, MouseState mouseState, GamePadState[] gamepadStates, TouchCollection touchCollection, GestureSample g, GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
                ControllerPlayer.Instance.moveLeft(gameTime.ElapsedGameTime.TotalMilliseconds);

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
                ControllerPlayer.Instance.moveUp(gameTime.ElapsedGameTime.TotalMilliseconds);

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                ControllerPlayer.Instance.moveRight(gameTime.ElapsedGameTime.TotalMilliseconds);

            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                ControllerPlayer.Instance.moveDown(gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void LoadScreenContent(ContentManager Content)
        {
            tObjects = new Texture2D[objects_num];
            tObjects[0] = Content.Load<Texture2D>(texture_path_player);
            tObjects[1] = Content.Load<Texture2D>(texture_path_object1);
            tObjects[2] = Content.Load<Texture2D>(texture_path_object2);
            tObjects[3] = Content.Load<Texture2D>(texture_path_object3);

            tIcons = new Texture2D[objects_num];
            tIcons[0] = Content.Load<Texture2D>(texture_icon_path_player);
            tIcons[1] = Content.Load<Texture2D>(texture_icon_path_object1);
            tIcons[2] = Content.Load<Texture2D>(texture_icon_path_object2);
            tIcons[3] = Content.Load<Texture2D>(texture_icon_path_object3);

            ControllerPlayer.Instance.PlayerSize = tObjects[0].Bounds.Size.ToVector2();
            ControllerPlayer.Instance.PlayerIconSize = tIcons[0].Bounds.Size.ToVector2();

            ControllerObjects.Instance.ObjectSizes = new Vector2[]
            {
                tObjects[1].Bounds.Size.ToVector2()/2f,
                tObjects[2].Bounds.Size.ToVector2()/2f,
                tObjects[3].Bounds.Size.ToVector2()/2f
            };

            ControllerObjects.Instance.ObjectIconsSizes = new Vector2[]
            {
                tIcons[1].Bounds.Size.ToVector2()/2f,
                tIcons[2].Bounds.Size.ToVector2()/2f,
                tIcons[3].Bounds.Size.ToVector2()/2f
            };

            camera = new CameraMinimap();
        }

        public void UpdateScreen(GameTime gameTime)
        {
        }
    }
}

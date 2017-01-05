using Engine.Model;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using ExampleTextMenu.Model;
using ExampleTextMenu.Controller;

namespace ExampleTextMenu.View
{
    sealed class MenuScene : Screen
    {
        private SpriteFont menuFont;
        private ControllerMenu<MenuItemObject> controllerMenu;
        private static readonly string font_res = "MenuFont";
        private static readonly int space_vertical = 20;

        public void DrawScreen(SpriteBatch spriteBatch)
        {
            drawTitle(spriteBatch);
            drawMenuItems(spriteBatch);
        }

        private void drawTitle(SpriteBatch spriteBatch)
        {
            if (String.IsNullOrEmpty(controllerMenu.CurrentMenuTitle)) return;
            spriteBatch.DrawString(menuFont, controllerMenu.CurrentMenuTitle, Vector2.Zero, Color.White);
        }

        private void drawMenuItems(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < controllerMenu.CurrentMenuItemsCount; ++i)
            {
                if (String.IsNullOrEmpty(controllerMenu.CurrentMenuItemTitle(i)))
                    continue;
                spriteBatch.DrawString(menuFont,
                    (i < 9 ? (i + 1).ToString() + ". " : "") + controllerMenu.CurrentMenuItemTitle(i),
                    Vector2.UnitY * (i + 1) * space_vertical, i == controllerMenu.SelectedMenuItemIndex ? Color.Yellow : Color.White);
            }
        }

        public void Input(KeyboardState keyboardState, MouseState mouseState, GamePadState[] gamepadStates, TouchCollection touchCollection, GestureSample g, GameTime gameTime)
        {
            if (GameController.Instance.isKeyboardButtonPressed(keyboardState, Keys.Escape))
            {
                controllerMenu.navigateUp();
                checkMenuItemSelected();
                return;
            }

            if (GameController.Instance.isKeyboardButtonPressed(keyboardState, Keys.Enter)
                || GameController.Instance.isKeyboardButtonPressed(keyboardState, Keys.Space))
            {
                controllerMenu.navigateInto();
                checkMenuItemSelected();
                return;
            }

            if (GameController.Instance.isKeyboardButtonPressed(keyboardState, Keys.Down))
                controllerMenu.setNextSelectedMenuItemIndex();

            if (GameController.Instance.isKeyboardButtonPressed(keyboardState, Keys.Up))
                controllerMenu.setPreviousSelectedMenuItemIndex();

            inputNumbers(keyboardState);
        }

        private void checkMenuItemSelected()
        {
            if (!controllerMenu.IsCurrentMenuExist)
                GameController.Instance.exit();

            if (controllerMenu.CurrentMenuTitle == ControllerMenu<MenuItemObject>.id_exit)
                GameController.Instance.exit();

            //todo add other items selected
        }

        private void inputNumbers(KeyboardState keyboardState)
        {
            for (int i = 0; i < Math.Min(controllerMenu.CurrentMenuItemsCount, 9); ++i)
                if (GameController.Instance.isKeyboardButtonPressed(keyboardState, Keys.D1 + i))
                    controllerMenu.navigateInto(i);
        }

        public void LoadScreenContent(ContentManager Content)
        {
            controllerMenu = ControllerMenu<MenuItemObject>.Instance;
            controllerMenu.init();
            menuFont = Content.Load<SpriteFont>(font_res);
        }

        public void UpdateScreen(GameTime gameTime)
        {
        }
    }
}
using Engine.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using ExampleDialog.Controller;
using System;

namespace ExampleDialog.View
{
    class SceneDialog : Screen
    {

        private ControllerDialog controllerDialog;
        private SpriteFont dialogFont;
        private static readonly string dialog_file = "\\Content\\dialog.json";
        private static readonly int vertical_space = 20;
        private static readonly int answer_vertical_space = 20;
        private static readonly string font_path = "DialogFont";
        private static readonly string npc_bob = "Bob: ";
        private static readonly string npc_mary = "Mary: ";

        #region draw
        public void DrawScreen(SpriteBatch spriteBatch)
        {
            drawNPCPhrases(spriteBatch);
            drawAnswers(spriteBatch);
        }

        private void drawAnswers(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < controllerDialog.AnswersNum; ++i)
            {
                if (String.IsNullOrEmpty(controllerDialog.answerMessage(i))) continue;
                spriteBatch.DrawString(dialogFont
                    , answerNum(i) + controllerDialog.answerMessage(i)
                    , Vector2.UnitY * (i * vertical_space + controllerDialog.NPCPhrasesNum * vertical_space + answer_vertical_space)
                    , i == controllerDialog.SelectedAnswerIndex ? Color.White : Color.LightGray);
            }
        }

        private string answerNum(int i)
        {
            return i < 9 ? (i + 1) + ". " : "";
        }

        private void drawNPCPhrases(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < controllerDialog.NPCPhrasesNum; ++i)
            {
                if (String.IsNullOrEmpty(controllerDialog.npcPhrase(i))) continue;
                spriteBatch.DrawString(dialogFont
                    , npcName(i) + controllerDialog.npcPhrase(i)
                    , Vector2.UnitY * i * vertical_space
                    , npcColor(i));
            }
        }

        private Color npcColor(int i)
        {
            return controllerDialog.npcID(i).HasValue && controllerDialog.npcID(i).Value == 1
                    ? Color.Yellow
                    : controllerDialog.npcID(i).HasValue && controllerDialog.npcID(i).Value == 2
                    ? Color.LightGreen
                    : Color.LightGray;
        }

        private string npcName(int i)
        {
            return controllerDialog.npcID(i).HasValue && controllerDialog.npcID(i).Value == 1
                ? npc_bob
                : controllerDialog.npcID(i).HasValue && controllerDialog.npcID(i).Value == 2
                ? npc_mary
                : "";
        }
        #endregion

        #region input
        public void Input(KeyboardState keyboardState, MouseState mouseState, GamePadState[] gamepadStates, TouchCollection touchCollection, GestureSample g, GameTime gameTime)
        {
            if (GameController.Instance.isKeyboardButtonPressed(keyboardState, Keys.Enter)
                || GameController.Instance.isKeyboardButtonPressed(keyboardState, Keys.Space))
            {
                controllerDialog.answer();
                return;
            }

            if (GameController.Instance.isKeyboardButtonPressed(keyboardState, Keys.Down))
                controllerDialog.nextSelectedAnswerIndex();

            if (GameController.Instance.isKeyboardButtonPressed(keyboardState, Keys.Up))
                controllerDialog.previousSelectedAnswerIndex();

            inputNumbers(keyboardState);
        }

        private void inputNumbers(KeyboardState keyboardState)
        {
            for (int i = 0; i < Math.Min(controllerDialog.AnswersNum, 9); ++i)
                if (GameController.Instance.isKeyboardButtonPressed(keyboardState, Keys.D1 + i))
                    controllerDialog.answer(i);
        }
        #endregion

        public void LoadScreenContent(ContentManager Content)
        {
            controllerDialog = new ControllerDialog();
            controllerDialog.init(dialog_file);
            dialogFont = Content.Load<SpriteFont>(font_path);
        }

        public void UpdateScreen(GameTime gameTime)
        {
        }
    }
}

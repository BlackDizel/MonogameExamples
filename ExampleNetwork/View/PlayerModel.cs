using Engine.Model;
using ExampleNetwork.Controller;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Controller;

namespace ExampleNetwork.View
{
    class PlayerModel
    {
        private Vector2 frameSize = new Vector2(64, 64);
        private const byte framesNum = 8;
        private const byte delayMillis = 100;

        public static PlayerModel Instance { get { if (instance == null) instance = new PlayerModel(); return instance; } }
        private static PlayerModel instance;

        private Texture2D tSpritesheet;
        private Vector2 position;
        private Rectangle standFrame;

        private Animation animation;
        private Model.Direction savedDirection;

        private PlayerModel()
        {
            position = new Vector2((GameController.Instance.ScreenWidth - frameSize.X) / 2, (GameController.Instance.ScreenHeight - frameSize.Y) / 2);
        }

        public void Update(GameTime gameTime)
        {
            updateAnimation(gameTime);
            updateStandFrame();
        }

        private void updateAnimation(GameTime gameTime)
        {
            if (isDirectionChanged())
                initAnimation();
            animation.Update(gameTime);
        }

        private void initAnimation()
        {
            animation = Animation.AnimationSinglelineSourceRectangle(tSpritesheet
                    , framesNum
                    , delayMillis
                    , new Rectangle((int)frameSize.X
                        , multiplierFromDirection() * (int)frameSize.Y
                        , (int)frameSize.X * framesNum
                        , (int)frameSize.Y));

            savedDirection = ControllerPlayer.Instance.Direction;
        }

        private int multiplierFromDirection()
        {
            int multiplier = 0;
            switch (ControllerPlayer.Instance.Direction)
            {
                case Model.Direction.LEFT:
                    multiplier = 1;
                    break;
                case Model.Direction.UP:
                    multiplier = 0;
                    break;
                case Model.Direction.RIGHT:
                    multiplier = 3;
                    break;
                case Model.Direction.DOWN:
                    multiplier = 2;
                    break;
            }
            return multiplier;
        }

        private bool isDirectionChanged()
        {
            return savedDirection != ControllerPlayer.Instance.Direction;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (ControllerPlayer.Instance.IsStand())
                drawStand(spriteBatch);
            else drawMove(spriteBatch);
        }

        private void drawMove(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch, position);
        }

        private void drawStand(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tSpritesheet, position, sourceRectangle: standFrame);
        }

        private void updateStandFrame()
        {
            if (standFrame.IsEmpty)
            {
                standFrame = new Rectangle();
                standFrame.X = 0;
                standFrame.Width = (int)frameSize.X;
                standFrame.Height = (int)frameSize.Y;
            }
            standFrame.Y = multiplierFromDirection() * (int)frameSize.Y;
        }

        public Texture2D Spritesheet
        {
            set
            {
                tSpritesheet = value;
            }
        }

    }
}

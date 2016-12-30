using Engine.Model;
using ExampleLocalMultiplayer.Controller;
using GameEngine.Controllers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleLocalMultiplayer.Model
{
    class Player : MoveableObject
    {
        private const float speedInSeconds = 150f;

        private const int animationFrames = 3;
        private const int animationDelayMillis = 100;
        public const int frameSize = 64;
        private static Rectangle[] rectangleAnimationSource;

        private float currentSpeed;
        private Rectangle position;
        private Color colorMask;
        private Direction direction;
        private Direction animationDirection;
        private AnimationBase animation;

        public Rectangle Position { get { return position; } }
        public Color ColorMask { get { return colorMask; } }
        public Direction Direction { get { return direction; } }
        public AnimationBase Animation { get { return animation; } set { animation = value; } }

        public Player(Rectangle position, Color colorMask, Direction direction)
            : base()
        {
            if (Player.rectangleAnimationSource == null)
                Player.rectangleAnimationSource = new Rectangle[]
                {
                    new Rectangle(0, frameSize * 1, frameSize * animationFrames, frameSize)
                    ,new Rectangle(0, frameSize * 3, frameSize * animationFrames, frameSize)
                    ,new Rectangle(0, frameSize * 2, frameSize * animationFrames, frameSize)
                    ,new Rectangle(0, frameSize * 0, frameSize * animationFrames, frameSize)
                };

            this.colorMask = colorMask;
            this.position = position;
            this.direction = direction;
            animationDirection = Direction.UNKNOWN;
            setAnimation(direction);
            currentSpeed = 0;
        }

        public void setAnimation(Direction direction)
        {
            if (animationDirection == direction) return;

            Rectangle sourceRectangle = Rectangle.Empty;
            if (direction == Direction.LEFT)
                sourceRectangle = rectangleAnimationSource[0];
            else if (direction == Direction.UP)
                sourceRectangle = rectangleAnimationSource[1];
            else if (direction == Direction.RIGHT)
                sourceRectangle = rectangleAnimationSource[2];
            else if (direction == Direction.DOWN)
                sourceRectangle = rectangleAnimationSource[3];

            if (sourceRectangle == Rectangle.Empty) return;
            animation = AnimationBase.AnimationSinglelineSourceRectangle(animationFrames, animationDelayMillis, sourceRectangle);
            animationDirection = direction;
        }

        public void move(Direction direction)
        {
            this.direction = direction;
            currentSpeed = speedInSeconds;
        }

        public void move(Direction direction, float speed)
        {
            this.direction = direction;
            currentSpeed = speedInSeconds * speed;
        }

        public void stop()
        {
            currentSpeed = 0;
        }

        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);

            Vector2 delta = calcDelta(gameTime, direction, currentSpeed);
            if (ControllerTanks.Instance.CheckCollision(this, delta))
                return;
            position.Offset(delta);
        }

    }
}

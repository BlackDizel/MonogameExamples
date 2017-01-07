using Engine.Model;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleMinimap.Controller
{
    class ControllerPlayer
    {
        private const float speed = 0.2f;

        private static ControllerPlayer instance;
        public static ControllerPlayer Instance
        {
            get
            {
                if (instance == null) instance = new ControllerPlayer();
                return instance;
            }
        }

        private Vector2 position;
        private Vector2 playerSize;
        internal Vector2 PlayerIconSize;

        public Vector2 Position { get { return position; } }

        public Vector2 PlayerSize
        {
            set
            {
                playerSize = value;
            }
        }

        private ControllerPlayer()
        {
            position = new Vector2(GameController.Instance.ScreenWidth / 2f, GameController.Instance.ScreenHeight / 2f);
        }

        internal void moveLeft(double delta)
        {
            move(-Vector2.UnitX, delta);
        }

        internal void moveUp(double delta)
        {
            move(-Vector2.UnitY, delta);
        }

        internal void moveRight(double delta)
        {
            move(Vector2.UnitX, delta);
        }

        internal void moveDown(double delta)
        {
            move(Vector2.UnitY, delta);
        }

        private void move(Vector2 direction, double delta)
        {
            Vector2 newPosition = position + direction * speed * (long)delta;
            if (newPosition.X > GameController.Instance.ScreenWidth - (playerSize == null ? 0 : playerSize.X)
                || newPosition.X < 0
                || newPosition.Y > GameController.Instance.ScreenHeight - (playerSize == null ? 0 : playerSize.Y)
                || newPosition.Y < 0)
                return;
            position = newPosition;
        }
    }
}

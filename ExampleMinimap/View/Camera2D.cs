using System;
using Engine.Model;
using ExampleMinimap.Controller;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ExampleMinimap.View
{
    //based on Camera2D class from Monogame.Extended. 
    //See http://www.dylanwilson.net/implementing-a-2d-camera-in-monogame
    class CameraMinimap
    {
        private const float Zoom = 4f;
        private const int margin_top = 12;
        private const int margin_left = 24;
        private readonly Vector3 origin;
        public readonly Viewport ViewportCamera;
        private Vector3 position;

        public CameraMinimap()
        {
            ViewportCamera.Width = 200;
            ViewportCamera.Height = 200;
            ViewportCamera.X = GameController.Instance.GraphicDevice.Viewport.X + margin_left;
            ViewportCamera.Y = GameController.Instance.GraphicDevice.Viewport.Y + margin_top;

            origin = new Vector3(ViewportCamera.Width / 2f, ViewportCamera.Height / 2f, 0f);
            position = Vector3.Zero;
        }

        public Matrix ViewMatrix
        {
            get
            {
                position.X = -ControllerPlayer.Instance.Position.X;
                position.Y = -ControllerPlayer.Instance.Position.Y;

                return Matrix.CreateTranslation(Position(position))
                    * Matrix.CreateTranslation(origin);
            }
        }

        //use division to zoom instead of camera matrix zoom to save original texture size
        internal Vector2 Position(Vector2 position)
        {
            return position / Zoom;
        }

        //use division to zoom instead of camera matrix zoom to save original texture size
        internal Vector3 Position(Vector3 position)
        {
            return position / Zoom;
        }
    }
}

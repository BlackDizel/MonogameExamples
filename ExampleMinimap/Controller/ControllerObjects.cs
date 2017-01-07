using Engine.Model;
using Microsoft.Xna.Framework;

namespace ExampleMinimap.Controller
{
    class ControllerObjects
    {
        private static ControllerObjects instance;
        private Vector2[] positions;

        public Vector2[] ObjectSizes;
        internal Vector2[] ObjectIconsSizes;

        public static ControllerObjects Instance
        {
            get
            {
                if (instance == null) instance = new ControllerObjects();
                return instance;
            }
        }

        public Vector2 position(int index)
        {
            if (index < 0 || positions == null || positions.Length <= index)
                return Vector2.Zero;
            return positions[index];
        }

        private ControllerObjects()
        {
            positions = new Vector2[3];
            positions[0] = new Vector2(GameController.Instance.ScreenWidth / 6f, GameController.Instance.ScreenHeight / 2f);
            positions[1] = new Vector2(GameController.Instance.ScreenWidth / 6f * 5, GameController.Instance.ScreenHeight / 2f);
            positions[2] = new Vector2(GameController.Instance.ScreenWidth / 2f, GameController.Instance.ScreenHeight / 6f);
        }
    }
}

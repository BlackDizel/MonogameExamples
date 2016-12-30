using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleLocalMultiplayer.Model
{
    class MoveableObject
    {
        private double lastUpdateTime;
        public MoveableObject()
        {
            lastUpdateTime = -1d;
        }

        protected Vector2 calcDelta(GameTime gameTime, Direction direction, float speedInSecond)
        {
            Vector2 delta = Vector2.Zero;
            if (lastUpdateTime > 0)
            {
                double deltaMillis = gameTime.TotalGameTime.TotalMilliseconds - lastUpdateTime;
                float distance = speedInSecond * (float)deltaMillis / 1000f;
                if (direction == Direction.LEFT) delta = -Vector2.UnitX * distance;
                else if (direction == Direction.UP) delta = -Vector2.UnitY * distance;
                else if (direction == Direction.RIGHT) delta = Vector2.UnitX * distance;
                else if (direction == Direction.DOWN) delta = Vector2.UnitY * distance;
            }
            lastUpdateTime = gameTime.TotalGameTime.TotalMilliseconds;
            return delta;
        }
    }
}

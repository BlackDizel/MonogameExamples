using Engine.Controller;
using Engine.Model;
using ExampleLocalMultiplayer.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleLocalMultiplayer.Controller
{
    class ControllerTanks
    {
        private const float gamepadDeathZone = 0.1f;

        private Texture2D tTank;
        private const string tankFile = "tank1";
        private List<Player> players;
        private Rectangle rectangleCheckCollision;

        private static ControllerTanks instance;
        public static ControllerTanks Instance
        {
            get
            {
                if (instance == null) instance = new ControllerTanks();
                return instance;
            }
        }
        private ControllerTanks()
        {
            players = new List<Player>();

            //keyboard players
            players.Add(new Player(new Rectangle(5, 5
                , Player.frameSize, Player.frameSize)
                , Color.Yellow, Direction.RIGHT));

            players.Add(new Player(new Rectangle(5, 5 + Player.frameSize
                , Player.frameSize, Player.frameSize)
                , Color.LightGreen, Direction.RIGHT));

            //gamepad players
            players.Add(new Player(new Rectangle(GameController.Instance.ScreenWidth - Player.frameSize - 5, 5
                , Player.frameSize, Player.frameSize)
                , Color.White, Direction.LEFT));

            players.Add(new Player(
                new Rectangle(GameController.Instance.ScreenWidth - Player.frameSize - 5
                , 5 + Player.frameSize
                , Player.frameSize, Player.frameSize)
                , Color.OrangeRed, Direction.LEFT));

            players.Add(new Player(
                new Rectangle(GameController.Instance.ScreenWidth - Player.frameSize - 5
                , (5 + Player.frameSize) * 2
                , Player.frameSize, Player.frameSize)
                , Color.LightBlue, Direction.LEFT));

            players.Add(new Player(
                new Rectangle(GameController.Instance.ScreenWidth - Player.frameSize - 5
                , (5 + Player.frameSize) * 3
                , Player.frameSize, Player.frameSize)
                , Color.Pink, Direction.LEFT));
        }

        public void LoadContent(ContentManager content)
        {
            tTank = content.Load<Texture2D>(tankFile);
        }

        public void Update(GameTime gameTime)
        {
            if (players == null) return;
            players.ForEach(o => o.Update(gameTime));
        }

        public void inputPlayerOne(GameTime gameTime, KeyboardState keyboardState)
        {
            Player player = players[0];
            Direction direction = Direction.UNKNOWN;
            if (keyboardState.IsKeyDown(Keys.Left)) direction = Direction.LEFT;
            else if (keyboardState.IsKeyDown(Keys.Up)) direction = Direction.UP;
            else if (keyboardState.IsKeyDown(Keys.Right)) direction = Direction.RIGHT;
            else if (keyboardState.IsKeyDown(Keys.Down)) direction = Direction.DOWN;

            if (direction == Direction.UNKNOWN)
                player.stop();
            else
            {
                player.move(direction);
                player.setAnimation(direction);
            }
        }

        public void inputPlayerTwo(GameTime gameTime, KeyboardState keyboardState)
        {
            Player player = players[1];
            Direction direction = Direction.UNKNOWN;
            if (keyboardState.IsKeyDown(Keys.A)) direction = Direction.LEFT;
            else if (keyboardState.IsKeyDown(Keys.W)) direction = Direction.UP;
            else if (keyboardState.IsKeyDown(Keys.D)) direction = Direction.RIGHT;
            else if (keyboardState.IsKeyDown(Keys.S)) direction = Direction.DOWN;

            if (direction == Direction.UNKNOWN)
                player.stop();
            else
            {
                player.move(direction);
                player.setAnimation(direction);
            }
        }

        public void inputPlayerGamepad(GameTime gameTime, GamePadState state, int index)
        {
            int itemIndex = 2 + index;
            if (itemIndex >= players.Count)
                return;

            Player player = players[itemIndex];
            Direction direction = Direction.UNKNOWN;

            double angle = Math.Atan2(state.ThumbSticks.Left.Y, state.ThumbSticks.Left.X);

            if (state.ThumbSticks.Left.Length() < gamepadDeathZone) direction = Direction.UNKNOWN;
            else if (angle > Math.PI * 3 / 4f || angle < -Math.PI * 3 / 4f) direction = Direction.LEFT;
            else if (angle > Math.PI / 4f && angle < Math.PI * 3 / 4f) direction = Direction.UP;
            else if (angle < Math.PI / 4f && angle > -Math.PI * 1 / 4f) direction = Direction.RIGHT;
            else if (angle > -Math.PI * 3 / 4f && angle < -Math.PI * 1 / 4f) direction = Direction.DOWN;
            if (direction == Direction.UNKNOWN)
                player.stop();
            else
            {
                player.move(direction, state.ThumbSticks.Left.Length());
                player.setAnimation(direction);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (players == null) return;
            players.ForEach(o => o.Animation.Draw(spriteBatch, tTank, o.Position, color: o.ColorMask));
        }

        public bool CheckCollision(Player player, Vector2 delta)
        {
            if (player == null || delta == Vector2.Zero) return true;
            if (rectangleCheckCollision == null) rectangleCheckCollision = new Rectangle();

            rectangleCheckCollision.X = player.Position.X;
            rectangleCheckCollision.Y = player.Position.Y;
            rectangleCheckCollision.Width = player.Position.Width;
            rectangleCheckCollision.Height = player.Position.Height;
            rectangleCheckCollision.Offset(delta);

            return players.Where(o => o != player)
                .Any(o => o.Position.Intersects(rectangleCheckCollision))
                || rectangleCheckCollision.Right > GameController.Instance.ScreenWidth
                || rectangleCheckCollision.Bottom > GameController.Instance.ScreenHeight
                || rectangleCheckCollision.X < 0
                || rectangleCheckCollision.Y < 0
                ;
        }
    }
}

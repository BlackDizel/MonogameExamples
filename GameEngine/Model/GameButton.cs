using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Engine.Model
{
    public class GameButton : VisibleObject
    {
        public GameButton(Texture2D text, Rectangle rect) : base(text, rect) { }

        public bool Click(MouseState ms)
        {
            if ((ms.LeftButton == ButtonState.Pressed) && RecPos.Contains(ms.X, ms.Y))
                return true;
            return false;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, RecPos, Color.White);
        }

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Model
{
    class Popup
    {
        String message;
        SpriteFont sf;
        Vector2 Position;
        double seconds = 500;

        public Popup(SpriteFont sf) 
        { 
            seconds=0;
            this.sf = sf;
        }

        public Popup(double curTime, SpriteFont sf, string s, Vector2 Position)
        { 
            this.message=s;
            this.sf=sf;
            this.Position=Position;
            seconds+=curTime;
        }

        public Popup(Texture2D t, Vector2 Position)
        {

        }

        public void ChangePos(double curTime, string s, Rectangle Position)
        {
            seconds = curTime + 500;
            this.Position = new Vector2(Position.X,Position.Y);
            message = s;
        }
        public void ChangePos(double curTime, string s, Vector2 Position)
        {
            seconds = curTime + 500;
            this.Position = Position;
            message = s;
        }

        public void Draw(double curTime,SpriteBatch spriteBatch)
        {
            if (seconds > curTime)
            {
                spriteBatch.DrawString(sf, message, Position, Color.White);
                Position.Y -= 2;
            }
        }

    }
}

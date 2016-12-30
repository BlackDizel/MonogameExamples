using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Engine.Model
{
    public class VisibleObject
    {
        public Vector2 Position;
        public Texture2D Texture;
        public Rectangle RecPos;

        public VisibleObject(Texture2D t, Vector2 pos)
        {
            Position = pos;
            Texture = t;
        }
        public VisibleObject(Texture2D t, Rectangle pos)
        {
            RecPos = pos;
            Texture = t;
        }
        public bool Contains(Vector2 v)
        {
            return RecPos.Contains((int)v.X, (int)v.Y);
        }
        /// <summary>
        /// конвертирует положение прямоугольника в вектор
        /// </summary>
        /// <returns></returns>
        public Vector2 recVec()
        {
            return new Vector2(RecPos.X, RecPos.Y);
        }
        /// <summary>
        /// конвертирует вектор позиции в точку позиции
        /// </summary>
        /// <returns></returns>
        public Point posPoint()
        {
            return new Point((int)Position.X, (int)Position.Y);
        }
    }
}

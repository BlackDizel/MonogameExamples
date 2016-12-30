using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Controllers;

namespace Engine.Controller
{
    public class Animation
    {
        private Texture2D texture;
        private AnimationBase animationBase;

        private Animation(Texture2D texture, AnimationBase animationBase)
        {
            this.texture = texture;
            this.animationBase = animationBase;
        }

        public static Animation AnimationMultilineWholeTexture(Texture2D texture, byte framesNum, byte framesInLine, double delayMillis)
        {
            return new Animation(texture
            , AnimationBase.AnimationMultilineWholeTexture(texture.Width, texture.Height, framesNum, framesInLine, delayMillis));
        }

        public static Animation AnimationSinglelineWholeTexture(Texture2D texture, byte framesCount, double delayMillis)
        {
            return new Animation(texture
            , AnimationBase.AnimationSinglelineWholeTexture(texture.Width, texture.Height, framesCount, delayMillis));
        }

        public static Animation AnimationSinglelineSourceRectangle(Texture2D texture, byte framesNum, byte delayMillis, Rectangle rectangle)
        {
            return new Animation(texture
            , AnimationBase.AnimationSinglelineSourceRectangle(framesNum, delayMillis, rectangle));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, bool drawMirroredHorizontal = false, bool drawMirroredVertical = false)
        {
            animationBase.Draw(spriteBatch, texture, position, drawMirroredHorizontal, drawMirroredVertical);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle recPos, bool drawMirroredHorizontal = false, bool drawMirroredVertical = false)
        {
            animationBase.Draw(spriteBatch, texture, recPos, drawMirroredHorizontal, drawMirroredVertical);
        }

    }

}

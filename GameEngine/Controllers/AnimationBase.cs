using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Controllers
{
    public class AnimationBase
    {
        private int frameWidth, frameHeight;
        private int frameLinesNum;
        private byte currentFrameNumber;
        private int framesInLine;
        private byte framesNum;
        private double delayMillis, lastFrameChangeTime;
        private Rectangle currentFrameRectangle;
        private Rectangle sourceRectangle;

        protected AnimationBase() { }

        public static AnimationBase AnimationMultilineWholeTexture(int width, int height, byte framesNum, byte framesInLine, double delayMillis)
        {
            AnimationBase animation = new AnimationBase(framesNum, framesInLine, delayMillis, width, height);
            animation.frameLinesNum = framesNum / framesInLine;
            if (framesNum % framesInLine != 0) ++animation.frameLinesNum; //округление в большую сторону

            return animation;
        }

        public static AnimationBase AnimationSinglelineWholeTexture(int width, int height, byte framesCount, double delay)
        {
            return new AnimationBase(framesCount, framesCount, delay, width, height);
        }

        public static AnimationBase AnimationSinglelineSourceRectangle(byte framesNum, byte delayMillis, Rectangle rectangle)
        {
            return new AnimationBase(framesNum, framesNum, delayMillis, rectangle);
        }

        private int FramesRowsNum
        {
            get
            {
                return framesNum / framesInLine + (framesNum % framesInLine != 0 ? 1 : 0);
            }
        }

        private AnimationBase(byte framesNum, byte framesInLine, double delayMillis, int width, int height) :
            this(framesNum, framesInLine, delayMillis, new Rectangle(0, 0, width, height)) { }

        private AnimationBase(byte framesNum, byte framesInLine, double delayMillis, Rectangle sourceRectangle)
        {
            this.framesInLine = framesInLine;
            this.framesNum = framesNum;
            this.currentFrameNumber = 0;
            this.delayMillis = delayMillis;

            this.sourceRectangle = sourceRectangle;

            this.frameWidth = sourceRectangle.Width / framesInLine;
            this.frameHeight = sourceRectangle.Height / FramesRowsNum;

            this.currentFrameRectangle = new Rectangle();
            this.currentFrameRectangle.Width = frameWidth;
            this.currentFrameRectangle.Height = frameHeight;
        }

        private SpriteEffects effects(bool drawMirroredHorizontal, bool drawMirroredVertical)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (drawMirroredHorizontal) effects = effects == SpriteEffects.None ? SpriteEffects.FlipHorizontally : effects | SpriteEffects.FlipHorizontally;
            if (drawMirroredVertical) effects = effects == SpriteEffects.None ? SpriteEffects.FlipVertically : effects | SpriteEffects.FlipVertically;
            return effects;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, bool drawMirroredHorizontal = false, bool drawMirroredVertical = false, Color? color = null)
        {
            spriteBatch.Draw(texture, position: position, sourceRectangle: currentFrameRectangle, effects: effects(drawMirroredHorizontal, drawMirroredVertical), color: color == null ? Color.White : color.Value);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Rectangle _recPos, bool drawMirroredHorizontal = false, bool drawMirroredVertical = false, Color? color = null)
        {
            spriteBatch.Draw(texture, destinationRectangle: _recPos, sourceRectangle: currentFrameRectangle, effects: effects(drawMirroredHorizontal, drawMirroredVertical), color: color == null ? Color.White : color.Value);
        }

        public void Update(GameTime gameTime)
        {
            currentFrameRectangle.X = sourceRectangle.X + frameWidth * (currentFrameNumber % framesInLine);
            currentFrameRectangle.Y = sourceRectangle.Y + frameHeight * (currentFrameNumber / framesInLine);

            if (lastFrameChangeTime + delayMillis < gameTime.TotalGameTime.TotalMilliseconds)
            {
                ++currentFrameNumber;
                currentFrameNumber %= framesNum;
                lastFrameChangeTime = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }
    }
}

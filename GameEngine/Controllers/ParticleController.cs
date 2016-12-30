using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Model
{
    public class ParticleController
    {
        public List<Particle> particles;

        private Texture2D dot;
        private Random random;

        public ParticleController()
        {
            this.particles = new List<Particle>();
            random = new Random();
        }

        public void LoadContent(Texture2D texture)
        {
            dot = texture;
        }


        /// <summary>
        /// генерация частиц
        /// </summary>
        /// <param name="position"></param>
        /// <param name="velocityX"></param>
        /// <param name="velocityY"></param>
        /// <param name="ttl"></param>
        /// <param name="alphaVel"></param>
        /// <param name="colorAlpha"></param>
        public void EngineRocket(Vector2 position,
                                 Vector2? positionRandom = null,
                                float velocityX = 0.0f,
                                float velocityY = 0.0f,
                                int ttl = 80,
                                float alphaVel = 0.01f,
                                float colorAlpha = 0.5f,
                                Vector2? velocityRandom = null)
        {
            Vector2 velocity = new Vector2(velocityX, velocityY);
            if (velocityRandom != null)
            {
                velocity.X += random.Next((int)-velocityRandom.Value.X, (int)velocityRandom.Value.X);
                velocity.Y += random.Next((int)-velocityRandom.Value.Y, (int)velocityRandom.Value.Y);
            }

            float angle = 0;
            float angleVel = 0;

            Vector4 color = new Vector4(1f, 1f, 1f, colorAlpha);

            float size = 1f;
            float sizeVel = 0;

            if (positionRandom != null)
                position += new Vector2(random.Next((int)-positionRandom.Value.X, (int)positionRandom.Value.X),
                                        random.Next((int)-positionRandom.Value.Y, (int)positionRandom.Value.Y));
            
            generateNewParticle(dot, position, velocity, angle, angleVel, color, size, ttl, sizeVel, alphaVel);
        }

        private void generateNewParticle(Texture2D texture
            , Vector2 position
            , Vector2 velocity,
            float angle
            , float angularVelocity
            , Vector4 color
            , float size
            , int ttl
            , float sizeVel
            , float alphaVel)
        {
            var particle = new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl, sizeVel, alphaVel);
            particles.Add(particle);
        }

        public void Update(GameTime gameTime)
        {
            if (particles != null && particles.Count > 0)
            {
                particles.ForEach(o => o.Update());
                particles.RemoveAll(o => o.Size <= 0 || o.TTL <= 0);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (particles != null && particles.Count > 0)
                particles.ForEach(i => i.Draw(spriteBatch));
        }
    }
}

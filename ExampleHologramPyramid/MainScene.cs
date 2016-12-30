using Engine.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleHologramPyramid
{
    class MainScene : Screen
    {
        private float angle;
        private Model mShip;
        private Texture2D texture;
        private Viewport vpLeft, vpTop, vpRight, vpBottom;
        private Matrix world = Matrix.CreateTranslation(Vector3.Zero);
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 1, 0.1f, 100f);
        private Matrix vLeft, vRight, vTop, vBottom;
        private Matrix[] transforms;
        private const int vpSize = 200;

        public void LoadScreenContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("ship2_texture");
            mShip = Content.Load<Model>("ship2");
            transforms = new Matrix[mShip.Bones.Count];
            mShip.CopyAbsoluteBoneTransformsTo(transforms);

            float cameraDistance = 20f;
            float cameraHeight = 10f;

            vLeft = Matrix.CreateLookAt(new Vector3(0, cameraHeight, -cameraDistance), Vector3.Zero, -Vector3.UnitX);
            vTop = Matrix.CreateLookAt(new Vector3(-cameraDistance, cameraHeight, 0), Vector3.Zero, Vector3.UnitY);
            vRight = Matrix.CreateLookAt(new Vector3(0, cameraHeight, cameraDistance), Vector3.Zero, -Vector3.UnitX);
            vBottom = Matrix.CreateLookAt(new Vector3(cameraDistance, cameraHeight, 0), Vector3.Zero, -Vector3.UnitY);

            vpLeft = viewport(GameController.Instance.ScreenWidth / 2 - vpSize
                            , GameController.Instance.ScreenHeight / 2 - vpSize / 2);

            vpTop = viewport(GameController.Instance.ScreenWidth / 2 - vpSize / 2
                            , GameController.Instance.ScreenHeight / 2 - vpSize);

            vpRight = viewport(GameController.Instance.ScreenWidth / 2
                            , GameController.Instance.ScreenHeight / 2 - vpSize / 2);

            vpBottom = viewport(GameController.Instance.ScreenWidth / 2 - vpSize / 2
                            , GameController.Instance.ScreenHeight / 2);
        }

        private Viewport viewport(int x, int y)
        {
            Viewport viewport = new Viewport();
            viewport.X = x;
            viewport.Y = y;
            viewport.Width = vpSize;
            viewport.Height = vpSize;
            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;
            return viewport;
        }

        public void Input(KeyboardState keyboardState
            , MouseState mouseState
            , GamePadState[] gamepadStates
            , TouchCollection touchCollection
            , GestureSample g
            , GameTime gameTime) { }
        public void UpdateScreen(GameTime gameTime)
        {
            angle = (angle + 0.01f) % 360;
            world = Matrix.CreateRotationY(angle);
        }

        private void drawShip(Matrix view, Viewport viewport)
        {
            GameController.Instance.GraphicDevice.Viewport = viewport;
            foreach (ModelMesh mesh in mShip.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    //region optional
                    effect.EnableDefaultLighting();
                    effect.LightingEnabled = true;
                    effect.TextureEnabled = true;

                    effect.DirectionalLight0.DiffuseColor = new Vector3(0.2f, 0.2f, 0.2f);
                    effect.DirectionalLight0.Direction = Vector3.Zero;
                    effect.DirectionalLight0.SpecularColor = Vector3.UnitZ;

                    effect.DirectionalLight1.DiffuseColor = new Vector3(0.2f, 0.2f, 0.2f);
                    effect.DirectionalLight1.Direction = Vector3.UnitZ;
                    effect.DirectionalLight1.SpecularColor = Vector3.UnitX;

                    //effect.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f);
                    //effect.EmissiveColor = Vector3.UnitX;
                    //endregion

                    effect.World = transforms[mesh.ParentBone.Index] * world;
                    effect.View = view;
                    effect.Projection = projection;
                    effect.Texture = texture;
                }
                mesh.Draw();
            }

        }

        public void DrawScreen(SpriteBatch spriteBatch)
        {
            Viewport original = GameController.Instance.GraphicDevice.Viewport;

            drawShip(vLeft, vpLeft);
            drawShip(vTop, vpTop);
            drawShip(vRight, vpRight);
            drawShip(vBottom, vpBottom);

            GameController.Instance.GraphicDevice.Viewport = original;
        }
    }
}

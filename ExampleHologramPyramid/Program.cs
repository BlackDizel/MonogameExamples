using Engine;
using Engine.Model;
using System;

namespace ExampleHologramPyramid
{
#if WINDOWS || LINUX

    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameMain())
            {
                GameController.Instance.StartScreen = new MainScene();
                game.Run();
            }
        }
    }
#endif
}

using Engine;
using Engine.Model;
using ExampleMinimap.View;
using System;

namespace ExampleMinimap
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameMain())
            {
                GameController.Instance.StartScreen = new SceneMinimap();
                game.Run();
            }
        }
    }
#endif
}

using Engine;
using Engine.Model;
using ExampleMap.View;
using System;

namespace ExampleMap
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameMain())
            {
                GameController.Instance.StartScreen = new MapScene();
                game.Run();
            }
        }
    }
#endif
}

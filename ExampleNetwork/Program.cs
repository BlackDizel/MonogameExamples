using Engine;
using Engine.Model;
using System;

namespace ExampleNetwork
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

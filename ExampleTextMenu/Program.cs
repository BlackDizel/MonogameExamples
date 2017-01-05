using Engine;
using Engine.Model;
using ExampleTextMenu.View;
using System;

namespace ExampleTextMenu
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameMain())
            {
                GameController.Instance.StartScreen = new MenuScene();
                game.Run();
            }
        }
    }
#endif
}

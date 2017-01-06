using Engine;
using Engine.Model;
using ExampleDialog.View;
using System;

namespace ExampleDialog
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameMain())
            {
                GameController.Instance.StartScreen = new SceneDialog();
                game.Run();
            }
        }
    }
#endif
}

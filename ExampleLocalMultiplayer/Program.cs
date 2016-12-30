using Engine;
using Engine.Model;
using ExampleLocalMultiplayer.View;
using System;

namespace ExampleLocalMultiplayer
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
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

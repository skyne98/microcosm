using System;

namespace Microcosm.Desktop
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MainGame())
            {
                game.Window.AllowUserResizing = true;
                game.Run();
            }
        }
    }
}

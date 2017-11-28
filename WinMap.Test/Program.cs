using RLNET;

namespace WinMan.Test
{
    internal class Program
    {
        private static MainGameScreen mainGameScreen;

#pragma warning disable RECS0154 // Parameter is never used

        private static void Main(string[] args)
#pragma warning restore RECS0154 // Parameter is never used
        {
            var settings = new RLSettings
            {
                BitmapFile = "terminal8x8.png",
                Width = 60,
                Height = 40,
                CharWidth = 8,
                CharHeight = 8,
                Scale = 1f,
                Title = "RLNET Window Manager Test",
                WindowBorder = RLWindowBorder.Resizable,
                ResizeType = RLResizeType.ResizeCells
            };
            // All the test panels here work with resize via scaling as well.
            //settings.ResizeType = RLResizeType.ResizeScale;

            Engine.Init(settings);

            mainGameScreen = new MainGameScreen();
            mainGameScreen.Show();

            Engine.Run();
        }
    }
}
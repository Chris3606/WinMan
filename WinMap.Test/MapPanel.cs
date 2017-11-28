using RLNET;
using System;

namespace WinMan.Test
{
    internal class MapPanel : Panel
    {
        private int[,] map;

        public MapPanel(ResizeCalc rootX, ResizeCalc rootY, ResizeCalc width, ResizeCalc height)
            : base(rootX, rootY, width, height, true)
        {
            Random rng = new Random();
            map = new int[Width, Height];

            for (int x = 0; x < Width; ++x)
                for (int y = 0; y < Height; ++y)
                    map[x, y] = rng.Next(0, 2);

            OnResize += (object s, EventArgs e) => resizeMap(widthCalc(), heightCalc());
        }

        // would be unnecessary here, honestly. just a hack to resize array so i dont have to have an actual map
        private void resizeMap(int width, int height)
        {
            Random rng = new Random();
            map = new int[width, height];

            for (int x = 0; x < width; ++x)
                for (int y = 0; y < height; ++y)
                    map[x, y] = rng.Next(0, 2);
        }

        public override void UpdateLayout(object sender, UpdateEventArgs e)
        {
            for (int x = 0; x < Width; ++x)
                for (int y = 0; y < Height; ++y)
                    if (map[x, y] == 0)
                        console.Set(x, y, RLColor.White, RLColor.Black, '#', 1);
                    else
                        console.Set(x, y, RLColor.White, RLColor.Black, '.', 1);
        }
    }
}
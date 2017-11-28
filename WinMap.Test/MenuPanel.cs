using RLNET;

namespace WinMan.Test
{
    internal class MenuPanel : Panel
    {
        public MenuPanel(ResizeCalc rootX, ResizeCalc rootY, ResizeCalc width, ResizeCalc height) : base(rootX, rootY, width, height, true, false)
        {
        }

        public override void UpdateLayout(object sender, UpdateEventArgs e)
        {
            for (int i = 0; i < 5; ++i)
                console.Print(1, i, "op" + i.ToString(), RLColor.White, RLColor.Black);
        }

        protected override void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            System.Console.WriteLine("Hello?");
            e.Cancel = true;
        }
    }
}
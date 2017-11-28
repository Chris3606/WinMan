using RLNET;

namespace WinMan.Test
{
    internal class AlertPanel : Panel
    {
        private string message;

        public AlertPanel(ResizeCalc centerX, ResizeCalc centerY, string message)
            : base(() => centerX() - (message.Length / 2), () => centerY() - 2, () => message.Length, Screen.SizeC(5), true, false)
        {
            this.message = message;
        }

        // Reposition map based on the following center coordinates
        //public void Reposition(int centerX, int centerY)
        //{
        //RootX = centerX - (message.Length / 2);
        //RootY = centerY - 2;
        //}

        public override void UpdateLayout(object sender, UpdateEventArgs e)
        {
            console.Print(0, 2, message, RLColor.White, 2);
        }

        protected override void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (Shown)
                Hide();

            // Greedy capture don't send it to anyone else! This basically sets this as modal window, nobody else can get any key input.
            // Defaults to false
            e.Cancel = true;
        }
    }
}
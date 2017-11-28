namespace WinMan.Test
{
    internal class MainGameScreen : Screen
    {
        private MapPanel mapPanel;
        private MenuPanel menuPanel;
        private AlertPanel alertPanel;

        public MainGameScreen()
        {
            mapPanel = new MapPanel(SizeC(0), SizeC(0), WidthMinus(10), HeightMinus(0));
            menuPanel = new MenuPanel(WidthMinus(10), SizeC(0), SizeC(10), HeightMinus(0));
            alertPanel = new AlertPanel(HalfWidth(), HalfHeight(), "I'm an overlay! Press a key to toggle me!");

            addPanel(mapPanel);
            addPanel(menuPanel);
            addPanel(alertPanel);
        }
    }
}
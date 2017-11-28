using RLNET;
using System.Collections.Generic;

namespace WinMan
{
    /// <summary>
    /// This class is designed to be subclassed to represent any collection of related panels.  This could be panels that resize relative to each other, or even
    /// just panels that will be shown and hidden as one unit.
    /// </summary>
    public abstract class Screen
    {
        private List<Panel> panels;

        /// <summary>
        /// Whether or not the screen's panels are to be rendered (shown) or not.  call Show and Hide to change this.
        /// </summary>
        public bool Shown { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Screen()
        {
            panels = new List<Panel>();
            Engine.RootConsole.OnResize += onResize;
        }

        /// <summary>
        /// Simply calls all panel's show functions, if the screens are not already showing.
        /// </summary>
        public void Show()
        {
            if (!Shown)
            {
                Shown = true;

                foreach (var panel in panels)
                    panel.Show();
            }
            else
                System.Console.WriteLine("WARNING: Tried to show screen that was already shown... BAD!");
        }

        /// <summary>
        /// Calls all panel's hide functions, if the screen is not already hidden.
        /// </summary>
        public void Hide()
        {
            if (Shown)
            {
                Shown = false;

                foreach (var panel in panels)
                    panel.Hide();
            }
            else
                System.Console.WriteLine("WARNING: Tried to hide screen that was already hidden... BAD!");
        }

        private void onResize(object sender, ResizeEventArgs e)
        {
            foreach (Panel panel in panels)
                panel.onScreenResize(sender, e);
        }

        /// <summary>
        /// Returns a resising delegate that always returns the size specified.  Ex., SizeC(10) returns you a function that always returns 10.
        /// </summary>
        /// <param name="size">The size for the generated lambda function to always return.</param>
        /// <returns>A lambda function that always returns the integer size specified.</returns>
        public static ResizeCalc SizeC(int size)
        {
            return () => { return size; };
        }

        /// <summary>
        /// Returns a resizing delegate that returns the width of the root console, minus a specific value.  Ex,. WidthMinus(10) returns you a function
        /// that will return Engine.RootConsole.Width - 10
        /// </summary>
        /// <param name="subtract">Amount to subtract from the root console's width.</param>
        /// <returns>A lambda function that will return the root console's width, subtract the specified value when it is called.</returns>
        public static ResizeCalc WidthMinus(int subtract)
        {
            return () => { return Engine.RootConsole.Width - subtract; };
        }

        /// <summary>
        /// Returns a resizing delegate that returns the height of the root console, minus a specific value when it is called.  Ex,. HeightMinus(10)
        /// returns you a function that will return Engine.RootConsole.Height - 10.
        /// </summary>
        /// <param name="subtract">Amount to subtract from the root console's width.</param>
        /// <returns>A lambda function that will return the root console's height, subtract the specified value when it is called.</returns>
        public static ResizeCalc HeightMinus(int subtract)
        {
            return () => { return Engine.RootConsole.Height - subtract; };
        }

        /// <summary>
        /// Returns a resizing delegate that returns exactly half the width of the root console when it is called.
        /// </summary>
        /// <returns>A lambda function that will return exactly half the root console's width.</returns>
        public static ResizeCalc HalfWidth()
        {
            return () => { return Engine.RootConsole.Width / 2; };
        }

        /// <summary>
        /// Returns a resizing delegate that returns exactly half the height of the root console when it is called.
        /// </summary>
        /// <returns>A lambda function that will return exactly half the root console's height.</returns>
        public static ResizeCalc HalfHeight()
        {
            return () => { return Engine.RootConsole.Height / 2; };
        }

        /// <summary>
        /// Adds the panel to a list that keeps track of all panels that are part of this screen.  This MUST be called from subclass's constructor when a new panel
        /// is created in order for show and hide to work properly.
        /// </summary>
        /// <param name="panel">The panel to add to the list.</param>
        protected void addPanel(Panel panel)
        {
            if (!panels.Contains(panel))
                panels.Add(panel);
            else
                System.Console.WriteLine("WARNING: Tried to add a panel to a screen twice, ignoring second add.  This is a bug...");
        }

        /// <summary>
        /// Removes a panel from an internal list.  Should a panel ever cease to be part of a screen, this function must be called at that time.
        /// </summary>
        /// <param name="panel">The panel that is no longer part of the screen.</param>
        protected void removePanel(Panel panel) => panels.Remove(panel);
    }
}
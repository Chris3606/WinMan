namespace WinMan
{
    /// <summary>
    /// KeyHandler can be subclassed to represent anything that handles keys.  Panels have an OnKeyPress function by default, so this class
    /// can be subclassed to represents something that ONLY handles keys.  This allows objects that are not panels to handle keypresses.
    /// </summary>
    abstract public class KeyHandler
    {
        /// <summary>
        /// Whether or not this key handling is currently among those set to receive keypresses.  StartHandling and StopHandling modify
        /// this value.
        /// </summary>
        public bool Handling { get; private set; }

        /// <summary>
        /// Constructor.  By default, it does NOT begin handling key presses.  Call StartHandling to cause the handler to begin receiving key presses.
        /// </summary>
        public KeyHandler()
        {
            Handling = false;
        }

        /// <summary>
        /// Should be implemented to handle key presses as necessary.  If a key should be handled in such a way as that subsequent panel/key handlers should
        /// not be given the opportunity to handle the key, change the Cancel parameter of the KeyPressEventArgs given to true.
        /// </summary>
        /// <param name="sender">Sender of the key press.</param>
        /// <param name="e">Argument telling what key was pressed, etc., as well as as allowing the event to be "Canceled" such that it will not
        /// propegate to subsequent handlers.</param>
        abstract protected void OnKeyPress(object sender, KeyPressEventArgs e);

        /// <summary>
        /// Adds this KeyHandler to the top of the stack of handlers that receive keypresses, if it is not already doing so.  Otherwise,
        /// prints a warning to console and does nothing.
        /// </summary>
        public void StartHandling()
        {
            if (!Handling)
            {
                Engine.AddToKeyPressFront(OnKeyPress);
                Handling = true;
            }
            else
                System.Console.WriteLine("WARNING: Tried to start a keyhandler that was already handling... BAD!!!");
        }

        /// <summary>
        /// Prevents this key handler from receiving key presses by removing it from the list of handlers, if is currently handling keys.
        /// Otherwise, prints a warning to the console and does nothing.
        /// </summary>
        public void StopHandling()
        {
            if (Handling)
            {
                Engine.KeyPress -= OnKeyPress;
                Handling = false;
            }
            else
                System.Console.WriteLine("WARNING: Tried to stop a keyhandler that was already inactive... BAD!!!");
        }
    }
}
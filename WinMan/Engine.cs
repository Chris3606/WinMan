using RLNET;
using System;

namespace WinMan
{
    /// <summary>
    /// Passed to KeyPressEventHandler functions.  Contains an RLKeyPress detailing information about the key that was pressed. Cancelable.
    /// </summary>
    public class KeyPressEventArgs : EventArgs
    {
        /// <summary>
        /// Information about the key that was pressed.
        /// </summary>
        public RLKeyPress KeyPress { get; private set; }

        /// <summary>
        /// Whether or not the event is to be cancelled.  If you wish to supress a keypress, to say that you've handled it, in your KeyPressEventHandler function,
        /// set the KeyPressEventArgs Cancel to true.  The event will then automatically not be passed on to any further handlers.
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="keyPress">RLKeyPress representing the key that was pressed.</param>
        public KeyPressEventArgs(RLKeyPress keyPress)
        {
            KeyPress = keyPress;
            Cancel = false;
        }
    }

    /// <summary>
    /// Contains some information and events that are effectively global.  The only thing that really needs to be done with this class outside of the base Panel and
    /// Screen class is to call Init when the program starts.  This is effectively they way to get yourself a root console.  Then call Run() when you want to call the
    /// root console's run.  Any time you need access to the root console, it is static so from any class Engine.rootConsole is sufficient.
    /// </summary>
    public static class Engine
    {
        /// <summary>
        /// The root console for the program.
        /// </summary>
        public static RLRootConsole RootConsole { get; private set; } = null;

        /// <summary>
        /// Any event handlers that are added to this event are called BEFORE any offscreen consoles are blitted to the root console, but AFTER any Update()
        /// handlers.  Used by panels that need to update their offscreen consoles (clear and reset them) in real-time (every time a render frame happens)
        /// NOTE: the actual RENDERING is real-time regardless.  Every frame, the ROOT console is cleared, and all offscreen consoles from panels are blitted to it.
        /// </summary>
        public static event UpdateEventHandler UpdateRealTimeLayouts = null;

        /// <summary>
        /// This is the event that is designed to call every panel's render function each frame.  Panels automatically add their render functions to this handler when
        /// they are shown, there is no need to do it manually.
        /// </summary>
        public static event UpdateEventHandler Render = null;

        /// <summary>
        /// This event is designed to be used by panels to handle keys that are pressed.  It is cancelable.
        /// </summary>
        public static event EventHandler<KeyPressEventArgs> KeyPress = null;

        /// <summary>
        /// Whether or not the window is fullscreen.
        /// </summary>
        public static bool Fullscreen { get; private set; }

        /// <summary>
        /// Just initializes RLRootConsole. Don't call more than once, should just be likely at the start of your main.  WILL ERROR IF CALLED MORE THAN ONCE
        /// </summary>
        /// <param name="settings">Settings passed to root console to initialize.</param>
        public static void Init(RLSettings settings)
        {
            if (RootConsole == null)
            {
                if (settings.StartWindowState == RLWindowState.Fullscreen)
                    Fullscreen = true;
                else
                    Fullscreen = false;

                RootConsole = new RLRootConsole(settings);
                RootConsole.Update += onUpdate;
                RootConsole.Render += onRender;
            }
            else
            {
                throw new Exception("Init already called!");
            }
        }

        /// <summary>
        /// Kick off the entire system.  Effectively like calling the run of the root console.
        /// </summary>
        public static void Run() => RootConsole.Run();

        /// <summary>
        /// Makes the window fullscreen if it is not, or normal if it is fullscreen.
        /// </summary>
        public static void ToggleFullscreen()
        {
            if (!Fullscreen)
            {
                RootConsole.SetWindowState(RLWindowState.Fullscreen);
                Fullscreen = true;
            }
            else
            {
                RootConsole.SetWindowState(RLWindowState.Normal);
                Fullscreen = false;
            }
        }

        private static void onUpdate(object sender, UpdateEventArgs e)
        {
            if (KeyPress != null)
            {
                RLKeyPress key = RootConsole.Keyboard.GetKeyPress();

                if (key != null)
                {
                    KeyPressEventArgs arg = new KeyPressEventArgs(key);

                    foreach (EventHandler<KeyPressEventArgs> func in KeyPress.GetInvocationList())
                    {
                        func(sender, arg);
                        if (arg.Cancel)
                            break;
                    }
                }
            }
        }

        private static void onRender(object sender, UpdateEventArgs e)
        {
            // Tell all panels that want to update in real time to do so
            UpdateRealTimeLayouts?.Invoke(sender, e);

            // Draw all shown screens
            RootConsole.Clear();
            Render?.Invoke(sender, e);

            RootConsole.Draw();
        }

        internal static void AddToKeyPressFront(EventHandler<KeyPressEventArgs> handler) => KeyPress = handler + KeyPress;
    }
}
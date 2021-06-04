// <copyright file="KaLE.InputManager.cs">
// Copyright (c) 2021. All Rights Reserved
// </copyright>
// <author>
// Marvin Fuchs
// </author>
// <summary>
// Visit https://marvin-fuchs.de for more information
// </summary>

namespace KaLE
{
    using System.Windows.Forms;
    using System;
    using Gma.System.MouseKeyHook;
    using GameSense.Animation;
    using GameSense;

    /// <summary>
    /// Class responsible for managing keyboard and mouse inputs.
    /// </summary>
    public class InputManager
    {
        private static readonly IKeyboardMouseEvents GlobalHook = Hook.GlobalEvents();
        private static readonly Logger Logger = new Logger()
        {
            Ident = "InputManager",
            LogDebug = false,
        };

        static InputManager()
        {
            Logger.Log("Starting...", Logger.Type.Info);
            GlobalHook.KeyDown += KeyEvent;
            GlobalHook.MouseDownExt += MouseEvent;
            Logger.Log("Ready!", Logger.Type.Info);
        }

        /// <summary>
        /// Keys and their corresponding scan code.
        /// </summary>
        private enum ScanCode
        {
            Esc = 1,
            F1 = 59,
            F2 = 60,
            F3 = 61,
            F4 = 62,
            F5 = 63,
            F6 = 64,
            F7 = 65,
            F8 = 66,
            F9 = 67,
            F10 = 68,
            F11 = 87,
            F12 = 88,
            PrintScreen = 55,
            ScrollLock = 70,
            Pause = 69,
            Circumflex = 41,
            One = 2,
            Two = 3,
            Three = 4,
            Four = 5,
            Five = 6,
            Six = 7,
            Seven = 8,
            Eight = 9,
            Nine = 10,
            Zero = 11,
            ß = 12,
            Apostrophe = 13,
            Backspace = 14,
            M1 = 82,
            M2 = 71,
            M3 = 73,
            NumLock = 69,
            Divide = 53,
            Multiply = 55,
            Subtract = 74,
            Tab = 15,
            Q = 16,
            W = 17,
            E = 18,
            R = 19,
            T = 20,
            Z = 21,
            U = 22,
            I = 23,
            O = 24,
            P = 25,
            Ü = 526,
            Plus = 27,
            M4 = 83,
            M5 = 79,
            M6 = 81,
            NumSeven = 71,
            NumEight = 72,
            NumNine = 73,
            Add = 78,
            CapsLock = 58,
            A = 30,
            S = 31,
            D = 32,
            F = 33,
            G = 34,
            H = 35,
            J = 36,
            K = 37,
            L = 38,
            Ö = 39,
            Ä = 40,
            Hashtag = 43,
            Enter = 28,
            NumFour = 75,
            NumFive = 76,
            NumSix = 77,
            Shift = 42,
            LessThan = 86,
            Y = 44,
            X = 45,
            C = 46,
            V = 47,
            B = 48,
            N = 49,
            M = 50,
            Colon = 51,
            Period = 52,
            Hyphen = 53,
            ShiftRight = 54,
            Up = 72,
            NumOne = 79,
            NumTwo = 107,
            NumThree = 108,
            NumEnter = 109,
            ControleLeft = 110,
            WindowsLeft = 111,
            AltLeft = 112,
            Space = 116,
            AltRight = 120,
            WindowsRight = 121,
            Steelseries = 122,
            ControleRight = 123,
            Left = 125,
            Down = 126,
            Right = 127,
            NumZero = 129,
            Dot = 130,
        }

        /// <summary>
        /// Starts the <see cref="InputManager"/>
        /// </summary>
        public static void Start()
        {
        }

        /// <summary>
        /// Ends the <see cref="InputManager"/>. Should be called at the end of the program.
        /// </summary>
        public static void End()
        {
            GlobalHook.MouseDownExt -= MouseEvent;
            GlobalHook.KeyDown -= KeyEvent;

            GlobalHook.Dispose();
        }

        private static void KeyEvent(object sender, KeyEventArgs eventArgs)
        {
            try
            {
                Logger.Log(((Key)Enum.Parse(typeof(Key), eventArgs.KeyCode.ToString())).ToString());
                FrameManager.AddKeyAnimation(new KeyFade()
                {
                    Key = (Key)Enum.Parse(typeof(Key), eventArgs.KeyCode.ToString())
                });
            }
            catch (ArgumentException)
            {
                Logger.Log("Key " + eventArgs.KeyCode.ToString() + " does not exist", Logger.Type.Warning);
            }
        }

        private static void MouseEvent(object sender, MouseEventArgs eventArgs)
        {
            Logger.Log(eventArgs.Button.ToString());
        }
    }
}

// <copyright file="Program.cs">
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
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Logging;
    using GameSense.Animation;

    /// <summary>
    /// The main class of this project.
    /// </summary>
    public class Program
    {
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        private static bool visible;

        private static readonly IntPtr Window = GetConsoleWindow();

        private static readonly Logger Logger = new Logger
        {
            Ident = "Main",
        };

        private static void Main(string[] args)
        {
            ShowWindow(Window, SW_HIDE);
            Logger.Log("Program started. Welcome.", LoggerType.Info);
            GameSense.Controller.Background = new KeyboardGradient(new int[] { 255, 85, 0 }, new int[] { 0, 196, 255 }, 4, 2);
            GameSense.Controller.DefaultKeyAnimation = new KeyFade();

            // Create notify icon
            NotifyIcon notifyIcon =  new NotifyIcon();
            notifyIcon.Text = "KaLE";
            notifyIcon.Icon = SystemIcons.Application;
            notifyIcon.Visible = true;
            notifyIcon.Click += (sender, e) =>
            {
                if (visible)
                {
                    ShowWindow(Window, SW_HIDE);

                }
                else
                {
                    ShowWindow(Window, SW_SHOW);
                }
                visible = !visible;
            };

            Application.Run(new ApplicationContext());

            GameSense.Controller.Stop();
            Application.Exit();
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}

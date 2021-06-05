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
    using GameSense.Animation;
    using Logging;

    /// <summary>
    /// The main class of this project.
    /// </summary>
    public class Program
    {
        private const int SwHide = 0;
        private const int SwShow = 5;

        private static readonly IntPtr Window = GetConsoleWindow();

        private static readonly Logger Logger = new Logger
        {
            Ident = "Main",
        };

        private static bool visible;

        private static void Main(string[] args)
        {
            ShowWindow(Window, SwHide);
            Logger.Log("Program started. Welcome.", LoggerType.Info);
            GameSense.Controller.Background = new KeyboardGradient(new int[] { 255, 85, 0 }, new int[] { 0, 196, 255 }, 4, 2);
            GameSense.Controller.DefaultKeyAnimation = new KeyFade();
            GameSense.Controller.GameName = "KALE";
            GameSense.Controller.GameDisplayName = "KaLE";
            GameSense.Controller.Developer = "Marvin Fuchs";
            GameSense.Controller.GradientColor1 = new GameSense.Struct.Request.Color
            {
                Red = 255,
                Green = 85,
                Blue = 0
            };

            GameSense.Controller.GradientColor2 = new GameSense.Struct.Request.Color
            {
                Red = 0,
                Green = 196,
                Blue = 255
            };

            // Create notify icon
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Text = "KaLE";
            notifyIcon.Icon = SystemIcons.Application;
            notifyIcon.Visible = true;
            notifyIcon.Click += (sender, e) =>
            {
                if (visible)
                {
                    ShowWindow(Window, SwHide);
                }
                else
                {
                    ShowWindow(Window, SwShow);
                }

                visible = !visible;
            };

            Application.Run(new ApplicationContext());

            GameSense.Controller.Stop();
            Application.Exit();
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr window, int comand);
    }
}

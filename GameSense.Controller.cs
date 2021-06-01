// <copyright file="GameSense.Controller.cs">
// Copyright (c) 2021. All Rights Reserved
// </copyright>
// <author>
// Marvin Fuchs
// </author>
// <summary>
// Visit https://marvin-fuchs.de for more information
// </summary>

namespace GameSense
{
    using System;
    using GameSense.Animation;
    using GameSense.Struct;
    using KaLE;

    /// <summary>
    /// Controls communication with the game sense API and the animation cycle.
    /// </summary>
    public class Controller
    {
        private static readonly IAnimator Background = new KeyboardGradient(new int[] { 255, 85, 0 }, new int[] { 0, 196, 255 }, 4, 2);

        ////private static read only IAnimator Background = new KeyboardTest();
        private static readonly Logger Logger = new Logger
        {
            Ident = "GameSense/Controller",
        };

        private static readonly int FrameLength = 50;
        public static readonly string GameName = "KALE";

        static Controller()
        {
            Logger.Log("Starting...", Logger.Type.Info);
            registerGame();
            startHeartbeat();
            bindEvents();

            Logger.Log("Ready!", Logger.Type.Info);
        }

        private static void registerGame()
        {
            Logger.Log("Registering game...", Logger.Type.Info);
            Transmitter.Send(new Request
            {
                Game = GameName,
                GameDisplayName = "KaLE",
                Developer = "Marvin Fuchs"
            }, "game_metadata");
        }

        private static void startHeartbeat()
        {
            System.Timers.Timer timer = new System.Timers.Timer(10000);
            timer.Elapsed += heartbeat;
            timer.AutoReset = true;
            timer.Enabled = true;
            Logger.Log("Timer startet.", Logger.Type.Info);
        }

        private static void bindEvents()
        {
            Logger.Log("Binding events...", Logger.Type.Info);

            //Full keyboard effect
            Transmitter.Send(new Request
            {
                Game = GameName,
                Event = "KEYBOARD_BITMAP",
                Handlers = new Handler[]
                {
                    new Handler
                    {
                        DeviceType = "rgb-per-key-zones",
                        Mode = "bitmap"
                    }
                }
            }, "bind_game_event");
            Handler[] handlers = new Handler[] { new Handler() };
            Request request = new Request
            {
                Game = GameName,
                Event = "KEYBOARD_BITMAP",
                Handlers = new Handler[]
                {
                    new Handler
                    {
                        DeviceType = "rgb-per-key-zones",
                        Mode = "bitmap"
                    }
                }
            };
            System.Timers.Timer timer = new System.Timers.Timer(FrameLength);
            timer.Elapsed += keyboardEffect;
            timer.AutoReset = true;
            timer.Enabled = true;
            keyboardEffect(null, null);
            Logger.Log("Background-Effect binned!", Logger.Type.Info);
        }

        private static void heartbeat(Object source, System.Timers.ElapsedEventArgs e)
        {
            Logger.Log("Heartbeat...", Logger.Type.Info);
            Transmitter.Send(new Request { Game = GameName }, "game_heartbeat");
        }

        private static void keyboardEffect(Object source, System.Timers.ElapsedEventArgs e)
        {
            Logger.Log("Keyboard-Effect...", Logger.Type.Debug);
            Transmitter.Send(new Request
            {
                Game = GameName,
                Event = "KEYBOARD_BITMAP",
                Data = new RequestData
                {
                    Frame = Background.NextFrame()
                }
            }, "game_event");
        }

        public static void Start()
        {
        }
    }
}

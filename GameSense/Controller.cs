// <copyright file="Controller.cs">
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
    using System.Timers;
    using GameSense.Animation;
    using GameSense.Struct;
    using Logging;

    /// <summary>
    /// Controls communication with the game sense API and the animation cycle.
    /// </summary>
    public class Controller
    {
        public static readonly string GameName = "KALE";

        private static readonly Timer UpdateTimer = new Timer(50);

        ////private static read only IAnimator Background = new KeyboardTest();
        private static readonly Logger Logger = new Logger
        {
            Ident = "GameSense/Controller",
        };

        static Controller()
        {
            Logger.Log("Starting...", LoggerType.Info);
            RegisterGame();
            StartHeartbeat();
            BindEvents();
            StartUpdate();

            Logger.Log("Ready!", LoggerType.Info);
        }

        public static IAnimator Background
        {
            set
            {
                FrameManager.Background = value;
                UpdateTimer.Enabled = true;
                Logger.Log("Background set.", LoggerType.Info);
            }
        }

        public static IKeyAnimator DefaultKeyAnimation
        {
            set
            {
                InputManager.DefaultKeyAnimation = value;
            }
        }

        /// <summary>
        /// Initialize the <see cref="GameSense.Controller"/> and start game sense.
        /// </summary>
        public static void Start()
        {
        }

        public static void Stop()
        {
            InputManager.Stop();
        }

        private static void RegisterGame()
        {
            Logger.Log("Registering game...", LoggerType.Info);
            Transmitter.Send(
                new Request
                {
                    Game = GameName,
                    GameDisplayName = "KaLE",
                    Developer = "Marvin Fuchs"
                },
                "game_metadata");
        }

        private static void BindEvents()
        {
            Logger.Log("Binding events...", LoggerType.Info);

            // Full keyboard effect
            Transmitter.Send(
                new Request
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
                },
                "bind_game_event");
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

            Logger.Log("Keyboard effect binned!", LoggerType.Info);
        }

        private static void StartHeartbeat()
        {
            Timer timer = new System.Timers.Timer(10000);
            timer.Elapsed += Heartbeat;
            timer.AutoReset = true;
            timer.Enabled = true;
            Logger.Log("Heartbeat started.", LoggerType.Info);
        }

        private static void StartUpdate()
        {
            UpdateTimer.Elapsed += Update;
            UpdateTimer.AutoReset = true;
            UpdateTimer.Enabled = false;
            Logger.Log("UpdateTimer ready.", LoggerType.Info);
        }

        private static void Heartbeat(object source, System.Timers.ElapsedEventArgs e)
        {
            ////Logger.Log("Heartbeat...", LoggerType.Info);
            Transmitter.Send(new Request { Game = GameName }, "game_heartbeat");
        }

        private static void Update(object source, System.Timers.ElapsedEventArgs e)
        {
            Logger.Log("Keyboard-Effect...", LoggerType.Debug);
            Transmitter.Send(
                new Request
                {
                    Game = GameName,
                    Event = "KEYBOARD_BITMAP",
                    Data = new RequestData
                    {
                        Frame = FrameManager.Generate()
                    }
                },
                "game_event");
        }
    }
}

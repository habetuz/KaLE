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
    using GameSense.Struct.Request;
    using Logging;

    /// <summary>
    /// Controls communication with the game sense API and the animation cycle. It starts automatically when <see cref="GradientColor1"/>, <see cref="GradientColor2"/>, <see cref="GameName"/>, <see cref="GameDisplayName"/> and <see cref="Developer"/> are set.
    /// </summary>
    public class Controller
    {
        private static readonly Timer UpdateTimer = new Timer(50);

        private static readonly Logger Logger = new Logger
        {
            Ident = "GameSense/Controller",
        };

        private static string gameName;
        private static string gameDisplayName;
        private static string developer;
        private static Color? gradientColor1;
        private static Color? gradientColor2;

        /// <summary>
        /// Sets the <see cref="IAnimator"/> used for the keyboard background.
        /// </summary>
        public static IKeyboardAnimator Background
        {
            set
            {
                KeyboardFrameManager.Background = value;
                UpdateTimer.Enabled = true;
                Logger.Log("Background set.", LoggerType.Info);
            }
        }

        /// <summary>
        /// Sets the default <see cref="IKeyAnimator"/> used when a key gets pressed.
        /// </summary>
        public static IKeyAnimator DefaultKeyAnimation
        {
            set
            {
                InputManager.DefaultKeyAnimation = value;
            }
        }

        /// <summary>
        /// Sets the game name for the game sense engine.
        /// </summary>
        public static string GameName
        {
            set
            {
                Logger.Log("Name set: " + value, LoggerType.Info);
                gameName = value;
                if (ReadyForInitialization())
                {
                    Start();
                }
            }
        }

        /// <summary>
        /// Sets the name that is displayed in the game sense engine.
        /// </summary>
        public static string GameDisplayName
        {
            set
            {
                Logger.Log("Display name set: " + value, LoggerType.Info);
                gameDisplayName = value;
                if (ReadyForInitialization())
                {
                    Start();
                }
            }
        }

        /// <summary>
        /// Sets the developer of the project.
        /// </summary>
        public static string Developer
        {
            set
            {
                Logger.Log("Developer set: " + value, LoggerType.Info);
                developer = value;
                if (ReadyForInitialization())
                {
                    Start();
                }
            }
        }

        /// <summary>
        /// Sets the first <see cref="Color"/> for the gradient that will be the color range for any <see cref="IMouseAnimator"/> and <see cref="IMousePadAnimator"/>.
        /// </summary>
        public static Color GradientColor1
        {
            set
            {
                Logger.Log("Gradient color 1 set", LoggerType.Info);
                gradientColor1 = value;
                if (ReadyForInitialization())
                {
                    Start();
                }
            }
        }

        /// <summary>
        /// Sets the second <see cref="Color"/> for the gradient that will be the color range for any <see cref="IMouseAnimator"/> and <see cref="IMousePadAnimator"/>.
        /// </summary>
        public static Color GradientColor2
        {
            set
            {
                Logger.Log("Gradient color 2 set", LoggerType.Info);
                gradientColor2 = value;
                if (ReadyForInitialization())
                {
                    Start();
                }
            }
        }

        /// <summary>
        /// Initialize the <see cref="GameSense.Controller"/> and start game sense.
        /// </summary>
        public static void Start()
        {
            Logger.Log("Starting...", LoggerType.Info);

            RegisterGame();
            StartHeartbeat();
            BindEvents();
            StartUpdate();

            Logger.Log("Ready!", LoggerType.Info);
        }

        /// <summary>
        /// Stops game sense. Should be called at the end of the program.
        /// </summary>
        public static void Stop()
        {
            InputManager.Stop();
        }

        private static bool ReadyForInitialization()
        {
            return
                gradientColor1 != null &&
                gradientColor2 != null &&
                gameDisplayName != null &&
                developer != null &&
                gameName != null;
        }

        private static void RegisterGame()
        {
            Logger.Log("Registering game...", LoggerType.Info);
            Transmitter.Send(
                new BaseRequest
                {
                    Game = gameName,
                    GameDisplayName = gameDisplayName,
                    Developer = developer
                },
                "game_metadata");
        }

        private static void BindEvents()
        {
            Logger.Log("Binding events...", LoggerType.Info);

            // Full keyboard effect
            Transmitter.Send(
                new BaseRequest
                {
                    Game = gameName,
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
            Logger.Log("Keyboard effect binned!", LoggerType.Info);

            // Mouse effect
            Transmitter.Send(
                new BaseRequest
                {
                    Game = gameName,
                    Event = "MOUSE_WHEEL",
                    MinValue = 0,
                    MaxValue = 100,
                    ValueOptional = false,
                    Handlers = new Handler[]
                    {
                        new Handler
                        {
                            DeviceType = "mouse",
                            Zone = "wheel",
                            Color = new ColorHandler
                            {
                                Gradient = new ColorHandlerGradient
                                {
                                    Zero = (Color)gradientColor1,
                                    Hundred = (Color)gradientColor2
                                }
                            }
                        }
                    }
                },
                "bind_game_event");
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
            Logger.Log("UpdateTimer ready.", LoggerType.Info);
        }

        private static void Heartbeat(object source, System.Timers.ElapsedEventArgs e)
        {
            ////Logger.Log("Heartbeat...", LoggerType.Info);
            Transmitter.Send(new BaseRequest { Game = gameName }, "game_heartbeat");
        }

        private static void Update(object source, System.Timers.ElapsedEventArgs e)
        {
            Logger.Log("Keyboard-Effect...", LoggerType.Debug);
            Transmitter.Send(
                new BaseRequest
                {
                    Game = gameName,
                    Events = new EventBinder[]
                    {
                        new EventBinder
                        {
                            Event = "KEYBOARD_BITMAP",
                            Data = new RequestData
                            {
                                Frame = KeyboardFrameManager.Generate()
                            }
                        }
                    }
                },
                "multiple_game_events");
        }
    }
}

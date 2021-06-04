// <copyright file="GameSense.FrameManager.cs">
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
    using System.Collections.Generic;
    using GameSense.Animation;
    using KaLE;

    /// <summary>
    /// Keeps track of all <see cref="GameSense.Animation.IAnimator"/> and combines the <see cref="GameSense.Struct.Frame"/>s from <see cref="GameSense.Animation.IAnimator.NextFrame(Struct.Frame)"/> to one final <see cref="GameSense.Struct.Frame"/>
    /// </summary>
    public class FrameManager
    {
        private static readonly Logger Logger = new Logger()
        {
            Ident = "FrameManager",
            LogDebug = false
        };

        private static readonly List<IKeyAnimator> PressedKeys = new List<IKeyAnimator>();
        private static IAnimator background;

        /// <summary>
        /// Sets the <see cref="GameSense.Animation.IAnimator"/> for the background.
        /// </summary>
        public static IAnimator Background
        {
            set { background = value; }
        }

        public static void AddKeyAnimation(IKeyAnimator keyAnimation)
        {
            Logger.Log(PressedKeys.TrueForAll(animator => animator.Key != keyAnimation.Key) + "");
            PressedKeys.RemoveAll(animator => animator.Key == keyAnimation.Key);
            PressedKeys.Add(keyAnimation);

        }

        /// <summary>
        /// Combines all stored <see cref="GameSense.Animation.IAnimator"/> to one <see cref="GameSense.Struct.Frame"/> by calling their <see cref="GameSense.Animation.IAnimator.NextFrame(Frame)"/> method.
        /// </summary>
        /// <returns>The combined <see cref="GameSense.Struct.Frame"/></returns>
        public static Frame Generate()
        {
            Frame frame = background.NextFrame().Copy();
            Logger.Log(PressedKeys.Count + "");
            PressedKeys.ForEach(key => frame = key.NextFrame(frame));
            PressedKeys.RemoveAll(key => key.Finished);

            return frame;
        }
    }
}

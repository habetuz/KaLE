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
    using GameSense.Struct;

    /// <summary>
    /// Keeps track of all <see cref="GameSense.Animation.IAnimator"/> and combines the <see cref="GameSense.Struct.Frame"/>s from <see cref="GameSense.Animation.IAnimator.NextFrame(Struct.Frame)"/> to one final <see cref="GameSense.Struct.Frame"/>
    /// </summary>
    public class FrameManager
    {
        private static IAnimator background;
        private static List<IAnimator> pressedKeys;
        
        /// <summary>
        /// Sets the <see cref="GameSense.Animation.IAnimator"/> for the background.
        /// </summary>
        public static IAnimator Background
        {
            set { background = value; }
        }

        /// <summary>
        /// Gets a list containing <see cref="GameSense.Animation.IAnimator"/>s. These <see cref="GameSense.Animation.IAnimator"/>s represent pressed keys.
        /// </summary>
        public static List<IAnimator> PressedKeys
        {
            get { return pressedKeys; }
        }

        /// <summary>
        /// Combines all stored <see cref="GameSense.Animation.IAnimator"/> to one <see cref="GameSense.Struct.Frame"/> by calling their <see cref="GameSense.Animation.IAnimator.NextFrame(Frame)"/> method.
        /// </summary>
        /// <returns>The combined <see cref="GameSense.Struct.Frame"/></returns>
        public static Frame Generate()
        {
            Frame frame = background.NextFrame();
            foreach (IAnimator key in pressedKeys)
            {
                frame = key.NextFrame(frame);
            }

            return frame;
        }
    }
}

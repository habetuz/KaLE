// <copyright file="GameSense.FrameManager.cs">
// Copyright (c) 2021. All Rights Reserved
// </copyright>
// <author>
// Marvin Fuchs
// </author>
// <summary>
// Visit https://marvin-fuchs.de for more information
// </summary>

using GameSense.Animation;
using GameSense.Struct;
using System.Collections.Generic;

namespace GameSense
{
    public class FrameManager
    {
        private static IAnimator _background;
        public static IAnimator Background { set { _background = value; } }

        private static List<IAnimator> _pressedKeys;
        public static IAnimator PressedKey { set { _pressedKeys.Add(value); } }

        public static Frame Generate()
        {
            Frame frame = _background.NextFrame();
            foreach (IAnimator key in _pressedKeys)
            {
                frame = key.NextFrame(frame);
            }
            return frame;
        }
    }
}

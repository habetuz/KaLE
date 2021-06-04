// <copyright file="GameSense.Animation.IAnimator.cs">
// Copyright (c) 2021. All Rights Reserved
// </copyright>
// <author>
// Marvin Fuchs
// </author>
// <summary>
// Visit https://marvin-fuchs.de for more information
// </summary>

namespace GameSense.Animation
{
    /// <summary>
    /// Interface for animations.
    /// </summary>
    public interface IAnimator
    {
        /// <summary>
        /// Generates the next <see cref="Frame"/>.
        /// </summary>
        /// <param name="bottomLayer">The bottom <see cref="Frame"/> the method will add it's <see cref="Frame"/></param>
        /// <returns>the next <see cref="Frame"/></returns>
        Frame NextFrame(Frame bottomLayer = null);
    }
}

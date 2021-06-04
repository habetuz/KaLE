// <copyright file="GameSense.Animation.KeyFade.cs">
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
    using KaLE;

    /// <summary>
    /// An <see cref="IAnimator"/> that animates a fading color for pressed keys
    /// </summary>
    public class KeyFade : IKeyAnimator
    {
        private static readonly Logger Logger = new Logger()
        {
            Ident = "KeyFade",
            LogDebug = false
        };

        private int fadeDuration = 30;
        private int transparency = 100;
        private bool finished = false;
        private int[] color = new int[] { 255, 255, 255 };
        private Key key;

        /// <summary>
        /// Sets amount of <see cref="GameSense.Animation.IAnimator.NextFrame(Frame)"/> calls the key needs to fade out. Time dependents on the <see cref="GameSense.Controller.FrameLength"/>. Default: 100.
        /// </summary>
        public int FadeDuration
        {
            set
            {
                this.fadeDuration = value;
            }
        }

        /// <summary>
        /// Sets the key that was pressed.
        /// </summary>
        public Key Key
        {
            set { this.key = value; }
            get { return this.key; }
        }

        /// <summary>
        /// Sets the color that should fade away. Default: 255(R)|255(G)|255(B)
        /// </summary>
        public int[] Color
        {
            set { this.color = value; }
        }

        /// <summary>
        /// Gets a value indicating whether the animation is finished. If 'true' the animation finished and the object can be discarded.
        /// </summary>
        public bool Finished
        {
            get { return this.finished; }
        }

        public Frame NextFrame(Frame bottomLayer)
        {
            this.transparency -= 100 / this.fadeDuration;
            Logger.Log("Next frame. Transparency: " + this.transparency);
            if (this.transparency <= 0)
            {
                this.finished = true;
                return bottomLayer;
            }

            return bottomLayer.SetColor(
                (int)this.key, 
                ColorManipulation.Combine(
                    bottomLayer.Bitmap[(int)this.key], 
                    this.color, 
                    this.transparency)
                );
        }
    }
}

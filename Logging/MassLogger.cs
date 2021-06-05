// <copyright file="MassLogger.cs">
// Copyright (c) 2021. All Rights Reserved
// </copyright>
// <author>
// Marvin Fuchs
// </author>
// <summary>
// Visit https://marvin-fuchs.de for more information
// </summary>

namespace Logging
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extends <see cref="Logger"/> to log a massive amount of info logs. It does that by collecting info logs and logging them together and compressed at specific times.
    /// </summary>
    public class MassLogger : Logger
    {
        private readonly Dictionary<string, int> pairs = new Dictionary<string, int>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MassLogger"/> class.
        /// </summary>
        /// <param name="logPause">The pause between two info logs in milliseconds.</param>
        public MassLogger(int logPause) : base()
        {
            System.Timers.Timer timer = new System.Timers.Timer(logPause);
            timer.Elapsed += this.Log;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        /// <summary>
        /// Gets or sets the text that is displayed before the info log.
        /// </summary>
        public string InfoLogText { get; set; } = string.Empty;

        /// <summary>
        /// Logs to the console with time, origin and type information.
        /// </summary>
        /// <param name="text">The text to be logged</param>
        /// <param name="type">The type of the log. </param>
        /// <param name="instant">If true event <see cref="LoggerType.Info"/> gets logged instant.</param>
        public void Log(string text, LoggerType type, bool instant)
        {
            if (instant)
            {
                base.Log(text, type);
            }
            else
            {
                this.Log(text, type);
            }
        }

        /// <summary>
        /// Logs to the console with time, origin and type information.
        /// </summary>
        /// <param name="text">The text to be logged</param>
        /// <param name="type">The type of the log. <see cref="LoggerType.Info"/> does not get logged instant. It gets collected and logged after some time. Add true to log instant."/></param>
        public new void Log(string text, LoggerType type)
        {
            if (type != LoggerType.Info)
            {
                base.Log(text, type);
                return;
            }

            if (this.pairs.ContainsKey(text))
            {
                this.pairs[text]++;
            }
            else
            {
                this.pairs.Add(text, 1);
            }
        }

        private void Log(object source, System.Timers.ElapsedEventArgs e)
        {
            base.Log(this.InfoLogText, LoggerType.Info);
            foreach (KeyValuePair<string, int> entry in this.pairs)
            {
                Console.WriteLine("{0}x {1}", entry.Value, entry.Key);
            }

            Console.WriteLine();
            this.pairs.Clear();
        }
    }
}
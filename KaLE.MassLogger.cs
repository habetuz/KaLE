// <copyright file="KaLE.MassLogger.cs">
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
    using System.Collections.Generic;

    public class MassLogger : Logger
    {
        private readonly Dictionary<string, int> pairs = new Dictionary<string, int>();

        public MassLogger(int logPause) : base()
        {
            System.Timers.Timer timer = new System.Timers.Timer(logPause);
            timer.Elapsed += Log;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public string InfoLogText { get; set; } = string.Empty;

        public void Log(string text, LoggerType type, bool instant)
        {
            if (instant)
            {
                base.Log(text, type);
            } 
            else
            {
                Log(text, type);
            }
        }

        new public void Log(string text, LoggerType type)
        {
            if (type != LoggerType.Info)
            {
                base.Log(text, type);
                return;
            }
            
            if (pairs.ContainsKey(text))
            {
                pairs[text]++;
            }
            else
            {
                pairs.Add(text, 1);
            }
        }

        private void Log(object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine();
            base.Log(InfoLogText, LoggerType.Info);
            foreach (KeyValuePair<string, int> entry in pairs)
            {
                Console.WriteLine("{0}x {1}", entry.Value, entry.Key);
            }
        }
    }
}

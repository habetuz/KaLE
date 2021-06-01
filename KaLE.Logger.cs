// <copyright file="KaLE.Logger.cs">
// Copyright (c) 2021. All Rights Reserved
// </copyright>
// <author>
// Marvin Fuchs
// </author>
// <summary>
// Visit https://marvin-fuchs.de for more information
// </summary>

using System;

namespace KaLE
{
    public class Logger
    {
        public enum Type
        {
            Debug, Info, Warning, Error
        }

        public string Ident { get; set; } = "NoName";
        public bool LogDebug { get; set; } = false;
        public bool LogInfo { get; set; } = true;
        public bool LogWarning { get; set; } = true;
        public bool LogError { get; set; } = true;

        public void Log(string text, Type type = Type.Debug)
        {
            switch (type)
            {
                case Type.Debug: if (LogDebug) goto default; else return;
                case Type.Info: if (LogInfo) goto default; else return;
                case Type.Warning: if (LogWarning) goto default; else return;
                case Type.Error: if (LogError) goto default; else return;
                default: Console.WriteLine("[{0}] [{1}] [{2}]: {3}", DateTime.Now.ToString(), Ident, type.ToString(), text); break;
            }
        }
    }
}
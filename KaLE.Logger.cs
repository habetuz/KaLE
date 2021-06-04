// <copyright file="KaLE.Logger.cs">
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

    /// <summary>
    /// Class for uniformly and clear logging over the whole project.
    /// </summary>
    public class Logger
    {
        private string ident = "NoName";
        private bool logDebug = false;
        private bool logInfo = true;
        private bool logWarning = true;
        private bool logError = true;

        // TODO: Change enum to flags and remove bools.

        /// <summary>
        /// The logging level.
        /// </summary>
        public enum Type
        {
            Debug, Info, Warning, Error
        }

        /// <summary>
        /// Sets identification of the logger.
        /// </summary>
        public string Ident
        {
            set
            {
                this.ident = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether <see cref="Type.Debug"/> should be logged.
        /// </summary>
        public bool LogDebug
        {
            set
            {
                this.logDebug = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether <see cref="Type.Info"/> should be logged.
        /// </summary>
        public bool LogInfo
        {
            set
            {
                this.logInfo = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether <see cref="Type.Warning"/> should be logged.
        /// </summary>
        public bool LogWarning
        {
            set
            {
                this.logWarning = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether <see cref="Type.Error"/> should be logged.
        /// </summary>
        public bool LogError
        {
            set
            {
                this.logError = value;
            }
        }

        /// <summary>
        /// Logs to the console with time, origin and type information.
        /// </summary>
        /// <param name="text">The text to be logged</param>
        /// <param name="type">The type of the log.</param>
        public void Log(string text, Type type = Type.Debug)
        {
            switch (type)
            {
                case Type.Debug:
                    if (this.logDebug)
                    {
                        goto default;
                    }
                    else
                    {
                        return;
                    }

                case Type.Info:
                    if (this.logInfo)
                    {
                        goto default;
                    }
                    else
                    {
                        return;
                    }

                case Type.Warning:
                    if (this.logWarning)
                    {
                        goto default;
                    }
                    else
                    {
                        return;
                    }

                case Type.Error:
                    if (this.logError)
                    {
                        goto default;
                    }
                    else
                    {
                        return;
                    }

                default:
                    Console.WriteLine(
                        "[{0}] [{1}] [{2}]: {3}",
                        DateTime.Now.ToString(),
                        type.ToString(),
                        this.ident,
                        text);
                    break;
            }
        }
    }
}

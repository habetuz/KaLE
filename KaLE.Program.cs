// <copyright file="KaLE.Program.cs">
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
    using System.Threading;

    /// <summary>
    /// The main class of this project.
    /// </summary>
    public class Program
    {
        private static readonly Logger Logger = new Logger
        {
            Ident = "Main",
        };

        private static void Main(string[] args)
        {
            Logger.Log("Program started. Welcome.", Logger.Type.Info);
            ////GameSense.Controller.Start();
            new Thread(KeepAlive).Start();
            
            InputManager.Start();

            ////InputManager.End();
        }
        
        private static void KeepAlive()
        {
            Console.ReadLine();
        }
    }
}

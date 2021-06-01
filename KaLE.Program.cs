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

    public class Program
    {
        private static Logger _logger = new Logger
        {
            Ident = "Main",
        };

        private static void Main(string[] args)
        {
            _logger.Log("Program startet. Welcome.", Logger.Type.Info);
            GameSense.Controller.Start();
            //InputManager.Start();
            Console.ReadLine();
            //InputManager.End();
        }
    }
}

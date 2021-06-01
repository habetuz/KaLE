// <copyright file="GameSense.Struct.Frame.cs">
// Copyright (c) 2021. All Rights Reserved
// </copyright>
// <author>
// Marvin Fuchs
// </author>
// <summary>
// Visit https://marvin-fuchs.de for more information
// </summary>

namespace GameSense.Struct
{
    public class Frame
    {
        public Frame()
        {
            Bitmap = new int[132][];
            int[] standardColor = new int[3];
            for (int i = 0; i < Bitmap.Length; i++)
            {
                Bitmap[i] = standardColor;
            }
        }

        public int[][] Bitmap { get; set; }

        public Frame SetColor(int index, int r, int g, int b)
        {
            return SetColor(index, new int[] { r, g, b });
        }

        public Frame SetColor(int index, int[] color)
        {
            Bitmap[index] = color;
            return this;
        }
    }
}
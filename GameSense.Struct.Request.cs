// <copyright file="GameSense.Struct.Request.cs">
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
    public class Request
    {
        public string Game { get; set; }
        public string GameDisplayName { get; set; }
        public string Developer { get; set; }
        public string Event { get; set; }
        public int MinValue { get; set; } = 0;
        public int MaxValue { get; set; } = 1;
        public int IconId { get; set; } = 0;
        public bool ValueOptional { get; set; } = true;
        public Handler[] Handlers { get; set; }
        public RequestData Data { get; set; }
    }
}
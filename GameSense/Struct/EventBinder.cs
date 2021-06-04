// <copyright file="Event.cs">
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
    public struct EventBinder
    {
        /// <summary>
        /// Gets or sets the event.
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// Gets or sets the request-data.
        /// </summary>
        public RequestData Data { get; set; }
    }
}

// <copyright file="GameSense.GSJsonNamingPolicy.cs">
// Copyright (c) 2021. All Rights Reserved
// </copyright>
// <author>
// Marvin Fuchs
// </author>
// <summary>
// Visit https://marvin-fuchs.de for more information
// </summary>

using KaLE;
using System.Collections.Generic;
using System.Text.Json;

namespace GameSense
{
    public class GSJsonNamingPolicy : JsonNamingPolicy
    {
        private static readonly Dictionary<string, string> _dotNetGSPairs = new Dictionary<string, string> {
            {"GameDisplayName", "game_display_name"},
            {"MinValue", "min_value"},
            {"MaxValue", "max_value"},
            {"IconId", "icon_id"},
            {"ValueOptional", "value_optional"},
            {"DeviceType", "device-type"},
        };

        private static readonly Logger _logger = new Logger
        {
            Ident = "GameSense/GSJsonNamingPolicy",
        };

        public GSJsonNamingPolicy()
        {
        }

        public override string ConvertName(string name)
        {
            //Logger.Log("Converting " + name + " to " + _dotNetGSPairs[name]);
            try { return _dotNetGSPairs[name]; }
            catch { return name.ToLower(); }
        }
    }
}
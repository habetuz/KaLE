// <copyright file="GameSense.Transmitter.cs">
// Copyright (c) 2021. All Rights Reserved
// </copyright>
// <author>
// Marvin Fuchs
// </author>
// <summary>
// Visit https://marvin-fuchs.de for more information
// </summary>

using GameSense.Struct;
using KaLE;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace GameSense
{
    public class Transmitter
    {
        private static readonly Logger _logger = new Logger
        {
            Ident = "GameSense/Transmitter",
            LogInfo = false,
        };

        private static readonly HttpClient _client = new HttpClient();

        private static readonly JsonSerializerOptions _serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        private static readonly string _address;

        static Transmitter()
        {
            _logger.Log("Starting...", Logger.Type.Info);
            try
            {
                string file = System.IO.File.ReadAllText("C:/ProgramData/SteelSeries/SteelSeries Engine 3/coreProps.json");
                CoreProps coreProps = JsonSerializer.Deserialize<CoreProps>(file, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                _address = coreProps.Address;
                _logger.Log("GameSense server is running on " + _address, Logger.Type.Info);
                _logger.Log("Ready!", Logger.Type.Info);
            }
            catch (Exception ex)
            {
                if (ex is System.IO.DirectoryNotFoundException || ex is System.IO.FileNotFoundException)
                {
                    _logger.Log("coreProps.json could not be found. Maybe the SteelSeries Engine is not running.", Logger.Type.Error);
                }
                else
                {
                    _logger.Log("coreProps.json cannot be deserialized", Logger.Type.Error);
                }
                _logger.Log("Error:\n" + ex.ToString(), Logger.Type.Error);
            }
        }

        public async static void Send(Request request, string endpoint)
        {
            //Logger.Log("Data: " + request.Game + " | " + request.Handlers.Length +  " | " + request.Handlers[0].DeviceType);
            HttpContent payload = new StringContent
            (
                JsonSerializer.Serialize<Request>(request, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = new GSJsonNamingPolicy(),
                    WriteIndented = false,
                }),
                Encoding.UTF8,
                "application/json"
            );

            //Logger.Log(JsonSerializer.Serialize<Request>(request, new JsonSerializerOptions{PropertyNamingPolicy = new GSJsonNamingPolicy(),WriteIndented = true}));

            try
            {
                //Logger.Log("Sending a request to endpoint " + endpoint + "...", Logger.Type.Info);
                HttpResponseMessage response = await _client.PostAsync("http://" + _address + "/" + endpoint, payload);

                Logger.Type type;
                if (response.IsSuccessStatusCode) type = Logger.Type.Info;
                else type = Logger.Type.Warning;

                _logger.Log("Request to endpoint '" + endpoint + "' received! Status: " + response.StatusCode /*+ " | Content: " + await response.Content.ReadAsStringAsync()*/, type);
            }
            catch (Exception ex)
            {
                _logger.Log("Request to endpoint '" + endpoint + "' failed!\n" + ex.ToString(), Logger.Type.Warning);
            }
        }
    }
}
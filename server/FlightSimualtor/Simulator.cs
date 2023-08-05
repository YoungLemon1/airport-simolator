using FlightSimulator.Models;
using FlightSimulator.Models.Enums;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;
using System;
using FlightSimulator.Extentions;
using System.Diagnostics;

namespace FlightSimualtor
{
    public class Simulator : ISimulator
    {
        private readonly Random random = new();
        private readonly HttpClient _client;
        private static int lastId;
        private static bool simulationRunning = true;
        private readonly string? requestUrl = "https://localhost:5140/api/Home/Post";
        public Simulator(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
        }
        public async void RunSimulator(int id)
        {
            using var client = new HttpClient();
            lastId = id;
            while (simulationRunning)
            {
                await Task.Run(async () =>
                {
                    var flight = new Flight
                    {
                        Id = lastId++, FType = random.Next(2) == 0 ? FlightType.landing : FlightType.takeoff, Name = Extentions.GenerateRandomFlightName(), Stop = null
                    };
                    using var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                    var json = JsonSerializer.Serialize(flight);
                    try
                    {
                        using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                          request.Content = stringContent;
                        var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                        response.EnsureSuccessStatusCode();
                        Debug.WriteLine(response.StatusCode.ToString());
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                    await Task.Delay(random.Next(1000, 5000));
                });
            }
        }
        public void StartSimulation()
        {
            simulationRunning = true;
        }
        public void StopSimulation()
        {
            simulationRunning = false;
        }
    }
}
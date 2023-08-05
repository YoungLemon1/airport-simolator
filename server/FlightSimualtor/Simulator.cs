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
using System.Net;
using System.Net.Http.Json;

namespace FlightSimualtor
{
    public class Simulator : ISimulator
    {
        private readonly Random random = new();
        private readonly HttpClient _client;
        private static int lastId;
        private static bool simulationRunning = true;
        private readonly string? requestUrl = "http://localhost:5140/api/Home/Post";
        public Simulator(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
        }
        public async void GenerateFlights(int id)
        {
            lastId = id;
            while (simulationRunning)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Generating Flight");

                var flight = new Flight
                {
                    Id = lastId++,
                    FType = random.Next(2) == 0 ? FlightType.landing : FlightType.takeoff,
                    Name = Extentions.GenerateRandomFlightName(),
                    CreatedTime = DateTime.Now,
                    Stop = null
                };

                Console.WriteLine(flight.Id);

                var json = JsonSerializer.Serialize(flight);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using var response = await _client.PostAsync(requestUrl, content);

                if (response.IsSuccessStatusCode)
                {
                        Console.WriteLine("Flight generated successfully");
                        Console.WriteLine($"Flight ID: {flight.Id}");
                        Console.WriteLine($"Flight Type: {flight.FType}");
                        Console.WriteLine($"Flight Name: {flight.Name}");
                        Console.WriteLine($"Created Time: {flight.CreatedTime}");
                }
                else
                {
                    Console.WriteLine("Server error: Failed to generate flight. Status 500");
                }
                await Task.Delay(random.Next(1000, 5000));
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
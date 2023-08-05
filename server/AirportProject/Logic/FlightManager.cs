using System.Xml.Linq;
using System.Diagnostics;
using static System.Net.WebRequestMethods;
using static FlightSimulator.Models.Flight;
using FlightSimulator.Models.Enums;
using FlightSimulator.Repositories;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Eventing.Reader;

using FlightSimulator.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace FlightSimulator.Logic
{
    public class FlightManager : IFlightManager
    {
        ///private readonly IRepository _repository;
        public Stop[] Stops = Array.Empty<Stop>();
        public Stop[][] LandingPath = Array.Empty<Stop[]>();
        public Stop[][] TakeoffPath = Array.Empty<Stop[]>();
        public Stop[] TakeoffPathStart = Array.Empty<Stop>();
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly object _lock = new();
        private readonly Random random = new();
        //capacity of concurrent airport flights
        private readonly SemaphoreSlim airportCapacity = new(4);

        public FlightManager(IServiceScopeFactory scopeFactory)
        {
            serviceScopeFactory = scopeFactory;
        }
        public void Initialize()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IRepository>();
            Stops = repository.GetStops().ToArray();
            var gate1 = Stops[5];
            var gate2 = Stops[6];
            TakeoffPathStart = new Stop[] { gate1, gate2};
            LandingPath = new Stop[][] { new Stop[] { Stops[0] }, new Stop[] { Stops[1] }, new Stop[] { Stops[2] }, new Stop[] { Stops[3] }, new Stop[] { Stops[4] }, new Stop[] { gate1, gate2 } };
            TakeoffPath = new Stop[][] { new Stop[] { gate2, gate1 }, new Stop[] { Stops[7] }, new Stop[] { Stops[3] } };
        }

        public async Task StartFlight(Flight flight)
        {
            Stop[][] path;
            await airportCapacity.WaitAsync();
            if (flight.FType == FlightType.landing)
                path = LandingPath;
            else
                path = TakeoffPath;
            using var scope = serviceScopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IRepository>();
            await RunFlight(flight, path, repository);
        }

        private async Task RunFlight(Flight flight, Stop[][] path, IRepository repository)
        {
            await Task.Run(async () =>
            {
                foreach (Stop[] nextStops in path)
                {
                    var nextStop = nextStops.First();
                    Stop? stop = flight.Stop;
                    //Edge case: Takeoff start, Landing End
                    if (nextStops.Length > 1)
                    {
                        if (nextStop.semaphore.CurrentCount == 0)
                        {
                            var alternateStop = nextStops.Last();
                            nextStop = alternateStop;
                        }
                    }
                    await nextStop.semaphore.WaitAsync();
                    lock (_lock)
                    {
                        try
                        {
                            Debug.WriteLine($"FlightManager - flight {flight.Name}: id:{flight.Id}" +
                                $"Type:{flight.FType}, currentStop:{flight.StopId} nextStop:{nextStop.Id} Time:{DateTime.Now}");
                            ReleaseStop(flight.Stop, repository);
                            flight.Stop = nextStop;
                            nextStop.Flight = flight;
                            UpdateFlight(flight, repository);
                            UpdateStop(nextStop, repository);
                            repository.SaveAllChanges();
                        }
                        finally
                        {
                        }
                    }
                    await Task.Delay(random.Next(2000, 4000));

                }
                lock (_lock)
                {
                    Debug.WriteLine($"FlightManager - flight {flight.Name}: id:{flight.Id}, Type:{flight.FType}, currentStop:{flight.StopId} EXITING Time:{DateTime.Now}");
                    ReleaseStop(flight.Stop, repository);
                    flight.Stop=null;
                    UpdateFlight(flight, repository);
                    repository.SaveAllChanges();
                    airportCapacity.Release();
                }
            });
        }

        private static void ReleaseStop(Stop? stop, IRepository repository)
        {
            Debug.WriteLine($"FlightManager - ReleaseStop for {stop?.Id} Time:{DateTime.Now}");
            if (stop is not null)
            {
                stop.Flight = null;
                UpdateStop(stop, repository);
                stop.semaphore.Release();
                Debug.WriteLine($"FlightManager - release semaphore for {stop.Id} Time:{DateTime.Now}");
            }
        }
        private static void UpdateStop(Stop stop, IRepository repository)
        {
            repository.UpdateStop(stop);
        }
        private static void UpdateFlight(Flight flight, IRepository repository)
        {
            repository.UpdateFlight(flight);
        }
    }
}

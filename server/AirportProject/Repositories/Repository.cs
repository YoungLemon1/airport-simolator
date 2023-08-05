using FlightSimulator.Data;
using FlightSimulator.Models;
using FlightSimulator.Models.Enums;

namespace FlightSimulator.Repositories
{
    public class Repository : IRepository
    {
        private readonly AirportContext _context;

        public Repository(AirportContext context)
        {
            _context = context;
        }

        public void InsertFlight(Flight flight)
        {
            _context.Add(flight);
            _context.SaveChanges();
        }

        public Flight? GetFlightById(int id)
        {
            return _context.Flights.FirstOrDefault(f => f.Id == id);
        }

        public Flight? GetFlightByName(string name)
        {
            return _context.Flights.FirstOrDefault(f => f.Name == name);
        }

        public IEnumerable<Flight> GetFlights()
        {
            return _context.Flights;
        }

        public IEnumerable<Flight> GetFlights(FlightType flightType)
        {
            return _context.Flights.Where(f => f.FType == flightType);
        }

        public Stop? GetStopById(int id)
        {
            return _context.Stops.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Stop> GetStops()
        {
            return _context.Stops;
        }

        public void UpdateFlight(Flight flight)
        {
            _context.Update(flight);
          
        }
        public void DeleteFlight(Flight flight)
        {
            _context.Remove(flight);
        }
        public void UpdateStop(Stop stop)
        {
            _context.Update(stop);
        }
        public void SaveAllChanges()
        {
            _context.SaveChanges();
        }
    }
}

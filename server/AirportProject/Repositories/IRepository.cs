using FlightSimulator.Models;
using FlightSimulator.Models.Enums;

namespace FlightSimulator.Repositories
{
    public interface IRepository
    {
        IEnumerable<Flight> GetFlights();
        IEnumerable<Flight> GetFlights(FlightType flightType);
        IEnumerable<Stop> GetStops();
        Flight? GetFlightById(int id);
        Flight? GetFlightByName(string name);
        Stop? GetStopById(int id);
        void UpdateStop(Stop stop);
        void InsertFlight(Flight flight);
        void UpdateFlight(Flight flight);
        void DeleteFlight(Flight flight);
        void SaveAllChanges();
    }
}

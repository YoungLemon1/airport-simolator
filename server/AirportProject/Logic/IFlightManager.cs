using FlightSimulator.Models;

namespace FlightSimulator.Logic
{
    public interface IFlightManager
    {
        void Initialize();
        Task StartFlight(Flight flight);
    }
}
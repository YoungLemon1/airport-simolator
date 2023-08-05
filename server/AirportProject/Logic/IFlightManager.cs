using FlightSimulator.Models;

namespace FlightSimulator.Logic
{
    public interface IFlightManager
    {
        void Initialize();
        Task ManageFlight(Flight flight);
        Task ContinueFlight(Flight flight);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimualtor
{
    public interface ISimulator
    {
        void GenerateFlights(int id);
        public void StartSimulation();
        void StopSimulation();
    }
}

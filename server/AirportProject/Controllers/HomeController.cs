using FlightSimulator.Logic;
using FlightSimulator.Models;
using FlightSimulator.Repositories;
using FlightSimualtor;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Dynamic;
using FlightSimulator.Models.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlightSimulator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        private readonly IFlightManager _flightManager;
        private readonly ISimulator _simulator;

        public HomeController(IRepository repository, IFlightManager flightManager, ISimulator simulator)
        {
            _repository = repository;
            _flightManager = flightManager;
            _simulator = simulator;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        [Route("Get")]
        public JsonResult Get()
        {
            IEnumerable<Stop> stops = _repository.GetStops();
            IEnumerable<Flight> flights = _repository.GetFlights();
            dynamic res = new { stops, flights };
            return Json(res);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Flight? Get(int id)
        {
            var flight = _repository.GetFlightById(id);
            if (flight == null)
            {
                BadRequest();
                return null;
            }
            else return flight;
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("Post")]
        public void Post([FromBody] Flight flight)
        {
            _repository.InsertFlight(flight);
            _flightManager.StartFlight(flight);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id)
        {
            var flight = _repository.GetFlightById(id);
            if(flight == null)
                BadRequest();
            else _repository.UpdateFlight(flight);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var flight = _repository.GetFlightById(id);
            if (flight == null)
                BadRequest();
            else _repository.DeleteFlight(flight);
        }
        [Route("Start")]
        public void RunSimulator()
        {
            var flights = _repository.GetFlights();
            int lastFlightId = 1;
            if (flights.Any())
              lastFlightId = flights.Last().Id + 1;
            _simulator.StartSimulation();
            _simulator.RunSimulator(lastFlightId);
        }
        [Route("Stop")]
        public void StopSimulation()
        {
            _simulator.StopSimulation();
        }
    }
}

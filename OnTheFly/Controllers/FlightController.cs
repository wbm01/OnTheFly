using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using OnTheFly.FlightsService.DTOs;
using OnTheFly.Services;

namespace OnTheFly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly FlightService _flightService;

        public FlightController()
        {
            _flightService = new();
        }

        [HttpGet(Name = "Get Flights")]
        public Task<List<Flight>> GetFlights()
        {
            return _flightService.GetFlights();
        }

        [HttpGet("{iata:length(3)}, {rab:length(6)}", Name = "Get Flight")]
        public Task<Flight> GetFlight(string iata, string rab, string departude)
        {
            return _flightService.GetFlight(iata, rab, departude);
        }

        [HttpPost(Name = "Create Flight")]
        public Task<ActionResult<Flight>> CreateFlight(CreateFlightDTO flightDTO)
        {
            return _flightService.CreateFlight(flightDTO);
        }

        [HttpPut("{iata:length(3)}, {rab:length(6)}", Name = "Update Flight")]
        public Task<Flight> UpdateFlight(string iata, string rab, string departure, UpdateFlightDTO flightDTO)
        {
            return _flightService.UpdateFlight(iata, rab, departure, flightDTO);
        }

        [HttpDelete("{iata:length(3)}, {rab:length(6)}", Name = "Delete Flight")]
        public Task<ActionResult<Flight>> DeleteFlight(string iata, string rab, string departure)
        {
            return _flightService.DeleteFlight(iata, rab, departure);
        }
    }
}

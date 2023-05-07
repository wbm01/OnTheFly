using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using OnTheFly.FlightsService.DTOs;
using OnTheFly.FlightsService.Services;

namespace OnTheFly.FlightsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly FlightService _flightService;

        public FlightsController(FlightService service)
        {
            _flightService = service;
        }

        [HttpGet(Name = "Get All Flights")]
        public List<Flight> GetFlights()
        {
            return _flightService.GetFlights();
        }

        [HttpGet("{IATA}, {RAB}", Name = "Get Flight")]
        public Flight GetFlight(string IATA, string RAB)
        {
            return _flightService.GetFlight(IATA, RAB);
        }

        [HttpPost(Name = "Create Flight")]
        public Task<Flight> CreateFlight(CreateFlightDTO flightDTO)
        {
            return _flightService.CreateFlight(flightDTO);
        }

        [HttpPut("{IATA}, {RAB}", Name = "Update Flight")]
        public Flight UpdateFlight(string IATA, string RAB, Flight flight)
        {
            return _flightService.UpdateFlight(IATA, RAB, flight);
        }

        [HttpDelete("{IATA}, {RAB}", Name = "Delete Flight")]
        public ActionResult<Flight> DeleteFlight(string IATA, string RAB)
        {
            return _flightService.DeleteFlight(IATA, RAB);
        }
    }
}

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

        [HttpGet("{IATA:length(3)}, {RAB:length(6)}", Name = "Get Flight")]
        public ActionResult<Flight> GetFlight(string IATA, string RAB, string departure)
        {
            return _flightService.GetFlight(IATA, RAB, departure);
        }

        [HttpPost(Name = "Create Flight")]
        public Task<ActionResult<Flight>> CreateFlight(CreateFlightDTO flightDTO)
        {
            return _flightService.CreateFlight(flightDTO);
        }

        [HttpPut("{IATA}, {RAB}", Name = "Update Flight")]
        public ActionResult<Flight> UpdateFlight(string IATA, string RAB, UpdateFlightDTO flightDTO)
        {
            return _flightService.UpdateFlight(IATA, RAB, flightDTO);
        }

        [HttpDelete("{IATA}, {RAB}", Name = "Delete Flight")]
        public ActionResult<Flight> DeleteFlight(string IATA, string RAB, string departure)
        {
            return _flightService.DeleteFlight(IATA, RAB, departure);
        }
    }
}

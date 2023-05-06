using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using OnTheFly.AirportServices.Services;

namespace OnTheFly.AirportServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly AirportService _airportService;

        public AirportController()
        {
            _airportService = new();
        }

        [HttpGet(Name = "Get Airports")]
        public List<Airport> GetAirports()
        {
            return _airportService.GetAirports();
        }

        [HttpGet("{IATA}", Name = "Get Airport By IATA")]
        public Airport GetAirportByIATA(string IATA)
        {
            return _airportService.GetAirportByIATA(IATA);
        }

        [HttpPost(Name = "Create Airport")]
        public Airport CreateAirport(Airport airport)
        {
            return _airportService.CreateAirport(airport);
        }

        [HttpPut("{IATA}", Name = "Update Airport")]
        public Airport UpdateAirport(string IATA, Airport airport)
        {
            return _airportService.UpdateAirport(IATA, airport);
        }

        [HttpDelete("{IATA}", Name = "Delete Airport")]
        public ActionResult DeleteAirport(string IATA)
        {
            _airportService.DeleteAirport(IATA);

            return NoContent();
        }
    }
}

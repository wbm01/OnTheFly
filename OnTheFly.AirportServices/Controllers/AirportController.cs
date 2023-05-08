using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using OnTheFly.AirportServices.Config;
using OnTheFly.AirportServices.Services;

namespace OnTheFly.AirportServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly AirportService _airportService;
        private readonly IMongoDBConfig _mongoDbConfig;

        public AirportController(IMongoDBConfig config)
        {
            _mongoDbConfig = config;
            _airportService = new(config);

        }

        [HttpGet(Name = "GetAll")]
        public List<Airport> GetAirports()
        {
            return _airportService.GetAirports();
        }

        [HttpGet("{iata}", Name = "GetAirportIata")]
        public ActionResult<Airport> Get(string iata)
        {
            var airport = _airportService.GetAirportByIATA(iata);

            if (airport == null)
                return NotFound();

            return airport;
        }

        /*[HttpPost(Name = "Create Airport")]
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
        }*/
    }
}

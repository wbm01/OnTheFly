using Microsoft.AspNetCore.Mvc;
using Models;
using OnTheFly.AirportServices.Repositories;

namespace OnTheFly.AirportServices.Services
{
    public class AirportService
    {
        private readonly IAirportRepository _airportRepository;

        public AirportService()
        {
            _airportRepository = new AirportRepository();
        }

        public List<Airport> GetAirports()
        {
            return _airportRepository.GetAirports();
        }

        public Airport GetAirportByIATA(string IATA)
        {
            return _airportRepository.GetAirportByIATA(IATA);
        }

        public Airport CreateAirport(Airport airport)
        {
            /*Airport airport = new()
            {
                IATA = "AQA",
                State = "SP",
                City = "Araraquara",
                Country = "Brasil"
            };*/

            return _airportRepository.CreateAirport(airport);
        }

        public Airport UpdateAirport(string IATA, Airport airport)
        {
            return _airportRepository.UpdateAirport(IATA, airport);
        }

        public ActionResult<Airport> DeleteAirport(string RAB)
        {
            return _airportRepository.DeleteAirport(RAB);
        }
    }
}

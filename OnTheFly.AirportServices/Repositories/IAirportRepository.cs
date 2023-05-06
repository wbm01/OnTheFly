using Microsoft.AspNetCore.Mvc;
using Models;

namespace OnTheFly.AirportServices.Repositories
{
    public interface IAirportRepository
    {
        List<Airport> GetAirports();
        Airport GetAirportByIATA(string IATA);
        Airport CreateAirport(Airport airport);
        Airport UpdateAirport(string IATA, Airport airport);
        ActionResult<Airport> DeleteAirport(string IATA);
    }
}

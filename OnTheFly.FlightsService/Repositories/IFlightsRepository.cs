using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Driver;
using OnTheFly.AirCraftServices.DTOs;

namespace OnTheFly.FlightsService.Repositories
{
    public interface IFlightsRepository
    {
        List<Flight> GetFlights();
        Flight GetFlight(string IATA, string RAB);
        Flight CreateFlight(Flight flight);
        Flight UpdateFlight(string IATA, string RAB, Flight flight);
        ActionResult<Flight> DeleteFlight(string IATA, string RAB);
    }
}

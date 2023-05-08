using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Driver;
using OnTheFly.FlightsService.DTOs;

namespace OnTheFly.FlightsService.Repositories
{
    public interface IFlightsRepository
    {
        List<Flight> GetFlights();
        Flight GetFlight(string IATA, string RAB, DateTime date);
        Flight CreateFlight(Flight flight);
        Flight UpdateFlight(string IATA, string RAB, DateTime date,UpdateFlightDTO flightDTO);
        ActionResult<Flight> DeleteFlight(string IATA, string RAB, DateTime date);
    }
}

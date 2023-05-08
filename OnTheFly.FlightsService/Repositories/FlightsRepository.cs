using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Driver;
using OnTheFly.FlightsService.config;
using OnTheFly.FlightsService.DTOs;

namespace OnTheFly.FlightsService.Repositories
{
    public class FlightsRepository : IFlightsRepository
    {
        private readonly IMongoCollection<Flight> _flightRepository;
        string connection = "mongodb://localhost:27017";

        public FlightsRepository(IMongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.DatabaseName);
            _flightRepository = database.GetCollection<Flight>(config.FlightCollectionName);
        }

        public Flight GetFlight(string IATA, string RAB, DateTime date)
        {
            var filter = CreateFilter(IATA, RAB, date);
            
            return _flightRepository.Find(filter).FirstOrDefault();
        }

        public List<Flight> GetFlights() => _flightRepository.Find(f => true).ToList();

        public Flight CreateFlight(Flight flight)
        {
            _flightRepository.InsertOne(flight);

            return flight;
        }

        public Flight UpdateFlight(string IATA, string RAB, DateTime date, UpdateFlightDTO flightDTO)
        {
            var filter = CreateFilter(IATA, RAB, date);
            Flight flight = _flightRepository.Find(filter).FirstOrDefault();

            flight.Status = flightDTO.Status;

            _flightRepository.ReplaceOne(filter, flight);

            return flight;
        }

        public ActionResult<Flight> DeleteFlight(string IATA, string RAB, DateTime date)
        {
            var filter = CreateFilter(IATA, RAB, date);

            _flightRepository.FindOneAndDelete(filter);

            return _flightRepository.FindOneAndDelete(filter);
        }

        private FilterDefinition<Flight> CreateFilter(string IATA, string RAB, DateTime date)
        {
            var builder = Builders<Flight>.Filter;

            var airPort = builder.Eq(f => f.Destiny.iata, IATA);
            var plane = builder.Eq(f => f.Plane.RAB, RAB);
            var departure = builder.Eq(f => f.Departure, date);

            var filter = builder.And(airPort, plane, departure);

            return filter;
        }
    }
}

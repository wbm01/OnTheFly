using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Driver;
using OnTheFly.FlightsService.config;

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

        public Flight GetFlight(string IATA, string RAB)
        {
            var builder = Builders<Flight>.Filter;

            var airPort = builder.Eq(f => f.Destiny.iata, IATA);
            var plane = builder.Eq(f => f.Plane.RAB, RAB);

            var filter = builder.And(airPort, plane);
            
            return _flightRepository.Find(filter).FirstOrDefault();
        }

        public List<Flight> GetFlights() => _flightRepository.Find(f => true).ToList();

        public Flight CreateFlight(Flight flight)
        {
            _flightRepository.InsertOne(flight);

            return flight;
        }

        public Flight UpdateFlight(string IATA, string RAB, Flight flight)
        {
            var filter = CreateFilter(IATA, RAB);
            Flight flight1 = _flightRepository.Find(filter).FirstOrDefault();

            flight1.Status = flight.Status;

            _flightRepository.ReplaceOne(filter, flight1);

            return flight1;
        }

        public ActionResult<Flight> DeleteFlight(string IATA, string RAB)
        {
            var filter = CreateFilter(IATA, RAB);

            _flightRepository.FindOneAndDelete(filter);

            return _flightRepository.FindOneAndDelete(filter);
        }

        private FilterDefinition<Flight> CreateFilter(string IATA, string RAB)
        {
            var builder = Builders<Flight>.Filter;

            var airPort = builder.Eq(f => f.Destiny.iata, IATA);
            var plane = builder.Eq(f => f.Plane.RAB, RAB);

            var filter = builder.And(airPort, plane);

            return filter;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Driver;

namespace OnTheFly.AirportServices.Repositories
{
    public class AirportRepository:IAirportRepository
    {
        private readonly IMongoCollection<Airport> _airportRepository;
        private readonly string connectionString = "mongodb://localhost:27017";
        private readonly string databaseName = "OnTheFlyAirport";
        private readonly string airportCollectionName = "Airport";

        public AirportRepository()
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _airportRepository = database.GetCollection<Airport>(airportCollectionName);
        }

        public List<Airport> GetAirports() => _airportRepository.Find(a => true).ToList();

        public Airport GetAirportByIATA(string IATA)
        {
            string IATAUp = IATA.ToUpper();

            return _airportRepository.Find(a => a.IATA == IATAUp).FirstOrDefault();
        }

        public Airport CreateAirport(Airport airport)
        {
            _airportRepository.InsertOne(airport);

            return airport;
        }

        public Airport UpdateAirport(string IATA, Airport airport)
        {
            string IATAUp = IATA.ToUpper();

            _airportRepository.ReplaceOne(a => a.IATA == IATAUp, airport);

            return airport;
        }

        public ActionResult<Airport> DeleteAirport(string IATA)
        {
            string IATAUp = IATA.ToUpper();

            return _airportRepository.FindOneAndDelete(a => a.IATA == IATAUp);
        }
    }
}

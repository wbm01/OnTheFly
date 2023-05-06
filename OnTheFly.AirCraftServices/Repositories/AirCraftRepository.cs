using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Driver;
using OnTheFly.AirCraftServices.config;

namespace OnTheFly.AirCraftServices.Repositories
{
    public class AirCraftRepository : IAirCraftRepository
    {
        private readonly IMongoCollection<AirCraft> _aircraftRepository;
        private readonly string connectionString = "mongodb://localhost:27017";
        private readonly string databaseName = "OnTheFlyAirCraft";
        private readonly string airCraftCollectionName = "AirCraft";

        public AirCraftRepository()
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _aircraftRepository = database.GetCollection<AirCraft>(airCraftCollectionName);
        }

        public List<AirCraft> GetAirCrafts() => _aircraftRepository.Find(a => true).ToList();

        public AirCraft GetAirCraftByRAB(string RAB)
        {
            string RABUp = RAB.ToUpper();

            return _aircraftRepository.Find(a => a.RAB == RABUp).FirstOrDefault();
        }

        public AirCraft CreateAirCraft(AirCraft aircraft)
        {
            _aircraftRepository.InsertOne(aircraft);

            return aircraft;
        }

        public AirCraft UpdateAirCraft(string RAB, AirCraft aircraft)
        {
            string RABUp = RAB.ToUpper();

            _aircraftRepository.ReplaceOne(a => a.RAB == RABUp, aircraft);

            return aircraft;
        }

        public ActionResult<AirCraft> DeleteAirCraft(string RAB)
        {
            string RABUp = RAB.ToUpper();

            return _aircraftRepository.FindOneAndDelete(a => a.RAB == RABUp);
        }
    }
}

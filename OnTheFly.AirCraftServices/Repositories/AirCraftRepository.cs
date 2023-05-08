using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Driver;
using OnTheFly.AirCraftServices.config;

namespace OnTheFly.AirCraftServices.Repositories
{
    public class AirCraftRepository : IAirCraftRepository
    {
        private readonly IMongoCollection<AirCraft> _aircraftRepository;

        public AirCraftRepository(IMongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.DatabaseName);
            _aircraftRepository = database.GetCollection<AirCraft>(config.AirCraftCollectionName);
        }

        public List<AirCraft> GetAirCrafts() => _aircraftRepository.Find(a => true).ToList();

        public List<AirCraft> GetAirCraftsByCompany(string cnpj) => _aircraftRepository.Find(a => a.Company.CNPJ == cnpj).ToList();

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

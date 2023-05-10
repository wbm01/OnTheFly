using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using MongoDB.Driver;

namespace OnTheFly.PassengerServices.Repositories
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly IMongoCollection<Passenger> _passengerRepository;
        private readonly IMongoCollection<Passenger> _passengerRepositoryRestrit;

        private readonly string _connectionString = "mongodb://localhost:27017";
        private readonly string _dataBaseName = "DBPassenger";
        private readonly string _passengerCollectionName = "Passenger";
        private readonly string _passengerCollectionRestrictName = "PassengerRestrict";

        public PassengerRepository()
        {
            var passenger = new MongoClient(_connectionString);// estabeleci a conexão com o banco
            var database = passenger.GetDatabase(_dataBaseName);// definição do nome do banco
            _passengerRepository = database.GetCollection<Passenger>(_passengerCollectionName);// coneção a collection
            _passengerRepositoryRestrit = database.GetCollection<Passenger>(_passengerCollectionRestritName);
        }
        public ActionResult<Passenger> DeletePassenger(string CPF) => _passengerRepository.FindOneAndDelete(p => p.CPF == CPF);
        /*
        public ActionResult<Passenger> DeletePassenger(string CPF)
        {
            return _pasengerRepository.FindOneAndDelete(p => p.CPF == CPF);
        }
        */
        public List<Passenger> GetPassenger() => _passengerRepository.Find(p => true).ToList();

        public Passenger GetPassengerByCPF(string CPF) => _passengerRepository.Find(p => p.CPF == CPF).FirstOrDefault();

        public List<Passenger> GetRestritPassenger() => _passengerRepositoryRestrit.Find(c => true).ToList();

        public Passenger PostPassenger(Passenger passenger)
        {
            _passengerRepository.InsertOne(passenger);
            return passenger;
        }

        public Passenger UpdatePassenger(Passenger passenger, string CPF)
        {
            _passengerRepository.ReplaceOne(p => p.CPF == CPF, passenger);
            return passenger;   
        }
        public Passenger RestritPassenger(string CPF)
        {
            var consult = GetPassengerByCPF(CPF);
            _passengerRepositoryRestrit.InsertOne(consult);
            _passengerRepository.DeleteOne(c => c.CPF == CPF);
            return consult;
        }
        public Passenger NoRestritPassenger(string CPF)
        {
            var consult = _passengerRepositoryRestrit.Find(p => p.CPF == CPF).FirstOrDefault();
            _passengerRepository.InsertOne(consult);
            _passengerRepositoryRestrit.DeleteOne(c => c.CPF == CPF);
            return consult;
        }

        public Passenger GetRestritPassengerByCPF(string CPF) => _pasengerRepositoryRestrict.Find(c => c.CPF == CPF).FirstOrDefault();
    }
}

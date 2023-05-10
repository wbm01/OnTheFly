using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using MongoDB.Driver;

namespace OnTheFly.PassengerServices.Repositories
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly IMongoCollection<Passenger> _pasengerRepository;
        private readonly IMongoCollection<Passenger> _pasengerRepositoryRestrict;

        private readonly string _connectionString = "mongodb://localhost:27017";
        private readonly string _dataBaseName = "DBPassenger";
        private readonly string _passengerCollectionName = "Passenger";
        private readonly string _passengerCollectionRestrictName = "PassengerRestrict";

        public PassengerRepository()
        {
            var passenger = new MongoClient(_connectionString);// estabeleci a conexão com o banco
            var database = passenger.GetDatabase(_dataBaseName);// definição do nome do banco
            _pasengerRepository = database.GetCollection<Passenger>(_passengerCollectionName);// coneção a collection
            _pasengerRepositoryRestrict = database.GetCollection<Passenger>(_passengerCollectionRestrictName);
        }
        public ActionResult<Passenger> DeletePassenger(string CPF) => _pasengerRepository.FindOneAndDelete(p => p.CPF == CPF);
        /*
        public ActionResult<Passenger> DeletePassenger(string CPF)
        {
            return _pasengerRepository.FindOneAndDelete(p => p.CPF == CPF);
        }
        */
        public List<Passenger> GetPassenger() => _pasengerRepository.Find(p => true).ToList();

        public Passenger GetPassengerByCPF(string CPF) => _pasengerRepository.Find(p => p.CPF == CPF).FirstOrDefault();

        public Passenger GetRestritPassengerByCPF(string CPF) => _pasengerRepositoryRestrit.Find(c => c.CPF == CPF).FirstOrDefault();

        public Passenger PostPassenger(Passenger passenger)
        {
            _pasengerRepository.InsertOne(passenger);
            return passenger;
        }

        public Passenger UpdatePassenger(Passenger passenger, string CPF)
        {
            _pasengerRepository.ReplaceOne(p => p.CPF == CPF, passenger);
            return passenger;   
        }
        public Passenger RestritPassenger(string CPF)
        {
            var consult = GetPassengerByCPF(CPF);
            _pasengerRepositoryRestrict.InsertOne(consult);
            _pasengerRepository.DeleteOne(c => c.CPF == CPF);
            return consult;
        }
        public Passenger NoRestritPassenger(string CPF)
        {
            var consult = _pasengerRepositoryRestrict.Find(p => p.CPF == CPF).FirstOrDefault();
            _pasengerRepository.InsertOne(consult);
            _pasengerRepositoryRestrict.DeleteOne(c => c.CPF == CPF);
            return consult;
        }

        public Passenger GetRestritPassengerByCPF(string CPF) => _pasengerRepositoryRestrict.Find(c => c.CPF == CPF).FirstOrDefault();
    }
}

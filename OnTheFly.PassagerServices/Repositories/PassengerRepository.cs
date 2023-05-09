using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using MongoDB.Driver;

namespace OnTheFly.PassengerServices.Repositories
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly IMongoCollection<Passenger> _pasengerRepository;
        private readonly IMongoCollection<Passenger> _pasengerRepositoryRestrit;

        private readonly string _connectionString = "mongodb://localhost:27017";
        private readonly string _dataBaseName = "DBPassenger";
        private readonly string _passengerCollectionName = "Passenger";
        private readonly string _passengerCollectionRestritName = "PassengerRestrit";

        public PassengerRepository()
        {
            var passenger = new MongoClient(_connectionString);// estabeleci a conexão com o banco
            var database = passenger.GetDatabase(_dataBaseName);// definição do nome do banco
            _pasengerRepository = database.GetCollection<Passenger>(_passengerCollectionName);// coneção a collection
            _pasengerRepositoryRestrit = database.GetCollection<Passenger>(_passengerCollectionRestritName);
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
            _pasengerRepositoryRestrit.InsertOne(consult);
            _pasengerRepository.DeleteOne(c => c.CPF == CPF);
            return consult;
        }
        public Passenger NoRestritPassenger(string CPF)
        {
            var consult = _pasengerRepositoryRestrit.Find(p => p.CPF == CPF).FirstOrDefault();
            _pasengerRepository.InsertOne(consult);
            _pasengerRepositoryRestrit.DeleteOne(c => c.CPF == CPF);
            return consult;
        }
    }
}

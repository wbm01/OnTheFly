using Microsoft.AspNetCore.Mvc;
using Models;
using OnTheFly.PassengerServices.Repositories;

namespace OnTheFly.PassengerServices.Services
{
    public class PassengerService
    {
        private readonly IPassengerRepository _passengerRepository;
        public PassengerService()
        {
            _passengerRepository = new PassengerRepository();
        }
        public ActionResult<Passenger> DeletePassenger(string CPF)
        {
            return _passengerRepository.DeletePassenger(CPF);
        }
        public List<Passenger> GetPassenger()
        {
            return _passengerRepository.GetPassenger();
        }
        public Passenger GetPassengerByCPF(string CPF)
        {
            return _passengerRepository.GetPassengerByCPF(CPF);
        }
        public Passenger PostPassenger(Passenger passenger)
        {
            return _passengerRepository.PostPassenger(passenger);
        }
        public Passenger UpdatePassenger(Passenger passenger, string CPF)
        {
            return _passengerRepository.UpdatePassenger(passenger, CPF);    
        }

    }
}

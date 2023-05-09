using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace OnTheFly.PassengerServices.Repositories
{
    public interface IPassengerRepository
    {
        List<Passenger> GetPassenger();
        Passenger GetPassengerByCPF(string CPF);
        Passenger PostPassenger(Passenger passenger);

        Passenger UpdatePassenger(Passenger passenger, string CPF);

        ActionResult<Passenger> DeletePassenger(string CPF);

        Passenger NoRestritPassenger(string CPF);

        Passenger RestritPassenger(string CPF);

        List<Passenger> GetRestritPassenger();

        Passenger GetRestritPassengerByCPF(string CPF);
    }
}

using Microsoft.AspNetCore.Mvc;
using Models;

namespace OnTheFly.PassengerServices.Repositories
{
    public interface IPassengerRepository
    {
        List<Passenger> GetPassenger();
        Passenger GetPassengerByCPF(string CPF);
        Passenger PostPassenger(Passenger passenger);

        Passenger UpdatePassenger(Passenger passenger, string CPF);

        ActionResult<Passenger> DeletePassenger(string CPF);
    }
}

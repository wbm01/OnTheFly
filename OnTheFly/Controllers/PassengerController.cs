using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using OnTheFly.Services;

namespace OnTheFly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly PassengerService _passenger;

        public PassengerController()
        {
            _passenger = new PassengerService();
        }

        // GET: api/AddressModels
        [HttpGet(Name = "GetPassengers")]
        public async Task<List<Passenger>> GetPassenger()
        {
            return await _passenger.GetPassenger();
        }

        [HttpGet("{CPF}", Name = "GetPassenger")]
        public async Task<Passenger> GetPassengerCPF(string CPF)
        {
            return await _passenger.GetPassengerByCPF(CPF);
        }

        [HttpGet("restricts", Name = "GetRestritPassenger")]
        public async Task<List<Passenger>> GetRestrictPassenger()
        {
            return await _passenger.GetRestritPassenger();
        }

        [HttpDelete("{CPF}", Name = "DeletePassenger")]
        public async void DeletePassengerCPF(string CPF)
        {
            await _passenger.DeletePassenger(CPF);
        }

        [HttpPost(Name = "PostName")]
        public async Task<Passenger> PostPassenger(CreatePassengerDTO passenger)
        {
            return await _passenger.CreatePassenger(passenger);
        }

        [HttpPut("{CPF}", Name = "PutName")]
        public async void PutPassenger(string CPF, Passenger passenger)
        {
            await _passenger.UpdatePassenger(CPF, passenger);
        }

        /*[HttpPatch("restrict/{CPF}", Name = "UpdateStatus")]
        public async void UpdateStatus(string CPF, Passenger passenger)
        {
            await _passenger.UpdateStatus(CPF, passenger);
        }*/

        [HttpPatch("{CPF}", Name = "UpdateStatusPassanger")]
        public async void UpdateStatus(string CPF, Passenger passenger)
        {
            await _passenger.UpdateStatus(CPF, passenger);
        }

        [HttpGet("restrict/{cpf}", Name = "GetRestritPassengerByCPF")]
        public async Task<Passenger> GetRestrictPassengerByCPF(string cpf)
        {
            return await _passenger.GetRestritPassengerByCPF(cpf);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
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

        [HttpGet("{CPF}",Name = "GetPassenger")]
        public async Task<Passenger> GetPassengerCPF(string CPF)
        {
            return await _passenger.GetPassengerByCPF(CPF);
        }

        [HttpGet(Name = "Get Restrit Passenger")]
        public async Task<List<Passenger>> GetRestritPassenger()
        {
            return await _passenger.GetRestritPassenger();
        }

        [HttpDelete("{CPF}",Name = "DeletePassenger")]
        public async void DeletePassengerCPF(string CPF)
        {
            _passenger.DeletePassenger(CPF);
        }

        [HttpPost(Name = "PostName")]
        public async void PostPassenger(Passenger passenger)
        {
            _passenger.CreatePassenger(passenger);
        }

        [HttpPut("{CPF}",Name = "PutName")]
        public async void PutPassenger(string CPF, Passenger passenger)
        {
            _passenger.UpdatePassenger(CPF,passenger);
        }

        [HttpPost("{CPF}", Name = "UpdateStatus")]
        public async void UpdateStatus(string CPF, Passenger passenger)
        {
            _passenger.UpdateStatus(CPF, passenger);
        }

        [HttpGet("Get Restrit Passenger By CPF")]
        public async Task<Passenger> GetRestritPassengerByCPF(string CPF)
        {
            return await _passenger.GetRestritPassengerByCPF(CPF);
        }
    }
}

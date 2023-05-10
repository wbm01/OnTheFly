using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using OnTheFly.PassengerServices.Services;

namespace OnTheFly.PassengerServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private readonly PassengerService _passengerService;

        public PassengersController()
        {
            _passengerService = new PassengerService();
        }

        [HttpGet]
        public ActionResult<List<Passenger>> GetPassenger()
        {
            return _passengerService.GetPassenger();
        }

        [HttpGet("{CPF}", Name = "GetPassenger")]
        public ActionResult<Passenger> GetPassengerByCPF(string CPF)
        {
            return _passengerService.GetPassengerByCPF(CPF);
        }

        [HttpGet("GetRestritPassenger")]
        public ActionResult<List<Passenger>> GetRestritPassenger() => _passengerService.GetRestritPassenger();

        [HttpPost(Name = "PostPassenger")]
        public ActionResult<Passenger> PostPassenger(CreatePassengerDTO passenger)
        {
            return _passengerService.PostPassenger(passenger);
        }

        [HttpPatch("{CPF}", Name = "UpdateStatusPassanger")]
        public ActionResult<Passenger> UpdateStatus(string CPF)
        {
            return _passengerService.UpdateStatus(CPF);
        }

        [HttpPut]
        public ActionResult<Passenger> UpdatePassenger(string CPF, UpdatePassengerDTO passenger)
        {
            return _passengerService.UpdatePassenger(passenger, CPF);
        }

        [HttpDelete("{CPF}")]
        public ActionResult<Passenger> DeletePassenger(string CPF)
        { 
            return _passengerService.DeletePassenger(CPF);
        }

        [HttpGet("restrict/{cpf}", Name = "GetRestritPassengerByCPF")]
        public ActionResult<Passenger> GetRestritPassengerByCPF(string CPF) => _passengerService.GetRestritPassengerByCPF(CPF);
    }
}

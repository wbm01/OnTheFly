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

        [HttpGet(Name = "GetAllPassengers")]
        public ActionResult<List<Passenger>> GetPassenger()
        {
            return _passengerService.GetPassenger();
        }
        //[HttpGet("{CPF}", Name = "GetPassengerByCPF")]
        //public ActionResult<Passenger> GetPassengerByCPF(string CPF)
        //{
        //    return _passengerService.GetPassengerByCPF(CPF);
        //}

        [HttpGet("{CPF}", Name = "GetRestrictPassenger")]
        public ActionResult<Passenger> GetRestritPassengerByCPF(string CPF, bool restrict)
        {
            if(restrict) return _passengerService.GetRestritPassengerByCPF(CPF);
            return _passengerService.GetPassengerByCPF(CPF);
        }

        [HttpPost(Name = "PostPassenger")]
        public ActionResult<Passenger> PostPassenger(CreatePassengerDTO passenger)
        {
            return _passengerService.PostPassenger(passenger);
        }
        [HttpPost("{CPF}", Name = "RestritPassenger")]
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

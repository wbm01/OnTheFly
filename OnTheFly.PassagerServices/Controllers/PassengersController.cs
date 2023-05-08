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
        [HttpGet("{CPF}")]
        public ActionResult<Passenger> GetPassengerByCPF(string CPF)
        {
            return _passengerService.GetPassengerByCPF(CPF);
        }
        [HttpPost]
        public ActionResult<Passenger> PostPassenger(CreatePassengerDTO passenger)
        {
            return _passengerService.PostPassenger(passenger);
        }
        [HttpPut]
        public ActionResult<Passenger> UpdatePassenger(Passenger passenger, string CPF)
        {
            return _passengerService.UpdatePassenger(passenger, CPF);
        }
        [HttpDelete("{CPF}")]
        public ActionResult<Passenger> DeletePassenger(string CPF)
        { 
            return _passengerService.DeletePassenger(CPF);
        }
    }
}

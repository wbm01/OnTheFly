using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using OnTheFly.AirCraftServices.config;
using OnTheFly.AirCraftServices.Services;
using OnTheFly.FlightsService.DTOs;

namespace OnTheFly.AirCraftServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirCraftController : ControllerBase
    {
        private readonly AirCraftService _airCraftService;

        public AirCraftController(AirCraftService service)
        {
            _airCraftService = service;
        }

        [HttpGet(Name = "GetAirCrafts")]
        public List<AirCraft> GetAirCrafts()
        {
            return _airCraftService.GetAirCrafts();
        }

        [HttpGet("{RAB:length(6)}", Name = "GetAirCraftByRAB")]
        public ActionResult<AirCraft> GetAirCraftByRAB(string RAB)
        {
            return _airCraftService.GetAirCraftByRAB(RAB);
        }

        [HttpPost(Name = "CreateAirCraft")]
        public Task<ActionResult<AirCraft>> CreateAirCraft(CreateAirCraftDTO airCraftDTO)
        {
            return _airCraftService.CreateAirCraft(airCraftDTO);
        }

        [HttpPut("{RAB:length(6)}", Name = "UpdateAirCraft")]
        public ActionResult<AirCraft> UpdateAirCraft(string RAB, UpdateAirCraftDTO airCraftDTO)
        {
            return _airCraftService.UpdateAirCraft(RAB, airCraftDTO);
        }

        [HttpDelete("{RAB:length(6)}", Name = "DeleteAirCraft")]
        public ActionResult DeleteAirCraft(string RAB)
        {
            _airCraftService.Delete(RAB);

            return NoContent();
        }
    }
}

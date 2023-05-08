using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using OnTheFly.AirCraftServices.config;
using OnTheFly.AirCraftServices.Services;

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

        [HttpGet(Name = "Get AirCrafts")]
        public List<AirCraft> GetAirCrafts()
        {
            return _airCraftService.GetAirCrafts();
        }

        [HttpGet("{RAB}", Name = "Get AirCraft By RAB")]
        public ActionResult<AirCraft> GetAirCraftByRAB(string RAB)
        {
            return _airCraftService.GetAirCraftByRAB(RAB);
        }

        [HttpPost(Name = "Create AirCraft")]
        public ActionResult<AirCraft> CreateAirCraft(AirCraft airCraft)
        {
            return _airCraftService.CreateAirCraft(airCraft);
        }

        [HttpPut("{RAB}", Name = "Update AirCraft")]
        public ActionResult<AirCraft> UpdateAirCraft(string RAB, AirCraft airCraft)
        {
            return _airCraftService.UpdateAirCraft(RAB, airCraft);
        }

        [HttpDelete("{RAB}", Name = "Delete AirCraft")]
        public ActionResult DeleteAirCraft(string RAB)
        {
            _airCraftService.Delete(RAB);

            return NoContent();
        }
    }
}

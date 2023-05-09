using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using OnTheFly.Services;

namespace OnTheFly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirCraftController : ControllerBase
    {
        private readonly AirCraftService _airCraftService;

        public AirCraftController()
        {
            _airCraftService = new();
        }

        [HttpGet(Name = "Get AirCrafts")]
        public Task<List<AirCraft>> GetAirCrafts()
        {
            return _airCraftService.GetAirCrafts();
        }

        [HttpGet("{RAB}", Name = "Get AirCraft By RAB")]
        public Task<ActionResult<AirCraft>> GetAirCraftByRAB(string RAB)
        {
            return _airCraftService.GetAirCraftByRAB(RAB);
        }

        [HttpPost(Name = "Create AirCraft")]
        public Task<ActionResult<AirCraft>> CreateAirCraft(CreateAirCraftDTO airCraftDTO)
        {
            return _airCraftService.CreateAirCraft(airCraftDTO);
        }

        [HttpPut("{RAB}", Name = "Update AirCraft")]
        public Task<ActionResult<AirCraft>> UpdateAirCraft(string RAB, [FromBody]string DtLastFlight)
        {
            return _airCraftService.UpdateAirCraft(RAB, DtLastFlight);
        }

        [HttpDelete("{RAB}", Name = "Delete AirCraft")]
        public Task<HttpStatusCode> DeleteAirCraft(string RAB)
        {           
            return _airCraftService.DeleteAirCraft(RAB);
        }
    }
}

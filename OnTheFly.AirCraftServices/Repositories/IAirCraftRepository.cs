using Microsoft.AspNetCore.Mvc;
using Models;

namespace OnTheFly.AirCraftServices.Repositories
{
    public interface IAirCraftRepository
    {
        List<AirCraft> GetAirCrafts();
        AirCraft GetAirCraftByRAB(string RAB);
        AirCraft CreateAirCraft(AirCraft aircraft);
        AirCraft UpdateAirCraft(string RAB, AirCraft aircraft);
        ActionResult<AirCraft> DeleteAirCraft(string RAB);
    }
}

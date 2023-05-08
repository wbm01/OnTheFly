using Microsoft.AspNetCore.Mvc;
using Models;
using OnTheFly.AirCraftServices.config;
using OnTheFly.AirCraftServices.Repositories;

namespace OnTheFly.AirCraftServices.Services
{
    public class AirCraftService
    {
        private readonly IAirCraftRepository _airCraftRepository;

        public AirCraftService(AirCraftRepository repository)
        {
            _airCraftRepository = repository;
        }

        public List<AirCraft> GetAirCrafts()
        {
            return _airCraftRepository.GetAirCrafts();
        }

        public AirCraft GetAirCraftByRAB(string RAB)
        {
            return _airCraftRepository.GetAirCraftByRAB(RAB);
        }

        public AirCraft CreateAirCraft(AirCraft airCraft)
        {
            /*AirCraft airCraft1 = new()
            {
                RAB = airCraft.RAB,
                Capacity = airCraft.Capacity,
                DtRegistry = DateTime.Now,                
                Company = new Company()
                {
                    CNPJ = "1234567891234567891",
                    Name = "Shalom AirLines",
                    NameOpt = "SAL",
                    DtOpen = DateTime.Now,
                    Status = true,
                    Address = new Address()
                    {
                        ZipCode = "14840000",
                        Street = "Rua Jornalista",
                        Number = 183,
                        Complement = "Casa",
                        City = "Guariba",
                        State = "SP",
                    }
                }
            };*/

            return _airCraftRepository.CreateAirCraft(airCraft);
        }

        public AirCraft UpdateAirCraft(string RAB, AirCraft airCraft)
        {
            return _airCraftRepository.UpdateAirCraft(RAB, airCraft);
        }

        public ActionResult<AirCraft> Delete(string RAB)
        {
            return _airCraftRepository.DeleteAirCraft(RAB);
        }
    }
}

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

        public ActionResult<AirCraft> GetAirCraftByRAB(string RAB)
        {
            if (ValidateRAB(RAB))
            {
                return _airCraftRepository.GetAirCraftByRAB(RAB);
            }

            return new BadRequestResult();
        }

        public ActionResult<AirCraft> CreateAirCraft(AirCraft airCraft)
        {
            if (!ValidateRAB(airCraft.RAB))
            {
                return new BadRequestResult();
            }

            var countAirCraftCompany = _airCraftRepository.GetAirCraftsByCompany(airCraft.Company.CNPJ).Count;

            /*
            if (countAirCraftCompany == 0)
            {
                //Alterar o status????
            }
            */

            Company comp = new()
            {
                CNPJ = "1234567891234567891",
                Name = "Shalom AirLines",
                NameOpt = "SAL",
                DtOpen = DateTime.Now,
                Status = false,
                Address = new Address()
                {
                    ZipCode = "14840000",
                    Street = "Rua Jornalista",
                    Number = 183,
                    Complement = "Casa",
                    City = "Guariba",
                    State = "SP",
                }
            };

            AirCraft airCraft1 = new()
            {
                RAB = airCraft.RAB,
                Capacity = airCraft.Capacity,
                DtRegistry = DateTime.Now,
                Company = comp,
            };

            return _airCraftRepository.CreateAirCraft(airCraft);
        }

        public ActionResult<AirCraft> UpdateAirCraft(string RAB, AirCraft airCraft)
        {
            if (ValidateRAB(RAB))
            {
                return _airCraftRepository.UpdateAirCraft(RAB, airCraft);
            }

            return new BadRequestResult();
        }

        public ActionResult<AirCraft> Delete(string RAB)
        {
            if (ValidateRAB(RAB))
            {
                return _airCraftRepository.DeleteAirCraft(RAB);
            }

            return new BadRequestResult();            
        }

        private static bool ValidateRAB(string rab)
        {
            rab = rab.ToUpper();
            if (String.IsNullOrWhiteSpace(rab)) return false;

            if (rab[2] != '-') return false;

            string[] vetRab = rab.Split("-");
            string rab1 = $"{vetRab[0]}{rab[2]}";

            if (rab1 != "PT-" && rab1 != "PR-") return false;

            return true;
        }
    }
}

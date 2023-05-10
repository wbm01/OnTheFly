using System.Globalization;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using OnTheFly.AirCraftServices.config;
using OnTheFly.AirCraftServices.Repositories;

namespace OnTheFly.AirCraftServices.Services
{
    public class AirCraftService
    {
        private readonly IAirCraftRepository _airCraftRepository;
        private readonly HttpClient _airCraftClient;
        private readonly string _companyHost;

        public AirCraftService(AirCraftRepository repository)
        {
            _airCraftClient = new();
            _airCraftRepository = repository;
            _companyHost = "https://localhost:5001/api/Companies/";
        }

        public List<AirCraft> GetAirCrafts()
        {
            return _airCraftRepository.GetAirCrafts();
        }

        public ActionResult<AirCraft> GetAirCraftByRAB(string RAB)
        {
            if (ValidateRAB(RAB))
            {
                AirCraft airCraft = _airCraftRepository.GetAirCraftByRAB(RAB);

                if (airCraft == null)
                {
                    return new BadRequestObjectResult("Aeronave não encontrada!");
                }

                return airCraft;
            }

            return new BadRequestObjectResult("RAB inválido!");
        }

        public async Task<ActionResult<AirCraft>> CreateAirCraft(CreateAirCraftDTO airCraftDTO)
        {
            HttpResponseMessage response = await _airCraftClient.GetAsync(_companyHost + airCraftDTO.cnpj);
            response.EnsureSuccessStatusCode();

            string companyResponse = await response.Content.ReadAsStringAsync();
            Company company = JsonConvert.DeserializeObject<Company>(companyResponse);

            //Lista de companhias restritas
            HttpResponseMessage responseCompany = await _airCraftClient.GetAsync($"{_companyHost}GetRestritCompany");
            responseCompany.EnsureSuccessStatusCode();

            string companyResponseRestric = await responseCompany.Content.ReadAsStringAsync();
            List<Company> companyList = JsonConvert.DeserializeObject<List<Company>>(companyResponseRestric);

            Company companyRestric = new();

            foreach (var comp in companyList)
            {
                if (comp.CNPJ == airCraftDTO.cnpj)
                {
                    companyRestric = comp;
                }
            }

            if (company == null)
            {
                company = companyRestric;
            }

            if (company == null)
            {
                return new NotFoundObjectResult("Companhia não encontrada!");
            }

            if (!ValidateRAB(airCraftDTO.rab))
            {
                return new BadRequestObjectResult("RAB inválido!");
            }

            

            if ((bool)(company.Status == true))
            {
                return new UnauthorizedObjectResult("Companhia inativa!");
            }

            AirCraft airCraft = new()
            {
                RAB = airCraftDTO.rab.ToUpper(),
                Capacity = airCraftDTO.Capacity,
                DtRegistry = DateTime.Now,
                Company = company,
            };

            return _airCraftRepository.CreateAirCraft(airCraft);
        }

        public ActionResult<AirCraft> UpdateAirCraft(string RAB, UpdateAirCraftDTO airCraftDTO)
        {
            if (ValidateRAB(RAB))
            {
                DateTime DtLast = ParseDate(airCraftDTO.DtLastFlight);

                AirCraft airCraft = _airCraftRepository.GetAirCraftByRAB(RAB);

                if (airCraft == null)
                {
                    return new NotFoundObjectResult("Aeronave não encontrada!");
                }

                airCraft.DtLastFlight = DtLast;

                return _airCraftRepository.UpdateAirCraft(RAB, airCraft);
            }

            return new BadRequestObjectResult("RAB inválido!");
        }

        public ActionResult<AirCraft> Delete(string RAB)
        {
            if (ValidateRAB(RAB))
            {
                AirCraft airCraft = _airCraftRepository.GetAirCraftByRAB(RAB);

                if (airCraft == null)
                {
                    return new NotFoundObjectResult("Aeronave não encontrada!");
                }

                int airCraftsCount = _airCraftRepository.GetAirCraftsByCompany(airCraft.Company.CNPJ).Count;

                if (airCraftsCount == 1)
                {
                    return new StatusCodeResult(401);
                }

                return _airCraftRepository.DeleteAirCraft(RAB);
            }

            return new BadRequestObjectResult("RAB inválido!");
        }

        private static DateTime ParseDate(string date)
        {
            var dateTimeB = date;
            var format = "dd/MM/yyyy HH:mm";
            return DateTime.ParseExact(dateTimeB, format, CultureInfo.InvariantCulture);
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

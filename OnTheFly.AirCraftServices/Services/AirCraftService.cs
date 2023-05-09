using System.Globalization;
using Microsoft.AspNetCore.Mvc;
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
            _companyHost = "https://localhost:7226/api/Companies/";
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

                if(airCraft == null)
                {
                    return new BadRequestResult();
                }

                return airCraft;
            }

            return new BadRequestResult();
        }

        public async Task<ActionResult<AirCraft>> CreateAirCraft(CreateAirCraftDTO airCraftDTO)
        {
            HttpResponseMessage response = await _airCraftClient.GetAsync(_companyHost + airCraftDTO.cnpj);
            response.EnsureSuccessStatusCode();

            string companyResponse = await response.Content.ReadAsStringAsync();
            Company company = JsonConvert.DeserializeObject<Company>(companyResponse);

            if (company == null)
            {
                return new NotFoundResult();
            }

            if (!ValidateRAB(airCraftDTO.rab))
            {
                return new BadRequestResult();
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

        public ActionResult<AirCraft> UpdateAirCraft(string RAB, string DtLastFlight)
        {
            if (ValidateRAB(RAB))
            {
                DateTime DtLast = ParseDate(DtLastFlight);

                AirCraft airCraft = _airCraftRepository.GetAirCraftByRAB(RAB);

                if(airCraft == null)
                {
                    return new BadRequestResult();
                }

                airCraft.DtLastFlight = DtLast;

                return _airCraftRepository.UpdateAirCraft(RAB, airCraft);
            }

            return new BadRequestResult();
        }

        public ActionResult<AirCraft> Delete(string RAB)
        {
            if (ValidateRAB(RAB))
            {
                AirCraft airCraft = _airCraftRepository.GetAirCraftByRAB(RAB);

                if(airCraft == null)
                {
                    return new NotFoundResult();
                }

                int airCraftsCount = _airCraftRepository.GetAirCraftsByCompany(airCraft.Company.CNPJ).Count;

                if(airCraftsCount == 1)
                {
                    return new StatusCodeResult(401);
                }

                return _airCraftRepository.DeleteAirCraft(RAB);
            }

            return new BadRequestResult();
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

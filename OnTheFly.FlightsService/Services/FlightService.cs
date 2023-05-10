using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using OnTheFly.FlightsService.DTOs;
using OnTheFly.FlightsService.Repositories;

namespace OnTheFly.FlightsService.Services
{
    public class FlightService
    {
        private readonly IFlightsRepository _flightsRepository;
        private readonly HttpClient _flightstClient;
        private readonly string _airCraftHost;
        private readonly string _airportHost;
        private readonly string _companyHost;

        public FlightService(FlightsRepository repository)
        {
            _flightsRepository = repository;
            _airCraftHost = "https://localhost:5002/api/AirCraft/";
            _airportHost = "https://localhost:5003/api/Airport/";
            _companyHost = "https://localhost:5001/api/Companies/GetRestritCompany";
            _flightstClient = new();
        }

        public List<Flight> GetFlights()
        {
            List<Flight> response = _flightsRepository.GetFlights();
            List<Flight> flights = new();

            foreach (var flight in response)
            {
                string date = $"{flight.Departure.Day}/{flight.Departure.Month}/{flight.Departure.Year} {flight.Departure.Hour - 3}:{flight.Departure.Minute}:{flight.Departure.Second}";
                DateTime dateTime = DateTime.Parse(date);
                flight.Departure = dateTime;

                flights.Add(flight);
            }

            return flights;
        }

        public ActionResult<Flight> GetFlight(string IATA, string RAB, string departure)
        {
            DateTime parseDateTime = ParseDate(departure);

            if (ValidateIATA(IATA) && ValidateRAB(RAB))
            {
                var flight = _flightsRepository.GetFlight(IATA.ToUpper(), RAB.ToUpper(), parseDateTime);

                if(flight == null)
                {
                    return new NotFoundObjectResult("Voo não encontrado!");
                }

                string date = $"{flight.Departure.Day}/{flight.Departure.Month}/{flight.Departure.Year} {flight.Departure.Hour - 3}:{flight.Departure.Minute}:{flight.Departure.Second}";
                DateTime dateTime = DateTime.Parse(date);
                flight.Departure = dateTime;

                return flight;
            }

            else return new BadRequestObjectResult("RAB ou IATA inválido!");
        }

        public async Task<ActionResult<Flight>> CreateFlight(CreateFlightDTO flightDTO)
        {
            if (!ValidateRAB(flightDTO.RAB) || !ValidateIATA(flightDTO.IATA))
            {
                return new BadRequestObjectResult("RAB ou IATA inválidos");
            }

            AirCraft plane = new();
            // Get AirCraft
            try
            {
                HttpResponseMessage airCraftResponse = await _flightstClient.GetAsync(_airCraftHost + flightDTO.RAB.ToUpper());
                //airCraftResponse.EnsureSuccessStatusCode();

                string airCraftStr = await airCraftResponse.Content.ReadAsStringAsync();
                plane = JsonConvert.DeserializeObject<AirCraft>(airCraftStr);
            }
            catch
            {
                return new BadRequestObjectResult("Aeronave não encontrada!");
            }

            Airport airport = new();
            // Get Airport
            try
            {
                HttpResponseMessage airportResponse = await _flightstClient.GetAsync(_airportHost + flightDTO.IATA.ToUpper());
                airportResponse.EnsureSuccessStatusCode();

                string airportStr = await airportResponse.Content.ReadAsStringAsync();
                airport = JsonConvert.DeserializeObject<Airport>(airportStr);
            }
            catch
            {
                return new BadRequestObjectResult("Aeroporto não encontrado!");
            }

            HttpResponseMessage companyResponse = await _flightstClient.GetAsync(_companyHost);
            companyResponse.EnsureSuccessStatusCode();

            string companyStr = await companyResponse.Content.ReadAsStringAsync();
            List<Company> company = JsonConvert.DeserializeObject<List<Company>>(companyStr);

            bool companyRestric = false;
            
            foreach (var comp in company)
            {
                if(comp.CNPJ == plane.Company.CNPJ)
                {
                    companyRestric = true;
                }
            }
            
            if(companyRestric == true)
            {
                return new UnauthorizedObjectResult("Companhia restrita!");
            }

            if(plane.Company.Status == true)
            {
                return new UnauthorizedObjectResult("Companhia inativa!");
            }

            var date = ParseDate(flightDTO.Departure);

            var flightExist = GetFlight(flightDTO.IATA, flightDTO.RAB, flightDTO.Departure);

            if(flightExist.Value != null)
            {
                return new BadRequestObjectResult("Esse Voo já existe!");
            }

            Flight flight = new()
            {
                Destiny = airport,
                Plane = plane,
                Sale = plane.Capacity,
                Departure = date,
                Status = flightDTO.Status
            };

            return _flightsRepository.CreateFlight(flight);
        }

        public ActionResult<Flight> UpdateFlight(string IATA, string RAB, string schedule, UpdateFlightDTO flightDTO)
        {
            DateTime date = ParseDate(schedule);

            if (ValidateIATA(IATA) && ValidateRAB(RAB))
            {
                var flight = _flightsRepository.GetFlight(IATA.ToUpper(), RAB.ToUpper(), date);

                if(flight == null)
                {
                    return new NotFoundObjectResult("Voo não encontrado!");
                }

                flight.Status = flightDTO.Status;

                return _flightsRepository.UpdateFlight(IATA.ToUpper(), RAB.ToUpper(), date, flightDTO);
            }

            return new BadRequestObjectResult("RAB ou IATA inválido!");
        }

        public ActionResult<Flight> DeleteFlight(string IATA, string RAB, string departure)
        {
            DateTime date = ParseDate(departure);

            if (ValidateIATA(IATA) && ValidateRAB(RAB))
            {
                var flight = _flightsRepository.GetFlight(IATA.ToUpper(), RAB.ToUpper(), date);

                if (flight == null)
                {
                    return new NotFoundObjectResult("Voo não encontrado!");
                }

                return _flightsRepository.DeleteFlight(IATA.ToUpper(), RAB.ToUpper(), date);
            }

            return new BadRequestObjectResult("RAB ou IATA inválido!");
        }

        private static DateTime ParseDate(string date)
        {
            var dateTimeB = date;
            var format = "dd/MM/yyyy HH:mm";
            return DateTime.ParseExact(dateTimeB, format, CultureInfo.InvariantCulture);
        }

        private static bool ValidateIATA(string iata)
        {
            if (String.IsNullOrWhiteSpace(iata)) return false;

            for (int i = 0; i < iata.Length; i++)
            {
                if (!Char.IsLetter(iata[i])) return false;
            }

            return true;
        }

        private static bool ValidateRAB(string rab)
        {
            rab = rab.ToUpper();
            if (String.IsNullOrWhiteSpace(rab)) return false;

            if (rab[2] !=  '-') return false;

            string[] vetRab = rab.Split("-");
            string rab1 = $"{vetRab[0]}{rab[2]}";

            if (rab1 != "PT-" && rab1 != "PR-") return false;

            return true;
        }
    }
}

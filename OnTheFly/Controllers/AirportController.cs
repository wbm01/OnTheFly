﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Services;

namespace OnTheFly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly AirportService _airportService;

        public AirportController()
        {
            _airportService = new AirportService();
        }


        [HttpGet(Name = "Get Airports")]
        public async Task<List<AirportDTO>> GetAirports()
        {
            return await _airportService.GetAirports();
        }

        [HttpGet("{IATA}", Name = "Get Airport By IATA")]
        public async Task<AirportDTO> GetAirportByIATA(string IATA)
        {
            return await _airportService.GetAirportByIATA(IATA);
        }


        [HttpPost(Name = "Creat Airport")]
        public async Task<Airport> CreateAirport(Airport airport)
        {
            return await _airportService.CreateAirport(airport);
        }

        [HttpDelete("{IATA}", Name = "Delete Airport")]
        public async Task<Airport> DeleteAirport(string IATA)
        {
            return await _airportService.DeleteAirport(IATA);
        }

        [HttpPut("{IATA}", Name = "Update Airport")]
        public async Task<Airport> UpdateAirport(string IATA, Airport airport)
        {
            return await _airportService.UpdateAirport(IATA, airport);
        }
    }
}

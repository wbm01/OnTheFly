﻿using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using OnTheFly.AirportServices.Config;
using OnTheFly.AirportServices.Repositories;

namespace OnTheFly.AirportServices.Services
{
    public class AirportService
    {
        private readonly IAirportRepository _airportRepository;

        public AirportService(IMongoDBConfig config)
        {
            _airportRepository = new AirportRepository(config);
        }

        public List<Airport> GetAirports()
        {
            return _airportRepository.GetAirports();
        }

       public Airport GetAirportByIATA(string IATA)
        {
            return _airportRepository.GetAirportByIATA(IATA);
        }
    }
}

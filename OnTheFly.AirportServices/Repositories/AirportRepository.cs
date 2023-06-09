﻿using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using MongoDB.Driver;
using OnTheFly.AirportServices.Config;

namespace OnTheFly.AirportServices.Repositories
{
    public class AirportRepository:IAirportRepository
    {
        private readonly IMongoCollection<Airport> _airportRepository;
        //private readonly string connectionString = "mongodb://localhost:27017";
        //private readonly string databaseName = "OnTheFlyAirport";
        //private readonly string airportCollectionName = "Airport";

        /*public AirportRepository()
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _airportRepository = database.GetCollection<Airport>(airportCollectionName);
        }*/

        public AirportRepository(IMongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.DatabaseName);
            _airportRepository = database.GetCollection<Airport>(config.AirportCollectionName);
        }

        public List<Airport> GetAirports() => _airportRepository.Find(airport => true).ToList();

        public Airport GetAirportByIATA(string IATA)
        {
            string IATAUp = IATA.ToUpper();

            return _airportRepository.Find<Airport>(airport => airport.iata == IATAUp).FirstOrDefault();
        }

        /*public Airport CreateAirport(Airport airport)
        {
            _airportRepository.InsertOne(airport);

            return airport;
        }

        public Airport UpdateAirport(string IATA, Airport airport)
        {
            string IATAUp = IATA.ToUpper();

            _airportRepository.ReplaceOne(a => a.iata == IATAUp, airport);

            return airport;
        }

        public ActionResult<Airport> DeleteAirport(string IATA)
        {
            string IATAUp = IATA.ToUpper();

            return _airportRepository.FindOneAndDelete(a => a.iata == IATAUp);
        }*/
    }
}

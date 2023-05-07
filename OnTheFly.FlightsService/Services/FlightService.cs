using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using OnTheFly.FlightsService.DTOs;
using OnTheFly.FlightsService.Repositories;

namespace OnTheFly.FlightsService.Services
{
    public class FlightService
    {
        private readonly IFlightsRepository _flightsRepository;
        private readonly HttpClient _airCraftClient;
        private readonly string _linkHost;

        public FlightService(FlightsRepository repository)
        {
            _flightsRepository = repository;
            _linkHost = "https://localhost:7250/api/AirCraft/";
            _airCraftClient = new();
        }

        public List<Flight> GetFlights()
        {
            List<Flight> response = _flightsRepository.GetFlights();
            List<Flight> flights = new();

            foreach(var flight in response)
            {
                string date = $"{flight.Departure.Day}/{flight.Departure.Month}/{flight.Departure.Year} {flight.Departure.Hour - 3}:{flight.Departure.Minute}:{flight.Departure.Second}";
                DateTime dateTime = DateTime.Parse(date);
                flight.Departure = dateTime;

                flights.Add(flight);
            }

            return flights;
        }

        public Flight GetFlight(string IATA, string RAB)
        {
            var flight = _flightsRepository.GetFlight(IATA, RAB);

            string date = $"{flight.Departure.Day}/{flight.Departure.Month}/{flight.Departure.Year} {flight.Departure.Hour - 3}:{flight.Departure.Minute}:{flight.Departure.Second}";
            DateTime dateTime = DateTime.Parse(date);
            flight.Departure = dateTime;

            return flight;
        }

        public async Task<Flight> CreateFlight(CreateFlightDTO flightDTO)
        {
            HttpResponseMessage response = await _airCraftClient.GetAsync(_linkHost + flightDTO.RAB);
            response.EnsureSuccessStatusCode();

            string airCraftResponse = await response.Content.ReadAsStringAsync();
            AirCraft plane = JsonConvert.DeserializeObject<AirCraft>(airCraftResponse);

            var dateTimeB = flightDTO.Departure;
            var format = "dd/MM/yyyy HH:mm:ss";
            var dateTime = DateTime.ParseExact(dateTimeB, format, CultureInfo.InvariantCulture);


            Flight flight = new()
            {
                Destiny = new Airport()
                {
                    iata = "ABC",
                    state = "SP",
                    city = "Araraquara"
                },
                Plane = plane,
                Sale = plane.Capacity,
                Departure = dateTime,
                Status = flightDTO.Status
            };

            return _flightsRepository.CreateFlight(flight);
        }

        public Flight UpdateFlight(string IATA, string RAB, Flight flight)
        {
            return _flightsRepository.UpdateFlight(IATA, RAB, flight);
        }

        public ActionResult<Flight> DeleteFlight(string IATA, string RAB)
        {
            return _flightsRepository.DeleteFlight(IATA, RAB);
        }
    }
}

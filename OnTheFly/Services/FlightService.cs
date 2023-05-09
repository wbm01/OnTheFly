using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using OnTheFly.FlightsService.DTOs;

namespace OnTheFly.Services
{
    public class FlightService
    {
        private readonly HttpClient _flightClient;
        private readonly string _flightHost;

        public FlightService()
        {
            _flightClient = new();
            _flightHost = "https://localhost:7076/api/Flights/";
        }

        public async Task<List<Flight>> GetFlights()
        {
            HttpResponseMessage response = await _flightClient.GetAsync(_flightHost);
            response.EnsureSuccessStatusCode();

            string airCraftResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Flight>>(airCraftResponse);
        }

        public async Task<Flight> GetFlight(string iata, string rab, string departure)
        {
            departure = departure.Replace("%2F", "/");
            HttpResponseMessage response = await _flightClient.GetAsync($"{_flightHost}{iata}, {rab}?departure={departure}");
            response.EnsureSuccessStatusCode();

            string airCraftResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Flight>(airCraftResponse);
        }

        public async Task<ActionResult<Flight>> CreateFlight(CreateFlightDTO flightDTO)
        {
            HttpResponseMessage response = await _flightClient.PostAsJsonAsync(_flightHost, flightDTO);
            response.EnsureSuccessStatusCode();

            string airCraftResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Flight>(airCraftResponse);

        }

        public async Task<Flight> UpdateFlight(string IATA, string RAB, string schedule, UpdateFlightDTO flightDTO)
        {
            //schedule = schedule.Replace("%2F", "/");
            HttpResponseMessage response = await _flightClient.PutAsJsonAsync($"{_flightHost}{IATA}, {RAB}?schedule={schedule}", flightDTO);
            response.EnsureSuccessStatusCode();

            string airCraftResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Flight>(airCraftResponse);
        }

        public async Task<ActionResult<Flight>> DeleteFlight(string IATA, string RAB, string departure)
        {
            HttpResponseMessage response = await _flightClient.DeleteAsync($"{_flightHost}{IATA}, {RAB}?departure={departure}");
            response.EnsureSuccessStatusCode();

            string airCraftResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Flight>(airCraftResponse);
        }
    }
}

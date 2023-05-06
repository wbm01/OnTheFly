using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace Services
{
    public class AirportService
    {
        static readonly HttpClient customerAirport = new HttpClient();

        public async Task<List<Airport>> GetAirports()
        {
            try
            {
                HttpResponseMessage response = await AirportService.customerAirport.GetAsync("https://localhost:7206/api/Airport");
                response.EnsureSuccessStatusCode();
                string airport = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Airport>>(airport);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Airport> GetAirportByIATA(string IATA)
        {
            try
            {
                HttpResponseMessage response = await AirportService.customerAirport.GetAsync("https://localhost:7206/api/Airport/" + IATA);
                response.EnsureSuccessStatusCode();
                string airport = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Airport>(airport);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Airport> CreateAirport(Airport airport)
        {
            try
            {
                HttpResponseMessage resposta = await customerAirport.PostAsJsonAsync("https://localhost:7206/api/Airport", airport);
                resposta.EnsureSuccessStatusCode();
                string airportReturn = await resposta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Airport>(airportReturn);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Airport> DeleteAirport(string IATA)
        {
            try
            {
                HttpResponseMessage resposta = await customerAirport.DeleteAsync("https://localhost:7206/api/Airport/" + IATA);
                resposta.EnsureSuccessStatusCode();
                string airportReturn = await resposta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Airport>(airportReturn);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Airport> UpdateAirport(string IATA, Airport airport)
        {
            try
            {
                HttpResponseMessage resposta = await customerAirport.PutAsJsonAsync("https://localhost:7206/api/Airport/" + IATA, airport);
                resposta.EnsureSuccessStatusCode();
                string airportReturn = await resposta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Airport>(airportReturn);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}

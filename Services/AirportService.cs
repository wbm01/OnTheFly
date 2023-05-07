using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.DTO;
using Newtonsoft.Json;

namespace Services
{
    public class AirportService
    {
        static readonly HttpClient customerAirport = new HttpClient();

        public async Task<List<AirportDTO>> GetAirports()
        {
            try
            {
                HttpResponseMessage response = await AirportService.customerAirport.GetAsync("https://localhost:7206/api/Airport");
                response.EnsureSuccessStatusCode();
                string airport = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<List<AirportDTO>>(airport);
                return end;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<AirportDTO> GetAirportByIATA(string IATA)
        {
            try
            {
                HttpResponseMessage response = await AirportService.customerAirport.GetAsync("https://localhost:7206/api/Airport/" + IATA);
                response.EnsureSuccessStatusCode();
                string airport = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AirportDTO>(airport);
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

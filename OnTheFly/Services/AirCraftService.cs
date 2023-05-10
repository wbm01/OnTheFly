using System.Net;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Newtonsoft.Json;

namespace OnTheFly.Services
{
    public class AirCraftService
    {
        private readonly HttpClient _airCraftClient;
        private readonly string _airCraftHost;

        public AirCraftService()
        {
            _airCraftClient = new();
            _airCraftHost = "https://localhost:5002/api/AirCraft/";
        }

        public async Task<List<AirCraft>> GetAirCrafts()
        {
            HttpResponseMessage response = await _airCraftClient.GetAsync(_airCraftHost);
            response.EnsureSuccessStatusCode();

            string airCraftResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<AirCraft>>(airCraftResponse);
        }

        public async Task<ActionResult<AirCraft>> GetAirCraftByRAB(string RAB)
        {
            HttpResponseMessage response = await _airCraftClient.GetAsync(_airCraftHost + RAB);
            response.EnsureSuccessStatusCode();

            string airCraftResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AirCraft>(airCraftResponse);
        }

        public async Task<ActionResult<AirCraft>> CreateAirCraft(CreateAirCraftDTO airCraftDTO)
        {
            try
            {
                HttpResponseMessage response = await _airCraftClient.PostAsJsonAsync(_airCraftHost, airCraftDTO);
                string airCraftResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AirCraft>(airCraftResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //response.EnsureSuccessStatusCode();            
        }

        public async Task<ActionResult<AirCraft>> UpdateAirCraft(string RAB, UpdateAirCraftDTO airCraftDTO)
        {
            HttpResponseMessage response = await _airCraftClient.PutAsJsonAsync(_airCraftHost + RAB, airCraftDTO);
            response.EnsureSuccessStatusCode();

            string airCraftResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AirCraft>(airCraftResponse);
        }

        public async Task<HttpStatusCode> DeleteAirCraft(string RAB)
        {
            HttpResponseMessage response = await _airCraftClient.DeleteAsync(_airCraftHost + RAB);
            response.EnsureSuccessStatusCode();
           
            return response.StatusCode;
        }
    }
}

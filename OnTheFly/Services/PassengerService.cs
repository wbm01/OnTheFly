using System.Text;
using Models;
using Models.DTO;
using Newtonsoft.Json;

namespace OnTheFly.Services
{
    public class PassengerService
    {

        static readonly HttpClient customerPassenger = new HttpClient();

        public async Task<List<Passenger>> GetPassenger()
        {
            try
            {
                HttpResponseMessage response = await PassengerService.customerPassenger.GetAsync("https://localhost:7240/api/Passengers");
                response.EnsureSuccessStatusCode();
                string passenger = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<List<Passenger>>(passenger);
                return end;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<List<Passenger>> GetRestritPassenger()
        {
            try
            {
                HttpResponseMessage response = await PassengerService.customerPassenger.GetAsync("https://localhost:7240/api/Passengers");
                response.EnsureSuccessStatusCode();
                string passenger = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<List<Passenger>>(passenger);
                return end;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Passenger> GetPassengerByCPF(string CPF)
        {
            try
            {
                HttpResponseMessage response = await PassengerService.customerPassenger.GetAsync("https://localhost:7240/api/Passengers/" + CPF);
                response.EnsureSuccessStatusCode();
                string passenger = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Passenger>(passenger);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Passenger> GetRestritPassengerByCPF(string CPF)
        {
            try
            {
                HttpResponseMessage response = await PassengerService.customerPassenger.GetAsync("https://localhost:7240/api/Passengers/" + CPF);
                response.EnsureSuccessStatusCode();
                string passenger = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Passenger>(passenger);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Passenger> CreatePassenger(CreatePassengerDTO passenger)
        {
            try
            {
                HttpResponseMessage resposta = await customerPassenger.PostAsJsonAsync("https://localhost:7240/api/Passengers/", passenger);
                resposta.EnsureSuccessStatusCode();
                string passengerReturn = await resposta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Passenger>(passengerReturn);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Passenger> DeletePassenger(string CPF)
        {
            try
            {
                HttpResponseMessage resposta = await customerPassenger.DeleteAsync("https://localhost:7240/api/Passengers/" + CPF);
                resposta.EnsureSuccessStatusCode();
                string passengerReturn = await resposta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Passenger>(passengerReturn);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Passenger> UpdatePassenger(string CPF, Passenger passenger)
        {
            try
            {
                HttpResponseMessage resposta = await customerPassenger.PutAsJsonAsync("https://localhost:7240/api/Passengers/" + CPF, passenger);
                resposta.EnsureSuccessStatusCode();
                string passengerReturn = await resposta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Passenger>(passengerReturn);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Passenger> UpdateStatus(string CPF, Passenger passenger)
        {
            try
            {
                HttpResponseMessage resposta = await customerPassenger.PutAsJsonAsync("https://localhost:7240/api/Passengers/" + CPF, passenger);
                resposta.EnsureSuccessStatusCode();
                string passengerReturn = await resposta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Passenger>(passengerReturn);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

    }
}

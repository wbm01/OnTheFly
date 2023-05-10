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
                HttpResponseMessage response = await PassengerService.customerPassenger.GetAsync("https://localhost:5005/api/Passengers");
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

        public async Task<List<Passenger>> GetRestrictPassenger()
        {
            try
            {
                HttpResponseMessage response = await PassengerService.customerPassenger.GetAsync("https://localhost:5005/api/Passengers");
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
                HttpResponseMessage response = await PassengerService.customerPassenger.GetAsync("https://localhost:5005/api/Passengers/" + CPF);
                response.EnsureSuccessStatusCode();
                string passenger = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Passenger>(passenger);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Passenger> GetRestrictPassengerByCPF(string CPF)
        {
            try
            {
                HttpResponseMessage response = await PassengerService.customerPassenger.GetAsync("https://localhost:5005/api/Passengers/" + CPF);
                response.EnsureSuccessStatusCode();
                string passenger = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Passenger>(passenger);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Passenger> PostPassenger(CreatePassengerDTO passenger)
        {
            try
            {
                HttpResponseMessage resposta = await customerPassenger.PostAsJsonAsync("https://localhost:5005/api/Passengers/", passenger);
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
                HttpResponseMessage resposta = await customerPassenger.DeleteAsync("https://localhost:5005/api/Passengers/" + CPF);
                resposta.EnsureSuccessStatusCode();
                string passengerReturn = await resposta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Passenger>(passengerReturn);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Passenger> PutPassenger(string CPF, Passenger passenger)
        {
            try
            {
                HttpResponseMessage resposta = await customerPassenger.PutAsJsonAsync("https://localhost:5005/api/Passengers/" + CPF, passenger);
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
                HttpResponseMessage resposta = await customerPassenger.PutAsJsonAsync("https://localhost:5005/api/Passengers/" + CPF, passenger);
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

using Models.DTO;
using Newtonsoft.Json;

namespace Services
{
    public class PostOfficeService
    {
        static readonly HttpClient street = new HttpClient();
        public static async Task<AddressDTO> GetAddress(string zipCode)
        {
            try
            {
                HttpResponseMessage response = await PostOfficeService.street.GetAsync("https://viacep.com.br/ws/" + zipCode + "/json/");
                response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<AddressDTO>(ender);
                return end;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
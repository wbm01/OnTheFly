using Models;
using Models.DTO;
using Newtonsoft.Json;

namespace Services
{
    public class PostOfficeService
    {
        static readonly HttpClient street = new HttpClient();
        public async static Task<AddressDTO> GetAddress(string zipCode)
        {
            try
            {
                HttpResponseMessage response = await PostOfficeService.street.GetAsync("https://viacep.com.br/ws/" + zipCode + "/json/");
                if (response.IsSuccessStatusCode) 
                {
                    string ender = await response.Content.ReadAsStringAsync();
                    var end = JsonConvert.DeserializeObject<AddressDTO>(ender);
                    return end;
                
                }
                //response.EnsureSuccessStatusCode();
                return null;

            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
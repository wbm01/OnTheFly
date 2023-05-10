using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Models.DTO;
using Models;
using Newtonsoft.Json;

namespace Services
{
    public class CompanyService
    {
        static readonly HttpClient customerCompany = new HttpClient();

        public async Task<List<Company>> GetCompany()
        {
            try
            {
                HttpResponseMessage response = await CompanyService.customerCompany.GetAsync("https://localhost:5001/api/Companies");
                response.EnsureSuccessStatusCode();
                string company = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<List<Company>>(company);
                return end;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Company> GetCompanyByCNPJ(string CNPJ)
        {
            try
            {
                HttpResponseMessage response = await CompanyService.customerCompany.GetAsync("https://localhost:5001/api/Companies/" + CNPJ);
                response.EnsureSuccessStatusCode();
                string company = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Company>(company);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Company> PostCompany(Company company)
        {
            try
            {
                HttpResponseMessage resposta = await customerCompany.PostAsJsonAsync("https://localhost:5001/api/Companies", company);
                resposta.EnsureSuccessStatusCode();
                string companyReturn = await resposta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Company>(companyReturn);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Company> DeleteCompany(string CNPJ)
        {
            try
            {
                HttpResponseMessage resposta = await customerCompany.DeleteAsync("https://localhost:5001/api/Companies/" + CNPJ);
                resposta.EnsureSuccessStatusCode();
                string companyReturn = await resposta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Company>(companyReturn);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Company> PutCompany(string CNPJ, CompanyDTO company)
        {
            try
            {
                HttpResponseMessage resposta = await customerCompany.PutAsJsonAsync("https://localhost:5001/api/Companies/" + CNPJ, company);
                resposta.EnsureSuccessStatusCode();
                string companyReturn = await resposta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Company>(companyReturn);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Company> UpdateStatus(string CNPJ, Company company)
        {
            try
            {
                HttpResponseMessage resposta = await customerCompany.PutAsJsonAsync("https://localhost:5001/api/Companies/" + CNPJ, company);
                resposta.EnsureSuccessStatusCode();
                string companyReturn = await resposta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Company>(companyReturn);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Services;

namespace OnTheFly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyService _companyService;

        public CompanyController()
        {
            _companyService = new CompanyService();
        }


        [HttpGet(Name = "GetListCompany")]
        public async Task<List<Company>> GetCompany()
        {
            return await _companyService.GetCompany();
        }

        [HttpGet("{CNPJ}", Name = "GetCompanyByCNPJ")]
        public async Task<Company> GetCompanyByCNPJ(string CNPJ)
        {
            return await _companyService.GetCompanyByCNPJ(CNPJ);
        }


        [HttpPost(Name = "CreateCompany")]
        public async Task<Company> CreateCompany(Company company)
        {
            return await _companyService.CreateCompany(company);
        }

        [HttpDelete("{CNPJ}", Name = "DeleteCompany")]
        public async Task<Company> DeleteCompany(string CNPJ)
        {
            return await _companyService.DeleteCompany(CNPJ);
        }

        [HttpPut("{CNPJ}", Name = "UpdateCompany")]
        public async Task<Company> UpdateCompany(string CNPJ, CompanyDTO company)
        {
            return await _companyService.UpdateCompany(CNPJ, company);
        }

        [HttpPut("restrict/{CNPJ}", Name = "UpdateStatusCompany")]
        public async void UpdateStatus(string CNPJ, Company company)
        {
            await _companyService.UpdateStatus(CNPJ, company);
        }

        /*[HttpPut("restrict/{CNPJ}", Name = "UpdateStatus")]
        public async void UpdateStatus(string CNPJ, Company company)
        {
            await _companyService.UpdateStatus(CNPJ, company);
        }*/
    }
}

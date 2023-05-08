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


        [HttpGet(Name = "Get Company")]
        public async Task<List<Company>> GetCompany()
        {
            return await _companyService.GetCompany();
        }

        /*[HttpGet("{CNPJ}", Name = "Get Company By CNPJ")]
        public async Task<Company> GetCompanyByCNPJ(string CNPJ)
        {
            return await _companyService.GetCompanyByCNPJ(CNPJ);
        }*/


        [HttpPost(Name = "Create Company")]
        public async Task<Company> CreateCompany(Company company)
        {
            return await _companyService.CreateCompany(company);
        }

        [HttpDelete("{CNPJ}", Name = "Delete Company")]
        public async Task<Company> DeleteCompany(string CNPJ)
        {
            return await _companyService.DeleteCompany(CNPJ);
        }

        [HttpPut("{CNPJ}", Name = "Update Company")]
        public async Task<CompanyDTO> UpdateCompany(string CNPJ, Company company)
        {
            return await _companyService.UpdateCompany(CNPJ, company);
        }
    }
}

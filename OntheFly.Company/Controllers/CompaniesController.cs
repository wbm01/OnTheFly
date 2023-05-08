﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using OnTheFly.CompanyServices.Services;

namespace OnTheFly.CompanyServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly CompanyService _companiesService;
        public CompaniesController()
        {
            _companiesService = new();
        }

        [HttpGet]
        public ActionResult<List<Company>> GetCompany() => _companiesService.GetCompany();

        [HttpGet("{CNPJ}", Name = "Get Company By CNPJ")]
        public ActionResult<Company> GetCompanyByCNPJ(string CNPJ) => _companiesService.GetCompanyByCNPJ(CNPJ);

        [HttpPost]
        public ActionResult<Company> PostCompany(Company company) => _companiesService.PostCompany(company);

        [HttpPut]
        public ActionResult<Company> UpdateCompany(string CNPJ, CompanyDTO company) => _companiesService.UpdateCompany(CNPJ, company);

        [HttpDelete]
        public ActionResult<Company> DeleteCompany(string CNPJ) => _companiesService.DeleteCompany(CNPJ);
    }
}

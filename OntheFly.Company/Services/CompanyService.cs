using Microsoft.AspNetCore.Mvc;
using Models;
using OnTheFly.CompanyServices.Repositories;

namespace OnTheFly.CompanyServices.Services
{
    public class CompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService()
        {
            _companyRepository = new CompanyRepository();
        }

        public List<Company> GetCompany() => _companyRepository.GetCompany();
        public Company GetCompanyByCNPJ(string CNPJ) => _companyRepository.GetCompanyByCNPJ(CNPJ);
        public Company PostCompany(Company company) => _companyRepository.PostCompany(company);
        public Company UpdateCompany(string CNPJ, Company company) => _companyRepository.UpdateCompany(CNPJ, company);
        public ActionResult<Company> DeleteCompany(string CNPJ) => _companyRepository.DeleteCompany(CNPJ);
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Models;
using Models.DTO;
using OnTheFly.CompanyServices.Repositories;
using Services;

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
        public ActionResult<Company> PostCompany(Company company)
        {
            if (String.IsNullOrWhiteSpace(company.CNPJ))
                return new BadRequestResult();

            Address address = PostOfficeService.GetAddress(company.Address.CEP).Result;
            //Address addressComplete = new Address(addressDTO);
            //addressComplete.Number = company.Address.Number;
            address.Number = company.Address.Number;
            //company.Address = addressComplete;
            company.Address = address;

            return _companyRepository.PostCompany(company);
        }
        public Company UpdateCompany(string CNPJ, Company company) => _companyRepository.UpdateCompany(CNPJ, company);
        public ActionResult<Company> DeleteCompany(string CNPJ) => _companyRepository.DeleteCompany(CNPJ);
    }
}

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

            AddressDTO addressDTO = PostOfficeService.GetAddress(company.Address.ZipCode).Result;

            if (addressDTO == null)
            {
                return new NotFoundResult();
            }

            if(addressDTO.CEP == null)
            {
                return new NotFoundResult();
            }

            Address addressComplete = new Address(addressDTO);
            addressComplete.Number = company.Address.Number;
            company.Address = addressComplete;

            if(company.NameOpt == "string" || company.NameOpt == "" || string.IsNullOrWhiteSpace(company.NameOpt))
            {
                company.NameOpt = company.Name;
            }

            return _companyRepository.PostCompany(company);
        }

        public ActionResult <Company> UpdateCompany(string CNPJ, CompanyDTO companyDTO) {

            var company = _companyRepository.GetCompanyByCNPJ(CNPJ);

            AddressDTO addressDTO = PostOfficeService.GetAddress(companyDTO.ZipCode).Result;

            if(addressDTO == null)
            {
                return new NotFoundResult();
            }

            if (addressDTO.CEP == null)
            {
                return new NotFoundResult();
            }

            Address addressComplete = new Address(addressDTO);
            addressComplete.Number = companyDTO.Number;

            if (company.NameOpt == "string" || company.NameOpt == "" || string.IsNullOrWhiteSpace(company.NameOpt))
            {
                company.NameOpt = company.Name;
            }

            company.NameOpt = companyDTO.NameOpt;
            company.Status = companyDTO.Status;
            company.Address = addressComplete;
            
            return _companyRepository.UpdateCompany(CNPJ, company);
        }

        public ActionResult<Company> UpdateStatus(string CNPJ)
        {
            var result = _companyRepository.GetCompanyByCNPJ(CNPJ);
            if (result != null)
            {
                return _companyRepository.RestritCompany(CNPJ);
            }
            else
            {
                return _companyRepository.NoRestritCompany(CNPJ);
            }
        }

        public ActionResult<Company> DeleteCompany(string CNPJ) => _companyRepository.DeleteCompany(CNPJ);
    }
}

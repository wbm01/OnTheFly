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

        public List<Company> GetRestritCompany() => _companyRepository.GetRestritCompany();

        public ActionResult<Company> PostCompany(Company company)
        {
            if (CheckCNPJ(company.CNPJ) == false)
            {
                return new BadRequestResult();
            }

            var result = _companyRepository.GetCompanyByCNPJ(company.CNPJ);

            if (result == null)
            {
                AddressDTO addressDTO = PostOfficeService.GetAddress(company.Address.ZipCode).Result;

                if (addressDTO == null)
                {
                    return new NotFoundResult();
                }

                if (addressDTO.CEP == null)
                {
                    return new NotFoundResult();
                }

                Address addressComplete = new Address(addressDTO);
                addressComplete.Number = company.Address.Number;
                company.Address = addressComplete;

                if (company.NameOpt == "string" || company.NameOpt == "" || string.IsNullOrWhiteSpace(company.NameOpt))
                {
                    company.NameOpt = company.Name;
                }

                return _companyRepository.PostCompany(company);
            }

            return new BadRequestResult();
        }

        public ActionResult<Company> UpdateCompany(string CNPJ, CompanyDTO companyDTO)
        {

            var company = _companyRepository.GetCompanyByCNPJ(CNPJ);

            AddressDTO addressDTO = PostOfficeService.GetAddress(companyDTO.ZipCode).Result;

            if (addressDTO == null)
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
            CNPJ = CNPJ.Replace("%2F", "/");

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

        public static bool CheckCNPJ(string cnpj)
        {
            if (String.IsNullOrWhiteSpace(cnpj))
                return false;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            int[] multiplicador1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            // Verifica os Patterns mais Comuns para CNPJ's Inválidos
            if (cnpj.Equals("00000000000000") ||
                cnpj.Equals("11111111111111") ||
                cnpj.Equals("22222222222222") ||
                cnpj.Equals("33333333333333") ||
                cnpj.Equals("44444444444444") ||
                cnpj.Equals("55555555555555") ||
                cnpj.Equals("66666666666666") ||
                cnpj.Equals("77777777777777") ||
                cnpj.Equals("88888888888888") ||
                cnpj.Equals("99999999999999"))
            {
                return false;
            }

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }
    }
}

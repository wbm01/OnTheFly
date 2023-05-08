using Microsoft.AspNetCore.Mvc;
using Models;

namespace OnTheFly.CompanyServices.Repositories
{
    public interface ICompanyRepository
    {
        List<Company> GetCompany();
        Company GetCompanyByCNPJ(string CNPJ);
        Company PostCompany(Company company);
        Company UpdateCompany(string CNPJ, Company company);
        ActionResult<Company> DeleteCompany(string CNPJ);
        Company RestritCompany(string CNPJ);
        Company NoRestritCompany(string CNPJ);
    }
}

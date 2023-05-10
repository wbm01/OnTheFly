using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Models;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace OnTheFly.CompanyServices.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IMongoCollection<Company> _companyRepository;
        private readonly IMongoCollection<Company> _companyRepositoryRestrict;
        private readonly string _connectionString = "mongodb://localhost:27017";
        private readonly string _databaseName = "DBCompany";
        private readonly string _companyCollectionName = "Company";
        private readonly string _companyCollectionRestrictName = "CompanyRestrict";

        public CompanyRepository()
        {
            var company = new MongoClient(_connectionString);
            var database = company.GetDatabase(_databaseName);
            _companyRepository = database.GetCollection<Company>(_companyCollectionName);
            _companyRepositoryRestrict = database.GetCollection<Company>(_companyCollectionRestrictName);
        }

        public ActionResult<Company> DeleteCompany(string CNPJ) => _companyRepository.FindOneAndDelete(a => a.CNPJ == CNPJ);

        public List<Company> GetCompany() => _companyRepository.Find(c => true).ToList();

        public Company GetCompanyByCNPJ(string CNPJ) => _companyRepository.Find(c => c.CNPJ == CNPJ).FirstOrDefault();

        public List<Company> GetRestritCompany() => _companyRepositoryRestrict.Find(c => true).ToList();

        public Company PostCompany(Company company)
        {
            _companyRepository.InsertOne(company);
            return company;
        }

        public Company UpdateCompany(string CNPJ, Company company)
        {
            //var options = new FindOneAndUpdateOptions<Company, Company> { };
            //var update = Builders<Company>.Update.Set();
            //var newcompany = _companyRepository.FindOneAndUpdate<Company>(a => a.CNPJ == CNPJ, update,  company);
            _companyRepository.ReplaceOne(c => c.CNPJ == CNPJ, company);

            return company;
        }

        public Company RestritCompany(string CNPJ)
        {
            var consult = GetCompanyByCNPJ(CNPJ);
            _companyRepositoryRestrict.InsertOne(consult);
            _companyRepository.DeleteOne(c => c.CNPJ == CNPJ);
            return consult;
        }
        public Company NoRestritCompany(string CNPJ)
        {
            var consult = _companyRepositoryRestrict.Find(p => p.CNPJ == CNPJ).FirstOrDefault();
            _companyRepository.InsertOne(consult);
            _companyRepositoryRestrict.DeleteOne(c => c.CNPJ == CNPJ);
            return consult;
        }
    }
}

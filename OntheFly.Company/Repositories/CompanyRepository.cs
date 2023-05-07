﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Models;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace OnTheFly.CompanyServices.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IMongoCollection<Company> _companyRepository;
        private readonly string _connectionString = "mongodb://localhost:27017";
        private readonly string _databaseName = "DBCompany";
        private readonly string _companyCollectionName = "Company";

        public CompanyRepository()
        {
            var company = new MongoClient(_connectionString);
            var database = company.GetDatabase(_databaseName);
            _companyRepository = database.GetCollection<Company>(_companyCollectionName);
        }

        public ActionResult<Company> DeleteCompany(string CNPJ) => _companyRepository.FindOneAndDelete(a => a.CNPJ == CNPJ);

        public List<Company> GetCompany() => _companyRepository.Find(c => true).ToList();

        public Company GetCompanyByCNPJ(string CNPJ) => _companyRepository.Find(c => c.CNPJ == CNPJ).FirstOrDefault();

        public Company PostCompany(Company company)
        {
            _companyRepository.InsertOne(company);
            return company;
        }

        public Company UpdateCompany(string CNPJ, Company company)
        {
            _companyRepository.ReplaceOne(c => c.CNPJ == CNPJ, company);
            return company;
        }
    }
}

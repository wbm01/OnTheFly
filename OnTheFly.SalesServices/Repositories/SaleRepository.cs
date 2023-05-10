using System.Net;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using MongoDB.Driver;
using OnTheFly.SalesServices.Config;

namespace OnTheFly.SalesServices.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly IMongoCollection<Sale> _saleRepository;
        private readonly HttpClient _saleClient;
        private readonly string _producerSaleSoldHost;
        private readonly string _producerSaleReservedHost;

        public SaleRepository(IMongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.DatabaseName);
            _saleRepository = database.GetCollection<Sale>(config.SalesCollectionName);
            _saleClient = new HttpClient();
            _producerSaleSoldHost = "https://localhost:5007/api/SalesSold";
            _producerSaleReservedHost = "https://localhost:5007/api/SalesReserved";
        }

        public ActionResult<Sale> DeleteSale(string iata, string rab, DateTime departure)
        {
            var filter = CreateFilter(iata, rab, departure);

            return _saleRepository.FindOneAndDelete(filter);
        }

        public List<Sale> GetSale() => _saleRepository.Find(s => true).ToList();

        public Sale GetSaleByFlight(string iata, string rab, DateTime departure)
        {
            var filter = CreateFilter(iata, rab, departure);

            return _saleRepository.Find(filter).FirstOrDefault();
        }

        public async Task<ActionResult<Sale>> PostSale(Sale sale)
        {
            if (sale.Sold == true)
            {
                HttpResponseMessage response = await _saleClient.PostAsJsonAsync("https://localhost:5007/api/SalesSold", sale);
                response.EnsureSuccessStatusCode();
                return new StatusCodeResult(200);
            }

            else
            {
                HttpResponseMessage response = await _saleClient.PostAsJsonAsync("https://localhost:5007/api/SalesReserved", sale);
                response.EnsureSuccessStatusCode();
                return new StatusCodeResult(200);
            }

        }

        public Sale UpdateSale(string iata, string rab, DateTime departure, SaleDTO saleDTO)
        {
            var filter = CreateFilter(iata, rab, departure);

            Sale sale = _saleRepository.Find(filter).FirstOrDefault();

            sale.Sold = saleDTO.Sold;
            sale.Reserved = saleDTO.Reserved;

            _saleRepository.ReplaceOne(filter, sale);

            return sale;
        }

        public FilterDefinition<Sale> CreateFilter(string iata, string rab, DateTime departure)
        {
            var builder = Builders<Sale>.Filter;

            var airPort = builder.Eq(s => s.Flight.Destiny.iata, iata);
            var plane = builder.Eq(s => s.Flight.Plane.RAB, rab);
            var datetime = builder.Eq(s => s.Flight.Departure, departure);

            return builder.And(airPort, plane, datetime);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models;
using OnTheFly.SalesServices.Repositories;

namespace OnTheFly.SalesServices.Services
{
    public class SaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(SaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public List<Sale> GetSale() => _saleRepository.GetSale();

        public Sale GetSaleByFlight(string iata, string rab, DateTime departure) => _saleRepository.GetSaleByFlight(iata, rab, departure);
        public ActionResult<Sale> PostSale(Sale sale)
        { 
            return _saleRepository.PostSale(sale);
        }
        public Sale UpdateSale(string iata, string rab, DateTime departure, SaleDTO saleDTO) => _saleRepository.UpdateSale(iata, rab, departure, saleDTO);
        public ActionResult<Sale> DeleteSale(string iata, string rab, DateTime departure) => _saleRepository.DeleteSale(iata, rab, departure);
    }
}

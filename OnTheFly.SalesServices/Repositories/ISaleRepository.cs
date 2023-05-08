using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace OnTheFly.SalesServices.Repositories
{
    public interface  ISaleRepository
    {
        List<Sale> GetSale();
        Sale GetSaleByFlight(string iata, string rab, DateTime departure);
        Sale PostSale(Sale sale);
        Sale UpdateSale(string iata, string rab, DateTime departure, SaleDTO saleDTO);
        ActionResult<Sale> DeleteSale(string iata, string rab, DateTime departure);
    }
}

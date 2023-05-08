using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using OnTheFly.SalesServices.Services;

namespace OnTheFly.SalesServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly SaleService _saleService;

        public SalesController(SaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public ActionResult<List<Sale>> GetSale() => _saleService.GetSale();

        [HttpGet("{iata}, {rab}, {departure}", Name = "Get Sale By Flight")]
        public ActionResult<Sale> GetSaleByFlight(string iata, string rab, DateTime departure) => _saleService.GetSaleByFlight(iata, rab, departure);

        [HttpPost]
        public ActionResult<Sale> PostSale(Sale Sale) => _saleService.PostSale(Sale);

        //[HttpPut]
        //public ActionResult<Sale> UpdateSale(string CNPJ, Sale sale) => _saleService.UpdateSale(CNPJ, sale);

        //[HttpDelete]
        //public ActionResult<Sale> DeleteSale(string CNPJ) => _saleService.DeleteSale(CNPJ);
    }
}

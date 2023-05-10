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
        public Task<ActionResult<Sale>> PostSale(CreateSaleDTO saleDTO) => _saleService.PostSale(saleDTO);

        [HttpPut("{iata}, {rab}, {departure}")]
        public ActionResult<Sale> UpdateSale(string iata, string rab, DateTime departure, SaleDTO saleDTO) => _saleService.UpdateSale(iata, rab, departure, saleDTO);

        [HttpDelete("{iata}, {rab}, {departure}")]
        public ActionResult<Sale> DeleteSale(string iata, string rab, DateTime departure) => _saleService.DeleteSale(iata, rab, departure);
    }
}

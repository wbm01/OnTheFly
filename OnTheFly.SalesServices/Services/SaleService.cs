using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models;
using OnTheFly.SalesServices.Repositories;
using MongoDB.Driver;
using System.Linq;
using Newtonsoft.Json;
using System.Globalization;

namespace OnTheFly.SalesServices.Services
{
    public class SaleService
    {
        //private const string BrazilianTimeZoneId = "E. South America Standard Time";
        private readonly ISaleRepository _saleRepository;
        private readonly HttpClient _saleClient;
        private readonly string _flightHost;
        private readonly string _passengerHost;

        public SaleService(SaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
            _flightHost = "https://localhost:7076/api/Flights/";
            _passengerHost = "https://localhost:7240/api/Passengers/";
            _saleClient = new();
        }

        public List<Sale> GetSale() => _saleRepository.GetSale();

        public Sale GetSaleByFlight(string iata, string rab, DateTime departure) => _saleRepository.GetSaleByFlight(iata, rab, departure);
        public async Task<ActionResult<Sale>> PostSale(Sale sale)
        {
            //numero de vendas nao exceda a capacidade do aviao ****
            //verificar se o passageiro nao esta restrito, cancelar venda de verdadeiro

            //var date = TimeZoneInfo.ConvertTime(sale.Flight.Departure, TimeZoneInfo.FindSystemTimeZoneById(BrazilianTimeZoneId));
            //var newdate = ParseDate(date);
            //var dateurl = newdate.ToString();
            //date = date.Replace("/", "%2F");
            //dateurl = dateurl.Replace(" ", "%20");
            //dateurl = dateurl.Replace(":", "%3A");
            //HttpResponseMessage flightResponse = await _saleClient.GetAsync(_flightHost + saleDTO.iata + ", "+ saleDTO.rab + "?departure=" + date);
            HttpResponseMessage flightResponse = await _saleClient.GetAsync(_flightHost + sale.Flight.Destiny.iata + ", " + sale.Flight.Plane.RAB + "?departure=" + "20/04/2023 13:40");
            
            flightResponse.EnsureSuccessStatusCode();
            string flightStr = await flightResponse.Content.ReadAsStringAsync();
            sale.Flight = JsonConvert.DeserializeObject<Flight>(flightStr);

            List<Passenger> passengerlist = new();

            foreach (var cpf in sale.Passenger)
            {
                if (cpf == null)
                {
                    return new BadRequestResult();
                }
                else
                {
                    HttpResponseMessage passengerResponse = await _saleClient.GetAsync(_passengerHost + cpf.CPF);
                    passengerResponse.EnsureSuccessStatusCode();

                    string passengerStr = await passengerResponse.Content.ReadAsStringAsync();
                    Passenger newpassenger = JsonConvert.DeserializeObject<Passenger>(passengerStr);
                    passengerlist.Add(newpassenger);
                }
            }

            sale.Passenger = passengerlist;
            //Sale sale = new Sale()
            //{
            //    Flight = flight,
            //    Passenger = passengerlist,
            //    Reserved = false,
            //    Sold = false
            //};

            List<Sale> salelist = _saleRepository.GetSale();
            List<Sale> sales = new();

            foreach (var newsales in salelist)
            {
                if(newsales.Flight.Destiny.iata == sale.Flight.Destiny.iata && newsales.Flight.Plane.RAB == sale.Flight.Plane.RAB && DateTime.Compare(newsales.Flight.Departure, sale.Flight.Departure) == 0)
                {
                    sales.Add(newsales);
                }
            }

            //_saleRepository.GetSaleByFlights(sale.Flight.Destiny.iata, sale.Flight.Plane.RAB, sale.Flight.Departure);

            //if (sales.Count() == sale.Flight.Plane.Capacity)
            //{
            //    return new ForbidResult("Limite de assentos foi atingido");
            //}
            //sale.Flight.Sale -= sale.Passenger.Count();

            int idade = CalcularIdade(sale.Passenger[0]);
            if (idade < 18)
            {
                return new BadRequestObjectResult("Passageiro menor de 18 anos");
            }

            foreach (var sale1 in sales)
            {
                foreach (var passenger in sale1.Passenger)
                {
                    foreach (var salepassenger in sale.Passenger)
                    {
                        if (salepassenger.CPF == passenger.CPF)
                        {
                            return new BadRequestObjectResult("CPF do passageiro já foi cadastrado");
                        }
                    }
                }
            }

            return _saleRepository.PostSale(sale);
        }

        public Sale UpdateSale(string iata, string rab, DateTime departure, SaleDTO saleDTO) => _saleRepository.UpdateSale(iata, rab, departure, saleDTO);
        public ActionResult<Sale> DeleteSale(string iata, string rab, DateTime departure) => _saleRepository.DeleteSale(iata, rab, departure);

        public static int CalcularIdade(Passenger passenger)
        {
            int idade = DateTime.Now.Year - passenger.DtBirth.Year;
            if (DateTime.Now.DayOfYear < passenger.DtBirth.DayOfYear)
            {
                idade = idade - 1;
            }
            return idade;
        }
        private static DateTime ParseDate(string date)
        {
            var dateTimeB = date;
            var format = "dd/MM/yyyy HH:mm";
            return DateTime.ParseExact(dateTimeB, format, CultureInfo.InvariantCulture);
        }
    }
}

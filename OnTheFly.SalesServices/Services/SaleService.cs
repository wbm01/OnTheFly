using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models;
using OnTheFly.SalesServices.Repositories;
using MongoDB.Driver;
using System.Linq;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;

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
        public async Task<ActionResult<Sale>> PostSale(CreateSaleDTO saleDTO)
        {
            HttpResponseMessage flightResponse = await _saleClient.GetAsync(_flightHost + saleDTO.Iata + ", " + saleDTO.Rab + "?departure=" + saleDTO.Departure);

            flightResponse.EnsureSuccessStatusCode();
            string flightStr = await flightResponse.Content.ReadAsStringAsync();
            Flight flight = JsonConvert.DeserializeObject<Flight>(flightStr);

            List<Passenger> passengerlist = new();

            foreach (var cpf in saleDTO.passengersCPFlist)
            {
                HttpResponseMessage passengerResponse = await _saleClient.GetAsync(_passengerHost + cpf);
                passengerResponse.EnsureSuccessStatusCode();

                if (passengerResponse.Content.Headers.ContentLength == 0)
                {
                    if (!VerifyPassenger(cpf).Result) return new NotFoundObjectResult("CPF não encontrado");
                    return new UnauthorizedObjectResult("Passageiro está restrito e não pode comprar uma passagem");
                }

                string passengerStr = await passengerResponse.Content.ReadAsStringAsync();
                Passenger newpassenger = JsonConvert.DeserializeObject<Passenger>(passengerStr);
                passengerlist.Add(newpassenger);
            }

            flight.Sale -= passengerlist.Count();
            if (flight.Sale < 0)
            {
                return new BadRequestObjectResult("Assentos esgotados");
            }

            Sale sale = new Sale()
            {
                Flight = flight,
                Passenger = passengerlist,
                Reserved = false,
                Sold = false
            };

            if (AgeCalculate(sale.Passenger[0])) return new BadRequestObjectResult("Passageiro menor de 18 anos");

            List<Sale> allsales = _saleRepository.GetSale();
            List<Sale> salesflight = allsales.FindAll(s => s.Flight._id == sale.Flight._id).ToList();

            foreach (var sale1 in salesflight)
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

            //bool test = false;
            //salesflight.ForEach(s =>
            //{
            //    s.Passenger.ForEach(p =>
            //    {
            //        test = sale.Passenger.Any(passenger =>
            //        {
            //            passenger.CPF = p.CPF;
            //        });
            //    });
            //}); 

            return _saleRepository.PostSale(sale);
        }

        public Sale UpdateSale(string iata, string rab, DateTime departure, SaleDTO saleDTO) => _saleRepository.UpdateSale(iata, rab, departure, saleDTO);
        public ActionResult<Sale> DeleteSale(string iata, string rab, DateTime departure) => _saleRepository.DeleteSale(iata, rab, departure);

        public static bool AgeCalculate(Passenger passenger)
        {
            int idade = DateTime.Now.Year - passenger.DtBirth.Year;
            if (DateTime.Now.DayOfYear < passenger.DtBirth.DayOfYear)
            {
                idade = idade - 1;
            }
            if (idade < 18) return true;

            return false;
        }

        public async Task<bool> VerifyPassenger(string CPF)
        {
            HttpResponseMessage passengerResponse = await _saleClient.GetAsync(_passengerHost + CPF + "?restrict=true");
            passengerResponse.EnsureSuccessStatusCode();

            if (passengerResponse.Content.Headers.ContentLength == 0) return false;

            return true;
        }
    }
}

using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using OnTheFly.PassengerServices.Repositories;
using Services;

namespace OnTheFly.PassengerServices.Services
{
    public class PassengerService
    {
        private readonly IPassengerRepository _passengerRepository;
        public PassengerService()
        {
            _passengerRepository = new PassengerRepository();
        }
        public ActionResult<Passenger> DeletePassenger(string CPF)
        {
            if (!ValidarCPF(CPF))
                return new BadRequestResult();

            return _passengerRepository.DeletePassenger(CPF);
        }
        public List<Passenger> GetPassenger()
        {
            List<Passenger> response = _passengerRepository.GetPassenger();
            List<Passenger> passengers = new List<Passenger>();

            foreach (var passenger in response)
            {
                string date = $"{passenger.DtBirth.Day}/{passenger.DtBirth.Month}/{passenger.DtBirth.Year}";
                DateTime dateTime = DateTime.Parse(date);
                passenger.DtBirth = dateTime;
                passengers.Add(passenger);
            }

            return passengers;
        }

        public List<Passenger> GetRestritPassenger() => _passengerRepository.GetRestritPassenger();

        public ActionResult<Passenger> GetPassengerByCPF(string CPF)
        {
            if (!ValidarCPF(CPF))
                return new BadRequestResult();

            return _passengerRepository.GetPassengerByCPF(CPF);
        }
        public ActionResult<Passenger> PostPassenger(CreatePassengerDTO passenger)
        {
            if (!ValidarCPF(passenger.CPF))
                return new BadRequestResult();

            if (passenger.Gender != 'M' && passenger.Gender != 'm' && passenger.Gender != 'f' && passenger.Gender != 'F')
                return new BadRequestResult();

            AddressDTO address = PostOfficeService.GetAddress(passenger.ZipCode).Result;
            Address addressComplete = new Address(address);
            addressComplete.Number = passenger.Number;

            Passenger passengerComplete = new Passenger(passenger);

            passengerComplete.Address = addressComplete;

            var date = ParseDate(passenger.DtBirth);
            passengerComplete.DtBirth = date;

            return _passengerRepository.PostPassenger(passengerComplete);
        }
        public ActionResult<Passenger> UpdatePassenger(UpdatePassengerDTO passenger, string CPF)
        {
            if (!ValidarCPF(CPF))
                return new BadRequestResult();

            var aux = _passengerRepository.GetPassengerByCPF(CPF);
            aux.Name = passenger.Name;
            aux.Gender = passenger.Gender;
            aux.Phone = passenger.Phone;

            var date = ParseDate(passenger.DtBirth);
            aux.DtBirth = date;
            aux.Status = passenger.Status;
            AddressDTO address = PostOfficeService.GetAddress(passenger.ZipCode).Result;

            if (address == null)
                return new NotFoundResult();

            if(address.CEP == null)
                return new NotFoundResult();


            Address addressComplete = new Address(address);
            addressComplete.Number = passenger.Number;
            aux.Address = addressComplete;  

            return _passengerRepository.UpdatePassenger(aux, CPF);
        }
        public ActionResult<Passenger> UpdateStatus(string CPF)
        {
            var result = _passengerRepository.GetPassengerByCPF(CPF);
            
            if(result == null)
            {
                return new BadRequestResult();  
            }

            if(result != null)
            {
                return _passengerRepository.RestritPassenger(CPF);
            }
            else
            {
                return _passengerRepository.NoRestritPassenger(CPF);
            }

        }

        private static bool ValidarCPF(string sourceCPF)
        {
            if (String.IsNullOrWhiteSpace(sourceCPF))
                return false;

            string clearCPF;
            clearCPF = sourceCPF.Trim();
            clearCPF = clearCPF.Replace("-", "");
            clearCPF = clearCPF.Replace(".", "");

            if (clearCPF.Length != 11)
            {
                return false;
            }

            int[] cpfArray;
            int totalDigitoI = 0;
            int totalDigitoII = 0;
            int modI;
            int modII;

            if (clearCPF.Equals("00000000000") ||
                clearCPF.Equals("11111111111") ||
                clearCPF.Equals("22222222222") ||
                clearCPF.Equals("33333333333") ||
                clearCPF.Equals("44444444444") ||
                clearCPF.Equals("55555555555") ||
                clearCPF.Equals("66666666666") ||
                clearCPF.Equals("77777777777") ||
                clearCPF.Equals("88888888888") ||
                clearCPF.Equals("99999999999"))
            {
                return false;
            }

            foreach (char c in clearCPF)
            {
                if (!char.IsNumber(c))
                {
                    return false;
                }
            }

            cpfArray = new int[11];
            for (int i = 0; i < clearCPF.Length; i++)
            {
                cpfArray[i] = int.Parse(clearCPF[i].ToString());
            }

            for (int posicao = 0; posicao < cpfArray.Length - 2; posicao++)
            {
                totalDigitoI += cpfArray[posicao] * (10 - posicao);
                totalDigitoII += cpfArray[posicao] * (11 - posicao);
            }

            modI = totalDigitoI % 11;
            if (modI < 2) { modI = 0; }
            else { modI = 11 - modI; }

            if (cpfArray[9] != modI)
            {
                return false;
            }

            totalDigitoII += modI * 2;

            modII = totalDigitoII % 11;
            if (modII < 2) { modII = 0; }
            else { modII = 11 - modII; }
            if (cpfArray[10] != modII)
            {
                return false;
            }
            // CPF Válido!
            return true;
        }

        private static DateTime ParseDate(string date)
        {
            var dateTimeB = date;
            var format = "dd/MM/yyyy";
            return DateTime.ParseExact(dateTimeB, format, CultureInfo.InvariantCulture);
        }

    }


}

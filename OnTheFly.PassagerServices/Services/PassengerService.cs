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
            if (!validateCPF(CPF))
               return new BadRequestObjectResult("CPF inválido !");

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
            if (!validateCPF(CPF))
                return new BadRequestObjectResult("CPF inválido !");

            return _passengerRepository.GetPassengerByCPF(CPF);
        }
        public ActionResult<Passenger> PostPassenger(CreatePassengerDTO passenger)
        {
            if (!validateCPF(passenger.CPF))
                return new BadRequestObjectResult("CPF inválido !");

            if(passenger.Phone != null && passenger.Phone.Length > 14)
                return new BadRequestObjectResult("Número de telefone inválido !");
            
            if (passenger.Gender.Length == 0)
                return new BadRequestObjectResult("Gênero não preechido!");
            

            string gender = passenger.Gender.Trim().ToUpper();
            var firstLetter = gender[0];

            if (gender.Equals("MASCULINO") == false && 
                gender.Equals("FEMININO") == false &&
                firstLetter != 'M' && firstLetter != 'F')
            {
                return new BadRequestObjectResult("Gênero incorreto!");
            }

          
            Passenger passengerComplete = new Passenger(passenger);
            passengerComplete.Gender = firstLetter;

            AddressDTO address = PostOfficeService.GetAddress(passenger.ZipCode).Result;

            if (address == null)
                return new BadRequestObjectResult("Endereço inválido !");

            if (address.CEP == null)
                return new BadRequestObjectResult("Endereço inválido !");

            Address addressComplete = new Address(address);
            addressComplete.Number = passenger.Number;

            passengerComplete.Address = addressComplete;

            var date = ParseDate(passenger.DtBirth);

            int result = DateTime.Compare(date, DateTime.Now);

            if (result > 0)
                return new BadRequestObjectResult("Data de nascimento errada!");

            passengerComplete.DtBirth = date;

            return _passengerRepository.PostPassenger(passengerComplete);
        }
        public ActionResult<Passenger> UpdatePassenger(UpdatePassengerDTO passenger, string CPF)
        {
            if (!validateCPF(CPF))
                return new BadRequestObjectResult("CPF inválido !");

            var auxiliaryPassenger = _passengerRepository.GetPassengerByCPF(CPF);
            auxiliaryPassenger.Name = passenger.Name;
            auxiliaryPassenger.Gender = passenger.Gender;
            auxiliaryPassenger.Phone = passenger.Phone;

            var date = ParseDate(passenger.DtBirth);
            auxiliaryPassenger.DtBirth = date;
            auxiliaryPassenger.Status = passenger.Status;
            AddressDTO address = PostOfficeService.GetAddress(passenger.ZipCode).Result;

            if (address == null)
                return new BadRequestObjectResult("Endereço inválido !");

            if (address.CEP == null)
                return new BadRequestObjectResult("Endereço inválido !");


            Address addressComplete = new Address(address);
            addressComplete.Number = passenger.Number;
            auxiliaryPassenger.Address = addressComplete;  

            return _passengerRepository.UpdatePassenger(auxiliaryPassenger, CPF);
        }
        public ActionResult<Passenger> UpdateStatus(string CPF)
        {
            var resultNoRestrit = _passengerRepository.GetPassengerByCPF(CPF);
            var resultRestrit = _passengerRepository.GetRestritPassenger();
            Passenger restritPassenger = new Passenger();

            foreach(Passenger passenger in resultRestrit)
            {
                if(passenger.CPF == CPF)
                {
                    restritPassenger = passenger;
                }
            }
            
            if(resultNoRestrit == null && restritPassenger == null)
            {
                return new BadRequestObjectResult("Passegeiro não encontrado !");
            }
            if(resultNoRestrit != null)
            {
                return _passengerRepository.RestritPassenger(CPF);
            }
            else
            {
                return _passengerRepository.NoRestritPassenger(CPF);
            }

        }

        private static bool validateCPF(string sourceCPF)
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
            int totalDigitI = 0;
            int totalDigitII = 0;
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
                totalDigitI += cpfArray[posicao] * (10 - posicao);
                totalDigitII += cpfArray[posicao] * (11 - posicao);
            }

            modI = totalDigitI % 11;
            if (modI < 2) { modI = 0; }
            else { modI = 11 - modI; }

            if (cpfArray[9] != modI)
            {
                return false;
            }

            totalDigitII += modI * 2;

            modII = totalDigitII % 11;
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

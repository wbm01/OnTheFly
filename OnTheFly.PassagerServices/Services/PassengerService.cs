﻿using Microsoft.AspNetCore.Mvc;
using Models;
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
            return _passengerRepository.GetPassenger();
        }
        public ActionResult<Passenger> GetPassengerByCPF(string CPF)
        {
            if (!ValidarCPF(CPF))
                return new BadRequestResult();

            return _passengerRepository.GetPassengerByCPF(CPF);
        }
        public ActionResult<Passenger> PostPassenger(Passenger passenger)
        {
            if (!ValidarCPF(passenger.CPF))
                return new BadRequestResult();
            
            Address address = PostOfficeService.GetAddress(passenger.Address.CEP).Result;
            address.Number = passenger.Address.Number;
            passenger.Address = address;

            return _passengerRepository.PostPassenger(passenger);
        }
        public ActionResult<Passenger> UpdatePassenger(Passenger passenger, string CPF)
        {
            if (!ValidarCPF(CPF))
                return new BadRequestResult();

            return _passengerRepository.UpdatePassenger(passenger, CPF);    
        }

        public static bool ValidarCPF(string sourceCPF)
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

    }


}

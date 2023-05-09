using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class CreateSaleDTO
    {
        public string iata;
        public string rab;
        public string departure;
        public List<string> passengersCPFlist;
    }
}

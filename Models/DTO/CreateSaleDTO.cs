using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class CreateSaleDTO
    {
        public string Iata { get; set; }
        public string Rab { get; set; }
        public string Departure { get; set; }
        public List<string> passengersCPFlist { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class SaleDTO
    {
        public string IATA { get; set; }
        public string RAB { get; set; }
        public DateTime Departure { get; set; }
    }
}

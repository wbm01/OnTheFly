using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class GetFlightDTO
    {
        public string iata{ get; set; }
        public string rab { get; set; }
        public string schedule { get; set; }
    }
}

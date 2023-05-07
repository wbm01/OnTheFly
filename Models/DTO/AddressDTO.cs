using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Models.DTO
{
    public class AddressDTO
    {
        public string ZipCode { get; set; }
        public string? Street { get; set; }
        public int Number { get; set; }
        public string? Complement { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}

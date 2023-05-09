using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class UpdatePassengerDTO
    {
        public string Name { get; set; }
        public char Gender { get; set; }
        public string? Phone { get; set; }
        public string DtBirth { get; set; }
        public bool? Status { get; set; }
        public string ZipCode { get; set; }
        public int Number { get; set; }
    }
}

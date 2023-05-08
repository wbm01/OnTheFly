using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class CompanyDTO
    {
        public string Name { get; set; }
        public string NameOpt { get; set; }
        public bool? Status { get; set; }
        public string Cep { get; set; }
        public int Number { get; set; }
    }
}

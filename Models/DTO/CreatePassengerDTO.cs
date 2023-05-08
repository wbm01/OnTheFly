using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Models.DTO
{
    public class CreatePassengerDTO
    {

            public string CPF { get; set; }
            public string Name { get; set; }
            public char Gender { get; set; }
            public string? Phone { get; set; }
            public string DtBirth { get; set; }
            public bool? Status { get; set; }
            public string ZipCode { get; set;}
            public int Number { get; set; }
          
        
    }
}

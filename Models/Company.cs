using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Models.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Company
    {
        [BsonId]
        public string CNPJ { get; set; }
        public string Name { get; set; }
        public string NameOpt { get; set; }
        public DateTime DtOpen { get; set; }
        public bool? Status { get; set; }
        public Address Address { get; set; }

        public Company(CompanyDTO dto)
        {
            this.NameOpt = dto.NameOpt;
            this.Status = dto.Status;
            this.Address.ZipCode = dto.ZipCode;
            this.Address.Number = dto.Number;
        }

        public Company()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DTO;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Models
{
    public class Address
    {

        public string ZipCode { get; set; }
        public string? Street { get; set; }
        public int Number { get; set; }
        public string? Complement { get; set; }
        public string City { get; set; }
        public string State { get; set; }


        public Address(AddressDTO addressDTO)
        {
            this.ZipCode = addressDTO.CEP;
            this.Street = addressDTO.Logradouro;
            this.Complement = addressDTO.Complemento;
            this.City = addressDTO.City;
            this.State = addressDTO.State;

        }

        public Address()
        {

        }
    }
}

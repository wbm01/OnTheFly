using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DTO;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Address
    {
        [BsonId]
        public string ZipCode { get; set; }
        public string? Street { get; set; }
        public int Number { get; set; }
        public string? Complement { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public Address(AddressDTO addressDTO)
        {
            this.ZipCode = addressDTO.ZipCode;
            this.Street = addressDTO.Street;
            this.Complement = addressDTO.Complement;
            this.City = addressDTO.City;
            this.State = addressDTO.State;

        }

        public Address()
        {

        }
    }
}

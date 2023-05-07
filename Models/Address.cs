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
        public int Id { get; set; }
        [JsonProperty("pais")]
        public string? Country { get; set; }
        [JsonProperty("cep")]
        public string CEP { get; set; }
        [JsonProperty("bairro")]
        public string Bairro { get; set; }
        [JsonProperty("localidade")]
        public string City { get; set; }
        [JsonProperty("uf")]
        public string State { get; set; }
        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }
        [JsonProperty("gia")]
        public int Number { get; set; }
        [JsonProperty("complemento")]
        public string Complemento { get; set; }
        public Address(AddressDTO addressDTO)
        {
            this.CEP = addressDTO.ZipCode;
            this.Logradouro = addressDTO.Street;
            this.Complemento = addressDTO.Complement;
            this.City = addressDTO.City;
            this.State = addressDTO.State;
        }
        public Address()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Models.DTO
{
    public class AirportDTO
    {
        public string IATA { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public string Country { get; set; }


    }
}

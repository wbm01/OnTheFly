﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Models.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Models
{
    //[JsonObject(MemberSerialization.OptIn)]
    public class Airport
    {
        [BsonId]
        //[JsonPropertyName("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [JsonPropertyName("iata")]
        [BsonRepresentation(BsonType.String)]
        public string iata { get; set; }
        [JsonPropertyName("time_zone_id")]
        public string time_zone_id { get; set; }
        [JsonPropertyName("name")]
        public string name { get; set; }
        [JsonPropertyName("city_code")]
        public string city_code { get; set; }
        [JsonPropertyName("country_id")]
        public string country_id { get; set; }
        [JsonPropertyName("location")]
        public string location { get; set; }
        [JsonPropertyName("elevation")]
        public string elevation { get; set; }
        [JsonPropertyName("icao")]
        public string icao { get; set; }
        [JsonPropertyName("city")]
        public string city { get; set; }
        [JsonPropertyName("state")]
        public string? state { get; set; }

        public Airport(AirportDTO airportDTO)
        {
            this.iata = airportDTO.IATA;
            this.state = airportDTO.State;
            this.city = airportDTO.City;
            this.country_id = airportDTO.Country;

        }

        public Airport()
        {

        }
    }
}


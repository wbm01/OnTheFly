using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Flight
    {
        [BsonId]
        public Airport Destiny { get; set; }
        public AirCraft Plane { get; set; }
        public int Sale { get; set; }
        public DateTime Departure { get; set; }
        public bool Status { get; set; }
    }
}

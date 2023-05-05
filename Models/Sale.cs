using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Sale
    {
        [BsonId]
        public Flight Flight { get; set; }
        public Passenger Passenger { get; set; }
        public bool Reserved { get; set; }
        public bool Sold { get; set; }
    }
}

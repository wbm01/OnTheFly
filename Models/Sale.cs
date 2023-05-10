using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Sale
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string?  id { get; set; }
        public Flight Flight { get; set; }
        public List<Passenger> Passenger { get; set; }
        public bool Reserved { get; set; }
        public bool Sold { get; set; }

        public Sale() { }
    }
}

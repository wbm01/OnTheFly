using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Passenger
    {
        [BsonId]
        public string CPF { get; set; }
        public string Name { get; set; }
        public char Gender { get; set; }
        public string? Phone { get; set; }
        public DateTime DtBirth { get; set; }
        public DateTime DtRegister { get; set; }
        public bool? Status { get; set; }
    }
}
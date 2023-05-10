using Models.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace Models
{
    public class Passenger
    {
        [BsonId]
        public string CPF { get; set; }
        public string Name { get; set; }
        [SwaggerSchema(Format = "byte")]
        public char Gender { get; set; }
        public string? Phone { get; set; }
        public DateTime DtBirth { get; set; }
        public DateTime DtRegister { get; set; }
        public bool? Status { get; set; }
        public Address Address { get; set; }

        public Passenger(CreatePassengerDTO passengerDTO)
        {
            CPF = passengerDTO.CPF; 
            Name = passengerDTO.Name;
            Gender = passengerDTO.Gender;
            Phone = passengerDTO.Phone;
            DtRegister = DateTime.Now;
            Status = passengerDTO.Status;
        }

        public Passenger()
        {

        }
    }
}
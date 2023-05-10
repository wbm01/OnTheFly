using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace OnTheFly.SalesProducer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesReservedController : ControllerBase
    {
        private readonly ConnectionFactory _factory;
        private readonly string QUEUE_NAME = "reserved";

        public SalesReservedController(ConnectionFactory factory)
        {
            _factory = factory;
        }

        [HttpPost(Name = "ReservedProducer")]
        public IActionResult PostSoldMQ([FromBody] Sale sale)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: QUEUE_NAME,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    var stringfieldReserved = JsonConvert.SerializeObject(sale);
                    var bytesReserved = Encoding.UTF8.GetBytes(stringfieldReserved);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QUEUE_NAME,
                        basicProperties: null,
                        body: bytesReserved
                    );
                }
            }

            return Accepted();
        }
    }
}
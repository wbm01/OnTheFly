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
    public class SalesSoldController : ControllerBase
    {
        private readonly ConnectionFactory _factory;
        private readonly string QUEUE_NAME = "sold";

        public SalesSoldController(ConnectionFactory factory)
        {
            _factory = factory;
        }

        [HttpPost(Name = "SoldProducer")]
        public IActionResult PostSoldMQ([FromBody] Sale sale)
        {
            using(var connection = _factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: QUEUE_NAME,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    var stringfieldSale = JsonConvert.SerializeObject(sale);
                    var bytesSale = Encoding.UTF8.GetBytes(stringfieldSale);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QUEUE_NAME,
                        basicProperties: null,
                        body: bytesSale
                    );
                }
            }

            return Accepted();
        }
    }
}

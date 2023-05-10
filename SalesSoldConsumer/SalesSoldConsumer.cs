using System.Text;
using Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

IMongoCollection<Sale> _saleRepository;
HttpClient _saleClient;


var client = new MongoClient("mongodb://localhost:27017");
var database = client.GetDatabase("DBSales");
_saleRepository = database.GetCollection<Sale>("Sales");
_saleClient = new();

const string QUEUE_NAME = "sold";

var factory = new ConnectionFactory() { HostName = "localhost" };

using (var connection = factory.CreateConnection())
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

        while (true)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var returnSold = Encoding.UTF8.GetString(body);
                var sold = JsonConvert.DeserializeObject<Sale>(returnSold);

                _saleRepository.InsertOne(sold);
            };

            channel.BasicConsume(
                queue: QUEUE_NAME,
                autoAck: true,
                consumer: consumer
            );

            Thread.Sleep(2000);
        }
    }
}
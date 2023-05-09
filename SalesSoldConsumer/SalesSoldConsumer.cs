using System.Text;
using Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

HttpClient _client = new();
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
                var returnCustomer = Encoding.UTF8.GetString(body);
                var customer = JsonConvert.DeserializeObject<Sale>(returnCustomer);
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
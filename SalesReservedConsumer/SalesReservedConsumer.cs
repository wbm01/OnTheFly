﻿using System.Text;
using Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

IMongoCollection<Sale> _saleRepository;
HttpClient _saleClient;


var client = new MongoClient("mongodb://localhost:27017");
var database = client.GetDatabase("DBSales");
_saleRepository = database.GetCollection<Sale>("Reserved");

_saleClient = new();
const string QUEUE_NAME = "reserved";

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
                var returnReserved = Encoding.UTF8.GetString(body);
                var reserved = JsonConvert.DeserializeObject<Sale>(returnReserved);

                _saleRepository.InsertOne(reserved);
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
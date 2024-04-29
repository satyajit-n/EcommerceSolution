﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory
{
    HostName = "localhost"
};
var connection = factory.CreateConnection();

using var channel = connection.CreateModel();
channel.QueueDeclare("customers", exclusive: false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    var body = args.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Message Recieved : {message}");
};

channel.BasicConsume(queue: "customers", autoAck: true, consumer: consumer);

Console.ReadKey();
using Core;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

    Console.WriteLine(" [*] Waiting for messages.");

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        QueueObject obj = JsonSerializer.Deserialize<QueueObject>(message);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        Console.WriteLine(" [x] Received {0}", obj.Id);
    };
    channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);

    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
}
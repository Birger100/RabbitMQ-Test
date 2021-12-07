using System;
using RabbitMQ.Client;
using System.Text;
using Core;
using System.Text.Json;

class Send
{
    public static void Main()
    {
        var factory = new ConnectionFactory()
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672"),

        };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            QueueObject obj = new QueueObject
            {
                Id = 1,
                Name = "Test Hest",
                Items = new List<ListItemObj>
                {
                    new ListItemObj{Name = "sub1"},
                    new ListItemObj {Name ="sub 2"}
                }
            };

            string message = JsonSerializer.Serialize(obj);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}
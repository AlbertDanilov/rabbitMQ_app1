using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace rabbitMQ_app1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Publisher is run");
            Console.WriteLine("");

            var counter = 0;
            do
            {
                int timeToSleep = new Random().Next(1000, 3000);
                Thread.Sleep(timeToSleep);

                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "dev-queue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = $"Message from publisher N {counter}";

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "dev-queue",
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine($"Publisher send message into Default Exchange N {counter++}");
                }
            } while (true);
        }
    }
}

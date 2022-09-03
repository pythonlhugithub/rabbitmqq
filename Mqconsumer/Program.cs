
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace consum;
internal class Program
{
    private static void Main(string[] args)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672")  //call docker container rabbitmq
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel(); //create channel

        channel.QueueDeclare("demo-queue",
        durable: true, exclusive: false, autoDelete: false, arguments: null);

        //consumer message

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received+=(sender, e)=>{
            var body=e.Body.ToArray();
            var message=Encoding.UTF8.GetString(body);
            Console.WriteLine(message);
        };

        channel.BasicConsume("demo-queue", true,consumer);
Console.ReadLine();
}
}

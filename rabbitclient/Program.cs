// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;


 


var factory=new ConnectionFactory{
    Uri=new Uri("amqp://guest:guest@localhost:5672")  //call docker container rabbitmq
};

using var connection=factory.CreateConnection();
using var channel=connection.CreateModel(); //create channel

channel.QueueDeclare("demo-queue", 
durable:true, exclusive:false, autoDelete: false, arguments: null);

var message=new {Name="producer", message="hello producer"};
var body=Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

channel.BasicPublish("","demo-queue", null, body); //pass to MQ



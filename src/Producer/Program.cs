using System;
using System.ComponentModel.Design;
using System.Text;
using RabbitMQ.Client;

namespace Producer
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("*** Producer ***");

			var factory = new ConnectionFactory()
			{
				HostName = "localhost", 
				//UserName = "Producer",
				//Password = "Pr@ducer"
			};

			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();

			channel.QueueDeclare(queue:"default", durable: false, exclusive: false, autoDelete: false, arguments: null);

			var message = "Hello world ;)";
			while (!string.IsNullOrWhiteSpace(message))
			{
				Console.WriteLine("Enter a Message: ");
				message = Console.ReadLine();

				var body = Encoding.UTF8.GetBytes(message);
				channel.BasicPublish(exchange: "", routingKey: "default", basicProperties: null, body: body);

				Console.WriteLine("Message send from [Produser] to [Consumer]: " + message);

			}
		}
	}
}

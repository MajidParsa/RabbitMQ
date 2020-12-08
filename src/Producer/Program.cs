using System;
using System.ComponentModel.Design;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;
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

			channel.QueueDeclare(queue: "messageQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

			var notification = "Hello world ;)";
			while (!string.IsNullOrWhiteSpace(notification))
			{
				Console.WriteLine("Enter a Message: ");
				notification = Console.ReadLine();
				var message = new Message()
				{
					From = "Majid@yahoo.com",
					To = "Parsa@gmail.com",
					Body = notification
				};

				var objMessage = JsonConvert.SerializeObject(message);
				var body = Encoding.UTF8.GetBytes(objMessage);
				channel.BasicPublish(exchange: "", routingKey: "messageQueue", basicProperties: null, body: body);

				Console.WriteLine("Message send from [Produser] to [Consumer]: " + 
				                  "From: " + message.From + ", To: " + message.To + ", Body: " + message.Body);

			}
		}
	}
}

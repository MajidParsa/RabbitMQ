using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("*** Consumer ***");

			var factory = new ConnectionFactory()
			{
				HostName = "localhost",
				// TODO: Implementing Authorization
				//UserName = "Producer
				//Password = "Pr@ducer"
			};

			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();

			channel.QueueDeclare(queue: "messageQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

			var consumer = new EventingBasicConsumer(channel);
			consumer.Received += Consumer_Received;

			channel.BasicConsume(queue: "messageQueue", autoAck: true, consumer: consumer);
			Console.ReadLine();
		}

		private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
		{
			var notification = Encoding.UTF8.GetString(e.Body.ToArray());
			Message message = JsonConvert.DeserializeObject<Message>(notification);
			Console.WriteLine("From: " + message.From + ", To: " + message.To + ", Body: " + message.Body);
		}
	}
}

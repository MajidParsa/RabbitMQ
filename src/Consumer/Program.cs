using System;
using System.Text;
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
				//UserName = "Producer",
				//Password = "Pr@ducer"
			};

			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();

			channel.QueueDeclare(queue: "default", durable: false, exclusive: false, autoDelete: false, arguments: null);

			var consumer = new EventingBasicConsumer(channel);
			consumer.Received += Consumer_Received;

			channel.BasicConsume(queue: "default", autoAck: true, consumer: consumer);
			Console.ReadLine();
		}

		private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
		{
			var message = Encoding.UTF8.GetString(e.Body.ToArray());
			Console.WriteLine(message);
		}
	}
}

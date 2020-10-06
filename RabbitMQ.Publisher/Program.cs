using RabbitMQ.Client;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace RabbitMQ.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {

            var factory = new ConnectionFactory();
            factory.Uri = new Uri("your cloud address");
            //factory.HostName = "localhost";

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("task_queue", durable:true, false, false, null);
                    string message = GetMessage(args);

                    for (int i = 1; i < 11; i++)
                    {
                        var bodyByte = Encoding.UTF8.GetBytes($"{message}-{i}");
                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true; // If Instance down or failover. It will stay in this channel.
                        channel.BasicPublish("", routingKey: "task_queue",properties, bodyByte);
                        Console.WriteLine("Mesajınız gönderilmiştir.");
                    }

                  
                }

                Console.WriteLine("Çıkış yapmak için tıklayınız...");
                Console.ReadLine();
            }



        }

        private static string GetMessage(string[] args)
        {

            return args[0].ToString();

        }
    }
}

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
            factory.Uri = new Uri("amqps://wnanjfbl:FjuLTzMb--fve1OuKcp3Ar02gdt-JGMw@wasp.rmq.cloudamqp.com/wnanjfbl");
            //factory.HostName = "localhost";

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("hello", false, false, false, null);
                    string message = "Hello World";
                    var bodyByte = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", routingKey: "hello", null, bodyByte);
                    Console.WriteLine("Mesajınız gönderilmiştir.");
                }

                Console.WriteLine("Çıkış yapmak için tıklayınız...");
                Console.ReadLine();
            }



        }
    }
}

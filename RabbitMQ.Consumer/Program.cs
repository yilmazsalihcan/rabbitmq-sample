using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQ.Consumer
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
                    var consumer = new EventingBasicConsumer(channel);

                    channel.BasicConsume("hello", true, consumer);

                    consumer.Received += (model, ev) =>
                    {                        
                        var message = Encoding.UTF8.GetString(ev.Body.ToArray());
                        Console.WriteLine("Mesaj alındı" + message);
                    };
                }

                Console.WriteLine("Çıkış yapmak için tıklayınız...");
                Console.ReadLine();
            }
        }

       
    }
}

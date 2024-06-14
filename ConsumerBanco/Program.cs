
using Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services;
using System.Text;
using System.Text.Json.Nodes;

const string QUEUE_NAME = "banco";

var factory = new ConnectionFactory() { HostName = "localhost" };

using (var conn = factory.CreateConnection())
{
    using (var channel = conn.CreateModel())
    {
        channel.QueueDeclare(
            queue: QUEUE_NAME,
            exclusive: false,
            autoDelete: false,
            arguments: null
            );

        while (true)
        {
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var returnBanco = Encoding.UTF8.GetString(body);
                var banco = JsonConvert.DeserializeObject<Banco>(returnBanco);
                GeralServiceMongoDb<Banco> gsmBanco = new(collectionName: "Banco");
                gsmBanco.Create(banco);
                Console.WriteLine($"{banco.CNPJ}, {banco.NomeBanco}, {banco.DataFundacao}");
            };

            channel.BasicConsume(
                consumer: consumer,
                queue: QUEUE_NAME,
                autoAck: true
                );

            Thread.Sleep(2000);
        }
    }

}
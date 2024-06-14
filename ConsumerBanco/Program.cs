
using Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

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

                Task<Banco> bancoMongo = PostApiSql(returnBanco);

                Task<Banco> bancoSql = PostApiMongo(returnBanco);

                var tasks = new List<Task> {bancoMongo, bancoSql};
                Task.WhenAll(bancoSql,  bancoMongo);

                Console.WriteLine($"{bancoSql.Result.CNPJ}, {bancoSql.Result.NomeBanco}, {bancoSql.Result.DataFundacao}");
                Console.WriteLine($"{bancoMongo.Result.CNPJ}, {bancoMongo.Result.NomeBanco} ,  {bancoMongo.Result.DataFundacao}");
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

async Task<Banco> PostApiSql(string banco)
{
    try
    {
        string url = "https://localhost:7058/api/SqlBancos";
        HttpClient client = new HttpClient();
        var content = new StringContent(banco, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
        string apiReturn = await response.Content.ReadAsStringAsync();
        Banco b = JsonConvert.DeserializeObject<Banco>(apiReturn);
        return b;

    }
    catch (Exception)
    {

        throw;
    }
}

async Task<Banco> PostApiMongo(string banco)
{
    try
    {
        string url = "https://localhost:7214/api/MongoBancos";
        HttpClient client = new HttpClient();
        var content = new StringContent(banco, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
        string apiReturn = await response.Content.ReadAsStringAsync();
        Banco b = JsonConvert.DeserializeObject<Banco>(apiReturn);
        return b;

    }
    catch (Exception)
    {

        throw;
    }
}
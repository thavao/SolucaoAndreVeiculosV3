using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Services;
using System.Text;

namespace ApiAndreVeiculos_Banco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancosController : ControllerBase
    {
        private readonly GeralServiceMongoDb<Banco> _bancoService;
        private readonly ConnectionFactory _factroy;
        private const string QUEUE_NAME = "banco";


        public BancosController(ConnectionFactory factory)
        {
            string colletionName = "Banco";
            _bancoService = new GeralServiceMongoDb<Banco>(colletionName);
            _factroy = factory;
        }

        [HttpGet]
        public ActionResult<List<Banco>> Get()
        {
            return _bancoService.Get();
        }
        [HttpGet("{CNPJ}")]
        public ActionResult<Banco> Get(string CNPJ)
        {
            return _bancoService.Get(CNPJ);
        }
        [HttpPut]
        public Banco Put(Banco banco)
        {
            return _bancoService.Update(banco, banco.CNPJ);
        }
        [HttpPost]
        public IActionResult PostMQBanco([FromBody] Banco banco)
        {
            using (var conn = _factroy.CreateConnection())
            {
                using (var channel = conn.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: QUEUE_NAME,
                        durable: false,
                        exclusive: false,
                        autoDelete: false);

                    var stringBanco = JsonConvert.SerializeObject(banco);
                    var bytesBanco = Encoding.UTF8.GetBytes(stringBanco);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QUEUE_NAME,
                        basicProperties: null,
                        body: bytesBanco
                        );
                };
            }
            return Accepted();
        }

        [HttpDelete]
        public ActionResult<Banco> Delete(string CNPJ)
        {
            if (_bancoService.Remove(CNPJ))
                return NoContent();
            return NotFound();
        }
    }
}

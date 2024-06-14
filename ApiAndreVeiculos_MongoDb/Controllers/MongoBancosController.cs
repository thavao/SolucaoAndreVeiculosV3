using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace ApiAndreVeiculos_MongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoBancosController : ControllerBase
    {
        private readonly GeralServiceMongoDb<Banco> _mongoDb;
        readonly string colecao = "Banco";
        public MongoBancosController()
        {
            _mongoDb = new GeralServiceMongoDb<Banco>(colecao);
        }

        [HttpPost]
        public async Task<Banco> Post(Banco banco)
        {
            return _mongoDb.Create(banco);
        }
    }
}

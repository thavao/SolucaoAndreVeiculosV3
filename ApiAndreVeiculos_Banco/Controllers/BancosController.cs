using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;

namespace ApiAndreVeiculos_Banco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancosController : ControllerBase
    {
        private readonly GeralServiceMongoDb<Banco> _bancoService;

        public BancosController()
        {
            string colletionName = "Banco";
            _bancoService = new GeralServiceMongoDb<Banco>(colletionName);
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
        public Banco Post(Banco banco)
        {
            return _bancoService.Create(banco);
        }

        [HttpDelete]
        public ActionResult<Banco> Delete(string CNPJ)
        {
            if(_bancoService.Remove(CNPJ))
                return NoContent();
            return NotFound();
        }
    }
}

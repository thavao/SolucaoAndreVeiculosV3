using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAndreVeiculos_SQLServer.Data;
using Models;

namespace ApiAndreVeiculos_SQLServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlBancosController : ControllerBase
    {
        private readonly ApiAndreVeiculos_SQLServerContext _context;

        public SqlBancosController(ApiAndreVeiculos_SQLServerContext context)
        {
            _context = context;
        }

        // POST: api/SqlBancos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Banco> PostBanco(Banco banco)
        {
            if (_context.Banco == null)
            {
                return null;//Problem("Entity set 'ApiAndreVeiculos_SQLServerContext.Banco'  is null.");
            }
            _context.Banco.Add(banco);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BancoExists(banco.CNPJ))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return banco;
        }

        private bool BancoExists(string id)
        {
            return (_context.Banco?.Any(e => e.CNPJ == id)).GetValueOrDefault();
        }
    }
}

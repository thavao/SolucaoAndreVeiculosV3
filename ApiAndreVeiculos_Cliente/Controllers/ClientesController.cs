using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAndreVeiculos_Cliente.Data;
using Models;
using Models.DTO;

namespace ApiAndreVeiculos_Cliente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ApiAndreVeiculos_ClienteContext _context;
        private readonly HttpClient _httpClient;

        public ClientesController(ApiAndreVeiculos_ClienteContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetCliente()
        {
            if (_context.Cliente == null)
            {
                return NotFound();
            }
            var clientes = await _context.Cliente.ToListAsync();
            foreach (Cliente cliente in clientes)
            {
                Endereco endereco = await _context.Endereco.Where(e => cliente.CEP == e.CEP).FirstAsync();
                cliente.Endereco = endereco;
            }
            return clientes;
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(string id)
        {
            if (_context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            Endereco endereco = await _context.Endereco.Where(e => cliente.CEP == e.CEP).FirstAsync();
            cliente.Endereco = endereco;
            //await cliente.ConstruirEndereco(_httpClient);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{documento}")]
        public async Task<IActionResult> PutCliente(string documento, ClienteDTO clienteDTO)
        {
            if (documento != clienteDTO.Documento)
            {
                return BadRequest();
            }
            Cliente cliente = new(clienteDTO);
            cliente.Endereco = await _context.Endereco.Where(e => cliente.CEP == e.CEP).FirstAsync();

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(documento))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(ClienteDTO clienteDTO)
        {
            if (_context.Cliente == null)
            {
                return Problem("Entity set 'ApiAndreVeiculos_ClienteContext.Cliente'  is null.");
            }
            Cliente cliente = new Cliente(clienteDTO);
            Endereco endereco = new(_httpClient, cliente.CEP);
            cliente.Endereco = endereco;
            _context.Cliente.Add(cliente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClienteExists(cliente.Documento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCliente", new { id = cliente.Documento }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{documento}")]
        public async Task<IActionResult> DeleteCliente(string documento)
        {
            if (_context.Cliente == null)
            {
                return NotFound();
            }
            var cliente = await _context.Cliente.FindAsync(documento);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(string documento)
        {
            return (_context.Cliente?.Any(e => e.Documento == documento)).GetValueOrDefault();
        }
    }
}

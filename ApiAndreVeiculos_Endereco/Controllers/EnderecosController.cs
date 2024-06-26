﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAndreVeiculos_Endereco.Data;
using ApiAndreVeiculos_Endereco;
using Models;

namespace ApiAndreVeiculos_Endereco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecosController : ControllerBase
    {
        private readonly ApiAndreVeiculos_EnderecoContext _context;
        private readonly HttpClient _httpClient;
		private readonly EnderecoService _enderecoService;


		public EnderecosController(ApiAndreVeiculos_EnderecoContext context, HttpClient httpClient, EnderecoService enderecoService)
        {
            _context = context;
            _httpClient = httpClient;
			_enderecoService = enderecoService;

		}

        // GET: api/Enderecos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Endereco>>> GetEndereco()
        {
            if (_context.Endereco == null)
            {
                return NotFound();
            }
            return await _context.Endereco.ToListAsync();
        }

        // GET: api/Enderecos/5
        [HttpGet("{cep}")]
        public async Task<ActionResult<Endereco>> GetEndereco(string cep)
        {
            if (_context.Endereco == null)
            {
                return NotFound();
            }

            var endereco = await _context.Endereco.FindAsync(cep);

            if (endereco == null)
            {
                return NotFound();
            }

            return endereco;
        }

        [HttpGet("viaCep/{cep}")]
        public ActionResult<Endereco> GetEnderecoViaCep(string cep)
        {
            if (_context.Endereco == null)
            {
                return NotFound();
            }

            Endereco endereco = new(_httpClient, cep);

            if (endereco == null)
            {
                return NotFound();
            }

            return endereco;
        }

        // PUT: api/Enderecos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{cep}")]
        public async Task<IActionResult> PutEndereco(string cep, Endereco endereco)
        {
            if (cep != endereco.CEP)
            {
                return BadRequest();
            }

            _context.Entry(endereco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnderecoExists(cep))
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

        // POST: api/Enderecos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Endereco>> PostEndereco(Endereco endereco)
        {
            if (_context.Endereco == null)
            {
                return Problem("Entity set 'ApiAndreVeiculos_EnderecoContext.Endereco'  is null.");
            }
            _context.Endereco.Add(endereco);
            try
            {
                await _context.SaveChangesAsync();
                _enderecoService.InserirUm(endereco);
            }
            catch (DbUpdateException)
            {
                if (EnderecoExists(endereco.CEP))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEndereco", new { id = endereco.CEP }, endereco);
        }

        [HttpPost("/ViaCep/{cep}")]
        public async Task<ActionResult<Endereco>> PostEndereco(string cep)
        {
            if (_context.Endereco == null)
            {
                return Problem("Entity set 'ApiAndreVeiculos_EnderecoContext.Endereco'  is null.");
            }
            Endereco endereco = new Endereco(_httpClient, cep);
            _context.Endereco.Add(endereco);
            try
            {
                await _context.SaveChangesAsync();
				_enderecoService.InserirUm(endereco);

			}
			catch (DbUpdateException)
            {
                if (EnderecoExists(endereco.CEP))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEndereco", new { id = endereco.CEP }, endereco);
        }

        // DELETE: api/Enderecos/5
        [HttpDelete("{cep}")]
        public async Task<IActionResult> DeleteEndereco(string cep)
        {
            if (_context.Endereco == null)
            {
                return NotFound();
            }
            var endereco = await _context.Endereco.FindAsync(cep);
            if (endereco == null)
            {
                return NotFound();
            }

            _context.Endereco.Remove(endereco);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnderecoExists(string id)
        {
            return (_context.Endereco?.Any(e => e.CEP == id)).GetValueOrDefault();
        }
    }
}

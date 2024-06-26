﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAndreVeiculos_Funcionario.Data;
using Models;
using Models.DTO;

namespace ApiAndreVeiculos_Funcionario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly ApiAndreVeiculos_FuncionarioContext _context;

        public FuncionariosController(ApiAndreVeiculos_FuncionarioContext context)
        {
            _context = context;
        }

        // GET: api/Funcionarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionario()
        {
            if (_context.Funcionario == null)
            {
                return NotFound();
            }
            var funcionarios =  _context.Funcionario.
                Include(f => f.Endereco).ToList();


            foreach (var f in funcionarios)
            {
                if (f.CEP == "" || f.CEP == null)
                    continue;

                //f.Endereco = await _context.Endereco.FindAsync(f.CEP);
            }

            return funcionarios;
        }

        // GET: api/Funcionarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionario>> GetFuncionario(string id)
        {
            if (_context.Funcionario == null)
            {
                return NotFound();
            }
            var funcionario = await _context.Funcionario.FindAsync(id);

            if (funcionario == null)
            {
                return NotFound();
            }
            funcionario.Endereco = await _context.Endereco.FindAsync(funcionario.CEP);



            return funcionario;
        }

        // PUT: api/Funcionarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuncionario(string id, FuncionarioDTO funcionarioDTO)
        {
            if (id != funcionarioDTO.Documento)
            {
                return BadRequest();
            }
            _context.Entry(funcionarioDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(id))
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

        // POST: api/Funcionarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Funcionario>> PostFuncionario(FuncionarioDTO funcionarioDTO)
        {
            if (_context.Funcionario == null)
            {
                return Problem("Entity set 'ApiAndreVeiculos_FuncionarioContext.Funcionario'  is null.");
            }
            Funcionario funcionario = new(funcionarioDTO);
            funcionario.Endereco = await _context.Endereco.FindAsync(funcionario.CEP);
            _context.Funcionario.Add(funcionario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FuncionarioExists(funcionario.Documento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFuncionario", new { id = funcionario.Documento }, funcionario);
        }

        // DELETE: api/Funcionarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuncionario(string id)
        {
            if (_context.Funcionario == null)
            {
                return NotFound();
            }
            var funcionario = await _context.Funcionario.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            _context.Funcionario.Remove(funcionario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FuncionarioExists(string id)
        {
            return (_context.Funcionario?.Any(e => e.Documento == id)).GetValueOrDefault();
        }
    }
}

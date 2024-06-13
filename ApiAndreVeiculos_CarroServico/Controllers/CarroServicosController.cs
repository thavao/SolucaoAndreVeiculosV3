using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAndreVeiculos_CarroServico.Data;
using Models;
using Models.DTO;

namespace ApiAndreVeiculos_CarroServico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarroServicosController : ControllerBase
    {
        private readonly ApiAndreVeiculos_CarroServicoContext _context;

        public CarroServicosController(ApiAndreVeiculos_CarroServicoContext context)
        {
            _context = context;
        }

        // GET: api/CarroServicos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarroServico>>> GetCarroServico()
        {
            if (_context.CarroServico == null)
            {
                return NotFound();
            }
            var carrosServicos = await _context.CarroServico
                .Include(cs => cs.Carro)
                .Include(cs => cs.Servico).ToListAsync();
            return carrosServicos;
        }

        // GET: api/CarroServicos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarroServico>> GetCarroServico(int id)
        {
            if (_context.CarroServico == null)
            {
                return NotFound();
            }
            var carroServico = await _context.CarroServico.FindAsync(id);

            if (carroServico == null)
            {
                return NotFound();
            }

            return carroServico;
        }

        // PUT: api/CarroServicos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarroServico(int id, CarroServicoDTO carroServicoDTO)
        {
            if (id != carroServicoDTO.Id)
            {
                return BadRequest();
            }

            CarroServico carroServico = new(carroServicoDTO);
            carroServico.Carro = await _context.Carro.FindAsync(carroServicoDTO.CarroPlaca);
            carroServico.Servico = await _context.Servico.FindAsync(carroServicoDTO.ServicoId);

            _context.Entry(carroServicoDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarroServicoExists(id))
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

        // POST: api/CarroServicos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarroServico>> PostCarroServico(CarroServicoDTO carroServicoDTO)
        {
            if (_context.CarroServico == null)
            {
                return Problem("Entity set 'ApiAndreVeiculos_CarroServicoContext.CarroServico'  is null.");
            }
            try
            {
                CarroServico carroServico = new(carroServicoDTO);
                carroServico.Carro = await _context.Carro.FindAsync(carroServicoDTO.CarroPlaca);
                carroServico.Servico = await _context.Servico.FindAsync(carroServicoDTO.ServicoId);



                _context.CarroServico.Add(carroServico);
                await _context.SaveChangesAsync();

                return carroServico;
            }
            catch (Exception)
            {

                throw;
            }

        }

        // DELETE: api/CarroServicos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarroServico(int id)
        {
            if (_context.CarroServico == null)
            {
                return NotFound();
            }
            var carroServico = await _context.CarroServico.FindAsync(id);
            if (carroServico == null)
            {
                return NotFound();
            }

            _context.CarroServico.Remove(carroServico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarroServicoExists(int id)
        {
            return (_context.CarroServico?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

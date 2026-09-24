using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAutos.Data;
using ProyectoAutos.Models;

namespace ProyectoAutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargarAutos : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CargarAutos(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CargarAutos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CargaAuto>>> GetCargaAuto()
        {
            return await _context.CargaAuto.ToListAsync();
        }

        // GET: api/CargarAutos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CargaAuto>> GetCargaAuto(int id)
        {
            var cargaAuto = await _context.CargaAuto.FindAsync(id);

            if (cargaAuto == null)
            {
                return NotFound();
            }

            return cargaAuto;
        }

        // PUT: api/CargarAutos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCargaAuto(int id, CargaAuto cargaAuto)
        {
            if (id != cargaAuto.AutoId)
            {
                return BadRequest();
            }

            _context.Entry(cargaAuto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CargaAutoExists(id))
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

        // POST: api/CargarAutos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CargaAuto>> PostCargaAuto(CargaAuto cargaAuto)
        {
            _context.CargaAuto.Add(cargaAuto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCargaAuto", new { id = cargaAuto.AutoId }, cargaAuto);
        }

        // DELETE: api/CargarAutos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCargaAuto(int id)
        {
            var cargaAuto = await _context.CargaAuto.FindAsync(id);
            if (cargaAuto == null)
            {
                return NotFound();
            }

            _context.CargaAuto.Remove(cargaAuto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CargaAutoExists(int id)
        {
            return _context.CargaAuto.Any(e => e.AutoId == id);
        }
    }
}

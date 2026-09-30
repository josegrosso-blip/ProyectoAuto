using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAutos.Data;
    using ProyectoAuto.Models;

namespace ProyectoAutos.Controllers
{
    [Route("api/CargarAutos")]
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
            if (id != cargaAuto.AutosId)
            {
                return BadRequest();
            }

            if (!DatosValidos(cargaAuto))
            {
                return BadRequest("Completa todos los campos. El modelo debe tener al menos 2 caracteres y la patente entre 7 y 9 caracteres.");
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
            if (!DatosValidos(cargaAuto))
            {
                return BadRequest("Completa todos los campos. El modelo debe tener al menos 2 caracteres y la patente entre 7 y 9 caracteres.");
            }

            _context.CargaAuto.Add(cargaAuto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCargaAuto", new { id = cargaAuto.AutosId }, cargaAuto);
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

            if (cargaAuto.Disponibilidad != false)
            {
                return BadRequest("Solo se puede eliminar un auto cuando está No disponible.");
            }

            _context.CargaAuto.Remove(cargaAuto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CargaAutoExists(int id)
        {
            return _context.CargaAuto.Any(e => e.AutosId == id);
        }

        private bool DatosValidos(CargaAuto auto)
        {
            return !string.IsNullOrWhiteSpace(auto.Marca)
                && !string.IsNullOrWhiteSpace(auto.Modelo)
                && auto.Modelo.Trim().Length >= 2
                && auto.Año >= 2000
                && auto.Año <= 2050
                && !string.IsNullOrWhiteSpace(auto.Patente)
                && auto.Patente.Trim().Length >= 7
                && auto.Patente.Trim().Length <= 9
                && auto.Kms >= 0
                && auto.FechaDeIngreso != default
                && auto.Disponibilidad.HasValue;
        }
    }
}

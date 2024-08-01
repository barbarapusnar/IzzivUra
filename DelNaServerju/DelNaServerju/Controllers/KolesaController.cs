using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DelNaServerju;

namespace DelNaServerju.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KolesaController : ControllerBase
    {
        private readonly PodatkiDb _context;

        public KolesaController(PodatkiDb context)
        {
            _context = context;
        }

        // GET: api/Kolesa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kolo>>> GetKolo()
        {
            return await _context.Kolo.ToListAsync();
        }

        // GET: api/Kolesa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kolo>> GetKolo(int id)
        {
            var kolo = await _context.Kolo.FindAsync(id);

            if (kolo == null)
            {
                return NotFound();
            }

            return kolo;
        }

        // PUT: api/Kolesa/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKolo(int id, Kolo kolo)
        {
            if (id != kolo.Id)
            {
                return BadRequest();
            }
            _context.Entry(kolo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KoloExists(id))
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

        // POST: api/Kolesa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Kolo>> PostKolo(Kolo kolo)
        {
            if (kolo != null)
            { 
                int x=kolo.Lastnik.Id;
                kolo.Lastnik = _context.Uporabniki.Where(e => e.Id == x).FirstOrDefault();
            }
            _context.Kolo.Add(kolo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKolo", new { id = kolo.Id }, kolo);
        }

        // DELETE: api/Kolesa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKolo(int id)
        {
            var kolo = await _context.Kolo.FindAsync(id);
            if (kolo == null)
            {
                return NotFound();
            }

            _context.Kolo.Remove(kolo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KoloExists(int id)
        {
            return _context.Kolo.Any(e => e.Id == id);
        }
    }
}

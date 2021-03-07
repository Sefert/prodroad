using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplysController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SupplysController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Supplys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supply>>> GetSupplys()
        {
            return await _context.Supplys.ToListAsync();
        }

        // GET: api/Supplys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supply>> GetSupply(Guid id)
        {
            var supply = await _context.Supplys.FindAsync(id);

            if (supply == null)
            {
                return NotFound();
            }

            return supply;
        }

        // PUT: api/Supplys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupply(Guid id, Supply supply)
        {
            if (id != supply.Id)
            {
                return BadRequest();
            }

            _context.Entry(supply).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplyExists(id))
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

        // POST: api/Supplys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Supply>> PostSupply(Supply supply)
        {
            _context.Supplys.Add(supply);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupply", new { id = supply.Id }, supply);
        }

        // DELETE: api/Supplys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupply(Guid id)
        {
            var supply = await _context.Supplys.FindAsync(id);
            if (supply == null)
            {
                return NotFound();
            }

            _context.Supplys.Remove(supply);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupplyExists(Guid id)
        {
            return _context.Supplys.Any(e => e.Id == id);
        }
    }
}

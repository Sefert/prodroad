#nullable disable
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
    public class PriceGroupController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PriceGroupController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PriceGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriceGroup>>> GetPriceGroups()
        {
            return await _context.PriceGroups.ToListAsync();
        }

        // GET: api/PriceGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PriceGroup>> GetPriceGroup(Guid id)
        {
            var priceGroup = await _context.PriceGroups.FindAsync(id);

            if (priceGroup == null)
            {
                return NotFound();
            }

            return priceGroup;
        }

        // PUT: api/PriceGroup/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPriceGroup(Guid id, PriceGroup priceGroup)
        {
            if (id != priceGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(priceGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceGroupExists(id))
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

        // POST: api/PriceGroup
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PriceGroup>> PostPriceGroup(PriceGroup priceGroup)
        {
            _context.PriceGroups.Add(priceGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPriceGroup", new { id = priceGroup.Id }, priceGroup);
        }

        // DELETE: api/PriceGroup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriceGroup(Guid id)
        {
            var priceGroup = await _context.PriceGroups.FindAsync(id);
            if (priceGroup == null)
            {
                return NotFound();
            }

            _context.PriceGroups.Remove(priceGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PriceGroupExists(Guid id)
        {
            return _context.PriceGroups.Any(e => e.Id == id);
        }
    }
}

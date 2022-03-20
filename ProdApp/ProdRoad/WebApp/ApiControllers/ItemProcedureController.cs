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
    public class ItemProcedureController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemProcedureController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ItemProcedure
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemProcedure>>> GetItemProcedures()
        {
            return await _context.ItemProcedures.ToListAsync();
        }

        // GET: api/ItemProcedure/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemProcedure>> GetItemProcedure(Guid id)
        {
            var itemProcedure = await _context.ItemProcedures.FindAsync(id);

            if (itemProcedure == null)
            {
                return NotFound();
            }

            return itemProcedure;
        }

        // PUT: api/ItemProcedure/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemProcedure(Guid id, ItemProcedure itemProcedure)
        {
            if (id != itemProcedure.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemProcedure).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemProcedureExists(id))
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

        // POST: api/ItemProcedure
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemProcedure>> PostItemProcedure(ItemProcedure itemProcedure)
        {
            _context.ItemProcedures.Add(itemProcedure);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemProcedure", new { id = itemProcedure.Id }, itemProcedure);
        }

        // DELETE: api/ItemProcedure/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemProcedure(Guid id)
        {
            var itemProcedure = await _context.ItemProcedures.FindAsync(id);
            if (itemProcedure == null)
            {
                return NotFound();
            }

            _context.ItemProcedures.Remove(itemProcedure);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemProcedureExists(Guid id)
        {
            return _context.ItemProcedures.Any(e => e.Id == id);
        }
    }
}

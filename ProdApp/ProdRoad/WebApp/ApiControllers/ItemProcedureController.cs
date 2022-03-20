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
using WebApp.DTO;

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
        public async Task<ActionResult<IEnumerable<ItemProcedureDTO>>> GetItemProcedures()
        {
            var dataList = (await _context.ItemProcedures
                    .ToListAsync())
                .Select(row => new ItemProcedureDTO()
                {
                    ProcedureId = row.ProcedureId,
                    ItemId = row.ItemId,
                    CreatedUsed= row.CreatedUsed,
                    Quantity = row.Quantity
                })
                .ToList();
            return dataList;
        }

        // GET: api/ItemProcedure/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemProcedureDTO>> GetItemProcedure(Guid id)
        {
            var dbRow = await _context.ItemProcedures.FindAsync(id);
            
            if (dbRow == null)
            {
                return NotFound();
            }
            
            var itemProcedure = new ItemProcedureDTO()
            {
                Id = dbRow.Id,
                ProcedureId = dbRow.ProcedureId,
                ItemId = dbRow.ItemId,
                CreatedUsed= dbRow.CreatedUsed,
                Quantity = dbRow.Quantity
            };
            
            return itemProcedure;
        }

        // PUT: api/ItemProcedure/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemProcedure(Guid id, ItemProcedureDTO itemProcedure)
        {
            if (id != itemProcedure.Id)
            {
                return BadRequest();
            }

            var dbRow = await _context.ItemProcedures.FindAsync(id);
            
            if (dbRow == null) {return NotFound();}

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
        public async Task<ActionResult<ItemProcedureDTO>> PostItemProcedure(ItemProcedureDTO itemProcedure)
        {
            var dbRow = new ItemProcedure()
            {
                ProcedureId = itemProcedure.ProcedureId,
                ItemId = itemProcedure.ItemId,
                CreatedUsed= itemProcedure.CreatedUsed,
                Quantity = itemProcedure.Quantity
            };     
            
            _context.ItemProcedures.Add(dbRow);
            
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

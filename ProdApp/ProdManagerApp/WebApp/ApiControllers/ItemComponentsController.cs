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
    public class ItemComponentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemComponentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ItemComponents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemComponent>>> GetItemComponents()
        {
            return await _context.ItemComponents.ToListAsync();
        }

        // GET: api/ItemComponents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemComponent>> GetItemComponent(Guid id)
        {
            var itemComponent = await _context.ItemComponents.FindAsync(id);

            if (itemComponent == null)
            {
                return NotFound();
            }

            return itemComponent;
        }

        // PUT: api/ItemComponents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemComponent(Guid id, ItemComponent itemComponent)
        {
            if (id != itemComponent.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemComponent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemComponentExists(id))
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

        // POST: api/ItemComponents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemComponent>> PostItemComponent(ItemComponent itemComponent)
        {
            _context.ItemComponents.Add(itemComponent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemComponent", new { id = itemComponent.Id }, itemComponent);
        }

        // DELETE: api/ItemComponents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemComponent(Guid id)
        {
            var itemComponent = await _context.ItemComponents.FindAsync(id);
            if (itemComponent == null)
            {
                return NotFound();
            }

            _context.ItemComponents.Remove(itemComponent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemComponentExists(Guid id)
        {
            return _context.ItemComponents.Any(e => e.Id == id);
        }
    }
}

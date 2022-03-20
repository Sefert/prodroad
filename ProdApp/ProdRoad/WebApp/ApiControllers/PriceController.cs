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
    public class PriceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PriceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Price
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriceDTO>>> GetPrices()
        {
            var dataList = (await _context.Prices
                    .ToListAsync())
                .Select(row => new PriceDTO()
                {
                    ItemId = row.ItemId,
                    ItemWarehouseId = row.ItemWarehouseId,
                    PureCost = row.PureCost
                })
                .ToList();
            return dataList;
        }

        // GET: api/Price/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PriceDTO>> GetPrice(Guid id)
        {
            var dbRow = await _context.Prices.FindAsync(id);
            
            if (dbRow == null)
            {
                return NotFound();
            }
            
            var price = new PriceDTO()
            {
                Id = dbRow.Id,
                ItemId = dbRow.ItemId,
                ItemWarehouseId = dbRow.ItemWarehouseId,
                PureCost = dbRow.PureCost
            };

            return price;
        }

        // PUT: api/Price/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrice(Guid id, PriceDTO price)
        {
            if (id != price.Id)
            {
                return BadRequest();
            }

            var dbRow = await _context.Prices.FindAsync(id);
            
            if (dbRow == null) {return NotFound();}

            _context.Entry(price).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceExists(id))
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

        // POST: api/Price
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PriceDTO>> PostPrice(PriceDTO price)
        {
            var dbRow = new Price()
            {
                ItemId = price.ItemId,
                ItemWarehouseId = price.ItemWarehouseId,
                PureCost = price.PureCost
            };
            _context.Prices.Add(dbRow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrice", new { id = price.Id }, price);
        }

        // DELETE: api/Price/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrice(Guid id)
        {
            var price = await _context.Prices.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }

            _context.Prices.Remove(price);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PriceExists(Guid id)
        {
            return _context.Prices.Any(e => e.Id == id);
        }
    }
}

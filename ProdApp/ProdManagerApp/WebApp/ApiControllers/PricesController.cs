using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPriceRepo _repo;

        public PricesController(AppDbContext context)
        {
            _context = context;
            _repo = new PriceRepo(context);
        }

        // GET: api/Prices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Price>>> GetPrices()
        {
            return Ok(await _repo.GetAllAsync(false));
        }

        // GET: api/Prices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Price>> GetPrice(Guid id)
        {
            if (!await _repo.ExistsAsync(id))
            {
                return NotFound();
            }

            return Ok(await _repo.FirstOrDefaultAsync(id));
        }

        // PUT: api/Prices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrice(Guid id, Price price)
        {
            if (id != price.Id)
            {
                return BadRequest();
            }

            _context.Entry(price).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repo.ExistsAsync(id))
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

        // POST: api/Prices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Price>> PostPrice(Price price)
        {
            _repo.Add(price);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrice", new { id = price.Id }, price);
        }

        // DELETE: api/Prices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrice(Guid id)
        {
            if (!await _repo.ExistsAsync(id))
            {
                return NotFound();
            }
            
            var component = await _repo.FirstOrDefaultAsync(id);
            _repo.Remove(component);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

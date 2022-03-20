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
    public class CustomerPriceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerPriceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CustomerPrice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerPrice>>> GetCustomerPrices()
        {
            return await _context.CustomerPrices.ToListAsync();
        }

        // GET: api/CustomerPrice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerPrice>> GetCustomerPrice(Guid id)
        {
            var customerPrice = await _context.CustomerPrices.FindAsync(id);

            if (customerPrice == null)
            {
                return NotFound();
            }

            return customerPrice;
        }

        // PUT: api/CustomerPrice/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerPrice(Guid id, CustomerPrice customerPrice)
        {
            if (id != customerPrice.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerPrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerPriceExists(id))
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

        // POST: api/CustomerPrice
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerPrice>> PostCustomerPrice(CustomerPrice customerPrice)
        {
            _context.CustomerPrices.Add(customerPrice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerPrice", new { id = customerPrice.Id }, customerPrice);
        }

        // DELETE: api/CustomerPrice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerPrice(Guid id)
        {
            var customerPrice = await _context.CustomerPrices.FindAsync(id);
            if (customerPrice == null)
            {
                return NotFound();
            }

            _context.CustomerPrices.Remove(customerPrice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerPriceExists(Guid id)
        {
            return _context.CustomerPrices.Any(e => e.Id == id);
        }
    }
}

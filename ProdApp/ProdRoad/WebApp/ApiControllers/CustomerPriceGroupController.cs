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
    public class CustomerPriceGroupController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerPriceGroupController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CustomerPriceGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerPriceGroup>>> GetCustomerPriceGroups()
        {
            return await _context.CustomerPriceGroups.ToListAsync();
        }

        // GET: api/CustomerPriceGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerPriceGroup>> GetCustomerPriceGroup(Guid id)
        {
            var customerPriceGroup = await _context.CustomerPriceGroups.FindAsync(id);

            if (customerPriceGroup == null)
            {
                return NotFound();
            }

            return customerPriceGroup;
        }

        // PUT: api/CustomerPriceGroup/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerPriceGroup(Guid id, CustomerPriceGroup customerPriceGroup)
        {
            if (id != customerPriceGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerPriceGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerPriceGroupExists(id))
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

        // POST: api/CustomerPriceGroup
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerPriceGroup>> PostCustomerPriceGroup(CustomerPriceGroup customerPriceGroup)
        {
            _context.CustomerPriceGroups.Add(customerPriceGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerPriceGroup", new { id = customerPriceGroup.Id }, customerPriceGroup);
        }

        // DELETE: api/CustomerPriceGroup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerPriceGroup(Guid id)
        {
            var customerPriceGroup = await _context.CustomerPriceGroups.FindAsync(id);
            if (customerPriceGroup == null)
            {
                return NotFound();
            }

            _context.CustomerPriceGroups.Remove(customerPriceGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerPriceGroupExists(Guid id)
        {
            return _context.CustomerPriceGroups.Any(e => e.Id == id);
        }
    }
}

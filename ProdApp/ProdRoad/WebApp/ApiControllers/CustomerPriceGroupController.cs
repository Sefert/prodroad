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
    public class CustomerPriceGroupController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerPriceGroupController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CustomerPriceGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerPriceGroupDTO>>> GetCustomerPriceGroups()
        {
            var dataList = (await _context.CustomerPriceGroups
                    .ToListAsync())
                .Select(row => new CustomerPriceGroupDTO()
                {
                    PriceGroupId = row.PriceGroupId,
                    CustomerId = row.CustomerId,
                })
                .ToList();
            return dataList;
        }

        // GET: api/CustomerPriceGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerPriceGroupDTO>> GetCustomerPriceGroup(Guid id)
        {
            var dbRow = await _context.CustomerPriceGroups.FindAsync(id);
            
            if (dbRow == null)
            {
                return NotFound();
            }
            
            var customerPriceGroup = new CustomerPriceGroupDTO()
            {
                Id = dbRow.Id,
                PriceGroupId = dbRow.PriceGroupId,
                CustomerId = dbRow.CustomerId,
            };

            return customerPriceGroup;
        }

        // PUT: api/CustomerPriceGroup/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerPriceGroup(Guid id, CustomerPriceGroupDTO customerPriceGroup)
        {
            if (id != customerPriceGroup.Id)
            {
                return BadRequest();
            }

            var dbRow = await _context.CustomerPriceGroups.FindAsync(id);
            
            if (dbRow == null) {return NotFound();}

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
        public async Task<ActionResult<CustomerPriceGroupDTO>> PostCustomerPriceGroup(CustomerPriceGroupDTO customerPriceGroup)
        {
            var dbRow = new CustomerPriceGroup()
            {
                PriceGroupId = customerPriceGroup.PriceGroupId,
                CustomerId = customerPriceGroup.CustomerId,
            };
            
            _context.CustomerPriceGroups.Add(dbRow);
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

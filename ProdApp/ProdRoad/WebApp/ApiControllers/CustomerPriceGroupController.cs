#nullable enable
using DAL.App.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.App;
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPriceGroupController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public CustomerPriceGroupController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/CustomerPriceGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerPriceGroupDTO>>> GetCustomerPriceGroups()
        {
            var dataList = (await _uow.CustomerPriceGroups
                    .GetAllAsync())
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
            var dbRow = await _uow.CustomerPriceGroups.FirstOrDefaultAsync(id);
            
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

            var dbRow = await _uow.CustomerPriceGroups.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            _uow.CustomerPriceGroups.ModifyState(dbRow);

            try
            {
                await _uow.SaveChangesAsync();
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
            
            _uow.CustomerPriceGroups.Add(dbRow);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetCustomerPriceGroup", new { id = customerPriceGroup.Id }, customerPriceGroup);
        }

        // DELETE: api/CustomerPriceGroup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerPriceGroup(Guid id)
        {
            var customerPriceGroup = await _uow.CustomerPriceGroups.FirstOrDefaultAsync(id);
            if (customerPriceGroup == null)
            {
                return NotFound();
            }

            _uow.CustomerPriceGroups.Remove(customerPriceGroup);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerPriceGroupExists(Guid id)
        {
            return _uow.CustomerPriceGroups.Exists(id);
        }
    }
}

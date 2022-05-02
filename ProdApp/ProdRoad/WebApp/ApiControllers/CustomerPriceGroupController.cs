#nullable enable
using BLL.App.Contracts;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPriceGroupController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public CustomerPriceGroupController(IAppBLL bll)
        {
            _bll = bll;

        }

        // GET: api/CustomerPriceGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerPriceGroup>>> GetCustomerPriceGroups()
        {
            var dataList = (await _bll.CustomerPriceGroups
                    .GetAllAsync())
                .ToList();
            return dataList;
        }

        // GET: api/CustomerPriceGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerPriceGroup>> GetCustomerPriceGroup(Guid id)
        {
            var customerPriceGroup = await _bll.CustomerPriceGroups.FirstOrDefaultAsync(id);
            
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

            var dbRow = await _bll.CustomerPriceGroups.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            _bll.CustomerPriceGroups.ModifyState(dbRow);

            try
            {
                await _bll.SaveChangesAsync();
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
            var dbRow = new CustomerPriceGroup()
            {
                PriceGroupId = customerPriceGroup.PriceGroupId,
                CustomerId = customerPriceGroup.CustomerId,
            };
            
            _bll.CustomerPriceGroups.Add(dbRow);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCustomerPriceGroup", new { id = customerPriceGroup.Id }, customerPriceGroup);
        }

        // DELETE: api/CustomerPriceGroup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerPriceGroup(Guid id)
        {
            var customerPriceGroup = await _bll.CustomerPriceGroups.FirstOrDefaultAsync(id);
            if (customerPriceGroup == null)
            {
                return NotFound();
            }

            _bll.CustomerPriceGroups.Remove(customerPriceGroup);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerPriceGroupExists(Guid id)
        {
            return _bll.CustomerPriceGroups.Exists(id);
        }
    }
}

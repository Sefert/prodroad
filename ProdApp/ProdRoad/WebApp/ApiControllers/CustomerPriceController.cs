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
    public class CustomerPriceController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public CustomerPriceController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/CustomerPrice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerPriceDTO>>> GetCustomerPrices()
        {
            var dataList = (await _uow.CustomerPrices
                    .GetAllAsync())
                .Select(row => new CustomerPriceDTO()
                {
                    Id = row.Id,
                    CustomerPriceGroupId = row.CustomerPriceGroupId,
                    PriceId = row.PriceId
                })
                .ToList();
            return dataList;
        }

        // GET: api/CustomerPrice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerPriceDTO>> GetCustomerPrice(Guid id)
        {
            var dbRow = await _uow.CustomerPrices.FirstOrDefaultAsync(id);
            
            if (dbRow == null)
            {
                return NotFound();
            }
            
            var customerPrice = new CustomerPriceDTO()
            {
                Id = dbRow.Id,
                CustomerPriceGroupId = dbRow.CustomerPriceGroupId,
                PriceId = dbRow.PriceId
            };

            return customerPrice;
        }

        // PUT: api/CustomerPrice/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerPrice(Guid id, CustomerPriceDTO customerPrice)
        {
            if (id != customerPrice.Id)
            {
                return BadRequest();
            }

            var dbRow = await _uow.CustomerPrices.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            _uow.CustomerPrices.ModifyState(dbRow);

            try
            {
                await _uow.SaveChangesAsync();
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
            _uow.CustomerPrices.Add(customerPrice);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetCustomerPrice", new { id = customerPrice.Id }, customerPrice);
        }

        // DELETE: api/CustomerPrice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerPrice(Guid id)
        {
            var customerPrice = await _uow.CustomerPrices.FirstOrDefaultAsync(id);
            if (customerPrice == null)
            {
                return NotFound();
            }

            _uow.CustomerPrices.Remove(customerPrice);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerPriceExists(Guid id)
        {
            return _uow.Customers.Exists(id);
        }
    }
}

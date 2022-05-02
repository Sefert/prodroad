#nullable enable
using BLL.App.Contracts;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPriceController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public CustomerPriceController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/CustomerPrice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerPrice>>> GetCustomerPrices()
        {
            var dataList = (await _bll.CustomerPrices
                    .GetAllAsync())
                .ToList();
            return dataList;
        }

        // GET: api/CustomerPrice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerPrice>> GetCustomerPrice(Guid id)
        {
            var customerPrice = await _bll.CustomerPrices.FirstOrDefaultAsync(id);
            
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

            var dbRow = await _bll.CustomerPrices.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            _bll.CustomerPrices.ModifyState(dbRow);

            try
            {
                await _bll.SaveChangesAsync();
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
            _bll.CustomerPrices.Add(customerPrice);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCustomerPrice", new { id = customerPrice.Id }, customerPrice);
        }

        // DELETE: api/CustomerPrice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerPrice(Guid id)
        {
            var customerPrice = await _bll.CustomerPrices.FirstOrDefaultAsync(id);
            if (customerPrice == null)
            {
                return NotFound();
            }

            _bll.CustomerPrices.Remove(customerPrice);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerPriceExists(Guid id)
        {
            return _bll.Customers.Exists(id);
        }
    }
}

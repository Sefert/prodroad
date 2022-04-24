#nullable enable
using DAL.App.Contracts;
using DTO.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public CustomerController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var dataList = (await _uow.Customers
                    .GetAllAsync(User.GetUserId()))
                .ToList();
            return dataList;
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(Guid id)
        {
            var customer = await _uow.Customers.FirstOrDefaultAsync(id);
            
            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            var dbRow = await _uow.Customers.FirstOrDefaultAsync(User.GetUserId(),id);
            
            if (dbRow == null) {return NotFound();}

            dbRow.Name!.SetTranslation(customer.Name!);
            dbRow.Registration!.SetTranslation(customer.Registration!);

            _uow.Customers.ModifyState(dbRow);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            var dbRow = new Customer()
            {
                AppUserId = customer.AppUserId,
            };
            
            dbRow.Name!.SetTranslation(customer.Name!);
            dbRow.Registration!.SetTranslation(customer.Registration!);
            
            _uow.Customers.Add(dbRow);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _uow.Customers.FirstOrDefaultAsync(User.GetUserId(),id);
            if (customer == null)
            {
                return NotFound();
            }

            _uow.Customers.Remove(customer);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(Guid id)
        {
            return _uow.Customers.Exists(id);
        }
    }
}

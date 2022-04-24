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
    public class AddressController : ControllerBase
    {
        //is now in IAppUnitOfWOrk
        //private readonly IAddressRepository _repo;
        
        private readonly IAppUnitOfWork _uow;
        
        public AddressController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Address
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            var address = (await _uow.Addresses.GetAllAsync(User.GetUserId()))
                .ToList();
            return address;
        }

        // GET: api/Address/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(Guid id)
        {
            var dbAddress = await _uow.Addresses.FirstOrDefaultAsync(User.GetUserId(),id);
            
            if (dbAddress == null)
            {
                return NotFound();
            }
            return dbAddress;
        }

        // PUT: api/Address/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(Guid id, Address address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            var dbAddress = await _uow.Addresses.FirstOrDefaultAsync(User.GetUserId(),id);
            
            if (dbAddress == null) {return NotFound();}

            dbAddress.Country!.SetTranslation(address.Country!);
            dbAddress.City!.SetTranslation(address.City!);
            dbAddress.Street!.SetTranslation(address.Street!);
            dbAddress.Code!.SetTranslation(address.Code!);
            dbAddress.Phone!.SetTranslation(address.Phone!);
            dbAddress.Email!.SetTranslation(address.Email!);

            _uow.Addresses.ModifyState(dbAddress);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Address
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {

            var dbAddress = new Address()
            {
                AppUserId = User.GetUserId(),
                CustomerId = address.CustomerId
            };
            
            dbAddress.Country!.SetTranslation(address.Country!);
            dbAddress.City!.SetTranslation(address.City!);
            dbAddress.Street!.SetTranslation(address.Street!);
            dbAddress.Code!.SetTranslation(address.Code!);
            dbAddress.Phone!.SetTranslation(address.Phone!);
            dbAddress.Email!.SetTranslation(address.Email!);
            
            _uow.Addresses.Add(dbAddress);
            
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.Id }, address);
        }

        // DELETE: api/Address/5
        //TODO: implement culture deletion
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            var address = await _uow.Addresses.FirstOrDefaultAsync(User.GetUserId(),id);
            if (address == null)
            {
                return NotFound();
            }

            _uow.Addresses.Remove(address);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(Guid id)
        {
            return _uow.Addresses.Exists(id);
        }
    }
}

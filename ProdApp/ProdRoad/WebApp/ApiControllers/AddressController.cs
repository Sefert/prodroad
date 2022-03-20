#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AddressController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Address
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDTO>>> GetAddresses()
        {
            var address = (await _context.Addresses
                    .ToListAsync())
                    .Select(ad => new AddressDTO()
                    {
                        Id = ad.Id,
                        AppUserId = ad.AppUserId,
                        CustomerId = ad.CustomerId,
                        Country =  ad.Country,
                        City = ad.City,
                        Street = ad.Street,
                        Phone = ad.Phone,
                        Code = ad.Code,
                        Email = ad.Email
                    })
                    .ToList();
            return address;
        }

        // GET: api/Address/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDTO>> GetAddress(Guid id)
        {
            var dbAddress = await _context.Addresses.FindAsync(id);
            
            if (dbAddress == null)
            {
                return NotFound();
            }
            
            var address = new AddressDTO()
            {
                Id = dbAddress.Id,
                AppUserId = dbAddress.AppUserId,
                CustomerId = dbAddress.CustomerId,
                Country =  dbAddress.Country,
                City = dbAddress.City,
                Street = dbAddress.Street,
                Phone = dbAddress.Phone,
                Code = dbAddress.Code,
                Email = dbAddress.Email
            };
            
            return address;
        }

        // PUT: api/Address/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(Guid id, AddressDTO address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            var dbAddress = await _context.Addresses.FindAsync(id);
            
            if (dbAddress == null) {return NotFound();}

            dbAddress.Country!.SetTranslation(address.Country!);
            dbAddress.City!.SetTranslation(address.City!);
            dbAddress.Street!.SetTranslation(address.Street!);
            dbAddress.Code!.SetTranslation(address.Code!);
            dbAddress.Phone!.SetTranslation(address.Phone!);
            dbAddress.Email!.SetTranslation(address.Email!);

            _context.Entry(dbAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        public async Task<ActionResult<AddressDTO>> PostAddress(AddressDTO address)
        {

            var dbAddress = new Address()
            {
                AppUserId = address.AppUserId,
                CustomerId = address.CustomerId
            };
            
            dbAddress.Country!.SetTranslation(address.Country!);
            dbAddress.City!.SetTranslation(address.City!);
            dbAddress.Street!.SetTranslation(address.Street!);
            dbAddress.Code!.SetTranslation(address.Code!);
            dbAddress.Phone!.SetTranslation(address.Phone!);
            dbAddress.Email!.SetTranslation(address.Email!);
            
            _context.Addresses.Add(dbAddress);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.Id }, address);
        }

        // DELETE: api/Address/5
        //TODO: implement culture deletion
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(Guid id)
        {
            return _context.Addresses.Any(e => e.Id == id);
        }
    }
}

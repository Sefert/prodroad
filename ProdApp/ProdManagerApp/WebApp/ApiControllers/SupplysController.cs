using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SupplysController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ISupplyRepo _repo;
        private readonly Guid _uId;

        public SupplysController(AppDbContext context)
        {
            _context = context;
            _repo = new SupplyRepo(context);
            _uId = User.GetUserId()!.Value;
        }

        // GET: api/Supplys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supply>>> GetSupplys()
        {
            return Ok(await _repo.GetAllAsync(_uId,false));
        }

        // GET: api/Supplys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supply>> GetSupply(Guid id)
        {
            if (!await _repo.ExistsAsync(id,_uId))
            {
                return NotFound();
            }

            return Ok(await _repo.FirstOrDefaultAsync(id,_uId));
        }

        // PUT: api/Supplys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupply(Guid id, Supply supply)
        {
            if (id != supply.Id)
            {
                return BadRequest();
            }

            _context.Entry(supply).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repo.ExistsAsync(id,_uId))
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

        // POST: api/Supplys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Supply>> PostSupply(Supply supply)
        {
            _repo.Add(supply);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupply", new { id = supply.Id }, supply);
        }

        // DELETE: api/Supplys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupply(Guid id)
        {
            if (!await _repo.ExistsAsync(id,_uId))
            {
                return NotFound();
            }
            
            var component = await _repo.FirstOrDefaultAsync(id,_uId);
            if (component == null) return NotFound();
            _repo.Remove(component,_uId);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

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
    public class ProductionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IProductionRepo _repo;
        private readonly Guid _uId;

        public ProductionsController(AppDbContext context)
        {
            _context = context;
            _repo = new ProductionRepo(context);
            _uId = User.GetUserId()!.Value;
        }

        // GET: api/Productions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Production>>> GetProductions()
        {
            return Ok(await _repo.GetAllAsync(_uId,false));
        }

        // GET: api/Productions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Production>> GetProduction(Guid id)
        {
            if (!await _repo.ExistsAsync(id,_uId))
            {
                return NotFound();
            }

            return Ok(await _repo.FirstOrDefaultAsync(id,_uId));
        }

        // PUT: api/Productions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduction(Guid id, Production production)
        {
            if (id != production.Id)
            {
                return BadRequest();
            }

            _context.Entry(production).State = EntityState.Modified;

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

        // POST: api/Productions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Production>> PostProduction(Production production)
        {
            _repo.Add(production);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetProduction", new { id = production.Id }, production);
        }

        // DELETE: api/Productions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduction(Guid id)
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

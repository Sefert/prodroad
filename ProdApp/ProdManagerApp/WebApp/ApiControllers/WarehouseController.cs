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

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWarehouseRepo _repo;

        public WarehouseController(AppDbContext context)
        {
            _context = context;
            _repo = new WarehouseRepo(context);
        }

        // GET: api/Warehouse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouses()
        {
            return Ok(await _repo.GetAllAsync(false));
        }

        // GET: api/Warehouse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse>> GetWarehouse(Guid id)
        {
            if (!await _repo.ExistsAsync(id))
            {
                return NotFound();
            }

            return Ok(await _repo.FirstOrDefaultAsync(id));
        }

        // PUT: api/Warehouse/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse(Guid id, Warehouse warehouse)
        {
            if (id != warehouse.Id)
            {
                return BadRequest();
            }

            _context.Entry(warehouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repo.ExistsAsync(id))
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

        // POST: api/Warehouse
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Warehouse>> PostWarehouse(Warehouse warehouse)
        {
            _repo.Add(warehouse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWarehouse", new { id = warehouse.Id }, warehouse);
        }

        // DELETE: api/Warehouse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(Guid id)
        {
            if (!await _repo.ExistsAsync(id))
            {
                return NotFound();
            }
            
            var component = await _repo.FirstOrDefaultAsync(id);
            _repo.Remove(component);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

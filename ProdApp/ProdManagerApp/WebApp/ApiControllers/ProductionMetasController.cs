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

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionMetasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IProductionMetaRepo _repo;
        private readonly Guid _uId;

        public ProductionMetasController(AppDbContext context)
        {
            _context = context;
            _repo = new ProductionMetaRepo(context);
            _uId = User.GetUserId()!.Value;
        }

        // GET: api/ProductionMetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductionMeta>>> GetProductionMetas()
        {
            return Ok(await _repo.GetAllAsync(_uId,false));
        }

        // GET: api/ProductionMetas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductionMeta>> GetProductionMeta(Guid id)
        {
            if (!await _repo.ExistsAsync(id,_uId))
            {
                return NotFound();
            }

            return Ok(await _repo.FirstOrDefaultAsync(id,_uId));
        }

        // PUT: api/ProductionMetas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductionMeta(Guid id, ProductionMeta productionMeta)
        {
            if (id != productionMeta.Id)
            {
                return BadRequest();
            }

            _context.Entry(productionMeta).State = EntityState.Modified;

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

        // POST: api/ProductionMetas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductionMeta>> PostProductionMeta(ProductionMeta productionMeta)
        {
            _repo.Add(productionMeta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductionMeta", new { id = productionMeta.Id }, productionMeta);
        }

        // DELETE: api/ProductionMetas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductionMeta(Guid id)
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

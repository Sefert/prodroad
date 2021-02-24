using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionMetasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductionMetasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductionMetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductionMeta>>> GetProductionMetas()
        {
            return await _context.ProductionMetas.ToListAsync();
        }

        // GET: api/ProductionMetas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductionMeta>> GetProductionMeta(Guid id)
        {
            var productionMeta = await _context.ProductionMetas.FindAsync(id);

            if (productionMeta == null)
            {
                return NotFound();
            }

            return productionMeta;
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
                if (!ProductionMetaExists(id))
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
            _context.ProductionMetas.Add(productionMeta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductionMeta", new { id = productionMeta.Id }, productionMeta);
        }

        // DELETE: api/ProductionMetas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductionMeta(Guid id)
        {
            var productionMeta = await _context.ProductionMetas.FindAsync(id);
            if (productionMeta == null)
            {
                return NotFound();
            }

            _context.ProductionMetas.Remove(productionMeta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductionMetaExists(Guid id)
        {
            return _context.ProductionMetas.Any(e => e.Id == id);
        }
    }
}

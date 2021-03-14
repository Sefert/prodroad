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
    public class ComponentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IComponentRepo _repo;

        public ComponentsController(AppDbContext context)
        {
            _context = context;
            _repo = new ComponentRepo(context);
        }

        // GET: api/Components
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Component>>> GetComponents()
        {
            return Ok(await _repo.GetAllAsync(false));
        }

        // GET: api/Components/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Component>> GetComponent(Guid id)
        {
            if (!await _repo.ExistsAsync(id))
            {
                return NotFound();
            }
            return Ok(await _repo.FirstOrDefaultAsync(id));
        }

        // PUT: api/Components/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponent(Guid id, Component component)
        {
            if (id != component.Id)
            {
                return BadRequest();
            }

            _context.Entry(component).State = EntityState.Modified;

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

        // POST: api/Components
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Component>> PostComponent(Component component)
        {
            _repo.Add(component);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComponent", new { id = component.Id }, component);
        }

        // DELETE: api/Components/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponent(Guid id)
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

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
    public class ItemComponentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IItemComponentRepo _repo;
        private readonly Guid _uId;

        public ItemComponentsController(AppDbContext context)
        {
            _context = context;
            _repo = new ItemComponentRepo(context);
            _uId = User.GetUserId()!.Value;
        }

        // GET: api/ItemComponents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemComponent>>> GetItemComponents()
        {
            return Ok(await _repo.GetAllAsync( _uId,false));
        }

        // GET: api/ItemComponents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemComponent>> GetItemComponent(Guid id)
        {
            if (!await _repo.ExistsAsync(id, _uId))
            {
                return NotFound();
            }

            return Ok(await _repo.FirstOrDefaultAsync(id, _uId));
        }

        // PUT: api/ItemComponents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemComponent(Guid id, ItemComponent itemComponent)
        {
            if (id != itemComponent.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemComponent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repo.ExistsAsync(id, _uId))
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

        // POST: api/ItemComponents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemComponent>> PostItemComponent(ItemComponent itemComponent)
        {
            _repo.Add(itemComponent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemComponent", new { id = itemComponent.Id }, itemComponent);
        }

        // DELETE: api/ItemComponents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemComponent(Guid id)
        {
            if (!await _repo.ExistsAsync(id, _uId))
            {
                return NotFound();
            }
            
            var component = await _repo.FirstOrDefaultAsync(id, _uId);
            if (component == null) return NotFound();
            _repo.Remove(component, _uId);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

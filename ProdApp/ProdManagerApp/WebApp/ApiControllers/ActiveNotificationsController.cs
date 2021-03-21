using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActiveNotificationsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IActiveNotificationRepo _repo;

        public ActiveNotificationsController(AppDbContext context)
        {
            _context = context;
            _repo = new ActiveNotificationRepo(context);
        }

        // GET: api/ActiveNotifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActiveNotification>>> GetActiveNotifications()
        {
            return Ok(await _repo.GetAllAsync(false));
        }

        // GET: api/ActiveNotifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActiveNotification>> GetActiveNotification(Guid id)
        {
            if (!await _repo.ExistsAsync(id))
            {
                return NotFound();
            }
            return Ok(await _repo.FirstOrDefaultAsync(id));
        }

        // PUT: api/ActiveNotifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActiveNotification(Guid id, ActiveNotification activeNotification)
        {
            if (id != activeNotification.Id) return BadRequest();

            _context.Entry(activeNotification).State = EntityState.Modified;

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

        // POST: api/ActiveNotifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ActiveNotification>> PostActiveNotification(ActiveNotification activeNotification)
        {
            _repo.Add(activeNotification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActiveNotification", new { id = activeNotification.Id }, activeNotification);
        }

        // DELETE: api/ActiveNotifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActiveNotification(Guid id)
        {
            
            if (!await _repo.ExistsAsync(id))
            {
                return NotFound();
            }
            
            var activeNotification = await _repo.FirstOrDefaultAsync(id);
            if (activeNotification == null) return NotFound();
            _repo.Remove(activeNotification);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

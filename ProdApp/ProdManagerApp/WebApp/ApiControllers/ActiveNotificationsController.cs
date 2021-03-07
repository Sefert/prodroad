using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActiveNotificationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActiveNotificationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ActiveNotifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActiveNotification>>> GetActiveNotifications()
        {
            return await _context.ActiveNotifications.ToListAsync();
        }

        // GET: api/ActiveNotifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActiveNotification>> GetActiveNotification(Guid id)
        {
            var activeNotification = await _context.ActiveNotifications.FindAsync(id);

            if (activeNotification == null)
            {
                return NotFound();
            }

            return activeNotification;
        }

        // PUT: api/ActiveNotifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActiveNotification(Guid id, ActiveNotification activeNotification)
        {
            if (id != activeNotification.Id)
            {
                return BadRequest();
            }

            _context.Entry(activeNotification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActiveNotificationExists(id))
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
            _context.ActiveNotifications.Add(activeNotification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActiveNotification", new { id = activeNotification.Id }, activeNotification);
        }

        // DELETE: api/ActiveNotifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActiveNotification(Guid id)
        {
            var activeNotification = await _context.ActiveNotifications.FindAsync(id);
            if (activeNotification == null)
            {
                return NotFound();
            }

            _context.ActiveNotifications.Remove(activeNotification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActiveNotificationExists(Guid id)
        {
            return _context.ActiveNotifications.Any(e => e.Id == id);
        }
    }
}

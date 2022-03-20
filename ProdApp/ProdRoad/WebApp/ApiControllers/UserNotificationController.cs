#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserNotificationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserNotificationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserNotification
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserNotificationDTO>>> GetUserNotifications()
        {
            var dataList = (await _context.UserNotifications
                    .ToListAsync())
                .Select(row => new UserNotificationDTO()
                {
                    NotificationType = row.NotificationType,
                    AppUserId = row.AppUserId,
                    Name = row.Name,
                    Color = row.Color,
                    Active = row.Active
                })
                .ToList();
            return dataList;
        }

        // GET: api/UserNotification/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserNotificationDTO>> GetUserNotification(Guid id)
        {
            var dbRow = await _context.UserNotifications.FindAsync(id);
            
            if (dbRow == null)
            {
                return NotFound();
            }
            
            var userNotification = new UserNotificationDTO()
            {
                Id = dbRow.Id,
                NotificationType = dbRow.NotificationType,
                AppUserId = dbRow.AppUserId,
                Name = dbRow.Name,
                Color = dbRow.Color,
                Active = dbRow.Active
            };

            return userNotification;
        }

        // PUT: api/UserNotification/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserNotification(Guid id, UserNotificationDTO userNotification)
        {
            if (id != userNotification.Id)
            {
                return BadRequest();
            }

            var dbRow = await _context.UserNotifications.FindAsync(id);
            
            if (dbRow == null) {return NotFound();}

            dbRow.Name!.SetTranslation(userNotification.Name!);
            dbRow.Color!.SetTranslation(userNotification.Color!);

            _context.Entry(userNotification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserNotificationExists(id))
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

        // POST: api/UserNotification
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserNotificationDTO>> PostUserNotification(UserNotificationDTO userNotification)
        {
            var dbRow = new UserNotification()
            {
                AppUserId = userNotification.AppUserId,
                Active = userNotification.Active
            };
            
            dbRow.Name!.SetTranslation(userNotification.Name!);
            dbRow.Color!.SetTranslation(userNotification.Color!);
            
            _context.UserNotifications.Add(dbRow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserNotification", new { id = userNotification.Id }, userNotification);
        }

        // DELETE: api/UserNotification/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserNotification(Guid id)
        {
            var userNotification = await _context.UserNotifications.FindAsync(id);
            if (userNotification == null)
            {
                return NotFound();
            }

            _context.UserNotifications.Remove(userNotification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserNotificationExists(Guid id)
        {
            return _context.UserNotifications.Any(e => e.Id == id);
        }
    }
}

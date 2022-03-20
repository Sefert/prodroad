#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActiveNotificationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActiveNotificationController(AppDbContext context)
        {
            _context = context;
        }
        
        // GET: api/ActiveNotification
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActiveNotificationDTO>>> GetActiveNotifications()
        {
            var dataList = (await _context.ActiveNotifications
                .ToListAsync())
                .Select(row => new ActiveNotificationDTO()
                {
                    ProcessId = row.ProcessId,
                    UserNotificationId = row.UserNotificationId,
                    TeamId = row.TeamId,
                    Head =  row.Head,
                    Info = row.Info,
                    Active = row.Active,
                })
                .ToList();
            return dataList;
        }

        // GET: api/ActiveNotification/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActiveNotificationDTO>> GetActiveNotification(Guid id)
        {
            var dbData = await _context.ActiveNotifications.FindAsync(id);
            
            if (dbData == null)
            {
                return NotFound();
            }
            
            var activeNotification = new ActiveNotificationDTO()
            {
                Id = dbData.Id,
                ProcessId = dbData.ProcessId,
                UserNotificationId = dbData.UserNotificationId,
                TeamId = dbData.TeamId,
                Head =  dbData.Head,
                Info = dbData.Info,
                Active = dbData.Active
            };

            return activeNotification;
        }

        // PUT: api/ActiveNotification/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActiveNotification(Guid id, ActiveNotificationDTO activeNotification)
        {
            if (id != activeNotification.Id)
            {
                return BadRequest();
            }

            var dbData = await _context.ActiveNotifications.FindAsync(id);
            
            if (dbData == null) {return NotFound();}

            dbData.Info!.SetTranslation(activeNotification.Info!);
            dbData.Head!.SetTranslation(activeNotification.Head!);

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

        // POST: api/ActiveNotification
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ActiveNotificationDTO>> PostActiveNotification(ActiveNotificationDTO activeNotification)
        {
            var dbRow = new ActiveNotification()
            {
                ProcessId = activeNotification.ProcessId,
                UserNotificationId = activeNotification.UserNotificationId,
                TeamId = activeNotification.TeamId
            };
            
            dbRow.Info!.SetTranslation(activeNotification.Info!);
            dbRow.Head!.SetTranslation(activeNotification.Head!);
            
            _context.ActiveNotifications.Add(dbRow);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActiveNotification", new { id = activeNotification.Id }, activeNotification);
        }

        // DELETE: api/ActiveNotification/5
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

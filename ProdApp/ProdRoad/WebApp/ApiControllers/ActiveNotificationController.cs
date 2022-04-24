#nullable enable
using DAL.App.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.App;
using DTO.App;
using WebApp.DTO;

//TODO: needs future implementation (removed for scope narrowing)
namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActiveNotificationController : ControllerBase
    {
        /*private readonly IAppUnitOfWork _uow;

        public ActiveNotificationController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }
        
        // GET: api/ActiveNotification
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActiveNotification>>> GetActiveNotifications()
        {
            var dataList = (await _uow.ActiveNotifications
                .GetAllAsync())
                .Select(row => new ActiveNotificationDTO()
                {
                    Id = row.Id,
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
            var dbData = await _uow.ActiveNotifications.FirstOrDefaultAsync(id);
            
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

            var dbData = await _uow.ActiveNotifications.FirstOrDefaultAsync(id);
            
            if (dbData == null) {return NotFound();}

            dbData.Info!.SetTranslation(activeNotification.Info!);
            dbData.Head!.SetTranslation(activeNotification.Head!);

            _uow.ActiveNotifications.ModifyState(dbData);

            try
            {
                await _uow.SaveChangesAsync();
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
            
            _uow.ActiveNotifications.Add(dbRow);
            
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetActiveNotification", new { id = activeNotification.Id }, activeNotification);
        }

        // DELETE: api/ActiveNotification/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActiveNotification(Guid id)
        {
            var activeNotification = await _uow.ActiveNotifications.FirstOrDefaultAsync(id);
            if (activeNotification == null)
            {
                return NotFound();
            }

            _uow.ActiveNotifications.Remove(activeNotification);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool ActiveNotificationExists(Guid id)
        {
            return _uow.ActiveNotifications.Exists(id);
        }*/
    }
}

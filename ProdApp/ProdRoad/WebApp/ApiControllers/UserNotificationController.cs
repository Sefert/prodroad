#nullable enable
using DAL.App.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.App;
using WebApp.DTO;

//TODO: needs future implementation (removed for scope narrowing)
namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserNotificationController : ControllerBase
    {
        /*private readonly IAppUnitOfWork _uow;

        public UserNotificationController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/UserNotification
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserNotificationDTO>>> GetUserNotifications()
        {
            var dataList = (await _uow.UserNotifications
                    .GetAllAsync())
                .Select(row => new UserNotificationDTO()
                {
                    Id = row.Id,
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
            var dbRow = await _uow.UserNotifications.FirstOrDefaultAsync(id);
            
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

            var dbRow = await _uow.UserNotifications.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            dbRow.Name!.SetTranslation(userNotification.Name!);
            dbRow.Color!.SetTranslation(userNotification.Color!);

            _uow.UserNotifications.ModifyState(dbRow);

            try
            {
                await _uow.SaveChangesAsync();
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
            
            _uow.UserNotifications.Add(dbRow);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetUserNotification", new { id = userNotification.Id }, userNotification);
        }

        // DELETE: api/UserNotification/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserNotification(Guid id)
        {
            var userNotification = await _uow.UserNotifications.FirstOrDefaultAsync(id);
            if (userNotification == null)
            {
                return NotFound();
            }

            _uow.UserNotifications.Remove(userNotification);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool UserNotificationExists(Guid id)
        {
            return _uow.UserNotifications.Exists(id);
        }*/
    }
}

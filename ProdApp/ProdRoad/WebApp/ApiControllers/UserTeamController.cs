#nullable enable
using BLL.App.Contracts;
using BLL.App.DTO;
using DAL.App.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTeamController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public UserTeamController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/UserTeam
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTeam>>> GetUserTeams()
        {
            var dataList = (await _bll.UserTeams
                    .GetAllAsync())
                .ToList();
            return dataList;
        }

        // GET: api/UserTeam/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTeam>> GetUserTeam(Guid id)
        {
            var userTeam  = await _bll.UserTeams.FirstOrDefaultAsync(id);
            
            if (userTeam  == null)
            {
                return NotFound();
            }

            return userTeam;
        }

        // PUT: api/UserTeam/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTeam(Guid id, UserTeam userTeam)
        {
            if (id != userTeam.Id)
            {
                return BadRequest();
            }

            var dbRow = await _bll.UserTeams.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            _bll.UserTeams.ModifyState(dbRow);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTeamExists(id))
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

        // POST: api/UserTeam
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserTeam>> PostUserTeam(UserTeam userTeam)
        {
            var dbRow = new UserTeam()
            {
                AppUserId = userTeam.AppUserId,
                TeamId = userTeam.TeamId
            };
            _bll.UserTeams.Add(dbRow);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserTeam", new { id = userTeam.Id }, userTeam);
        }

        // DELETE: api/UserTeam/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTeam(Guid id)
        {
            var userTeam = await _bll.UserTeams.FirstOrDefaultAsync(id);
            if (userTeam == null)
            {
                return NotFound();
            }

            _bll.UserTeams.Remove(userTeam);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool UserTeamExists(Guid id)
        {
            return _bll.UserTeams.Exists(id);
        }
    }
}

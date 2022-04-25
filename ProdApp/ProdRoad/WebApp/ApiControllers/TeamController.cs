#nullable enable
using DAL.App.Contracts;
using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TeamController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public TeamController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            var dataList = (await _uow.Teams
                    .GetAllAsync(User.GetUserId()))
                .ToList();
            return dataList;
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(Guid id)
        {
            var team = await _uow.Teams.FirstOrDefaultAsync(User.GetUserId(), id);
                
            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // PUT: api/Team/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(Guid id, Team team)
        {
            if (id != team.Id)
            {
                return BadRequest();
            }

            var dbRow = await _uow.Teams.FirstOrDefaultAsync(User.GetUserId(),id);
            
            if (dbRow == null) {return NotFound();}

            dbRow.Name!.SetTranslation(team.Name!);
            dbRow.Code!.SetTranslation(team.Code!);

            _uow.Teams.ModifyState(dbRow);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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

        // POST: api/Team
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            var dbRow = new Team()
            {
                Id = team.Id,
                AppUserId = User.GetUserId()
            };
            
            dbRow.Name!.SetTranslation(team.Name!);
            dbRow.Code!.SetTranslation(team.Code!);
            
            _uow.Teams.Add(dbRow);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetTeam", new { id = team.Id }, team);
        }

        // DELETE: api/Team/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(Guid id)
        {
            var team = await _uow.Teams.FirstOrDefaultAsync(User.GetUserId(),id);
            if (team == null)
            {
                return NotFound();
            }

            _uow.Teams.Remove(team);
            await _uow.SaveChangesAsync();

            return NoContent();
        }
        
        private bool TeamExists(Guid id)
        {
            return _uow.Teams.Exists(id);
        }
    }
}

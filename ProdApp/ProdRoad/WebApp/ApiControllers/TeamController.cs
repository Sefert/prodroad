#nullable enable
using DAL.App.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.App;
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public TeamController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeams()
        {
            var dataList = (await _uow.Teams
                    .GetAllAsync())
                .Select(row => new TeamDTO()
                {
                    AppUserId = row.AppUserId,
                    Name = row.Name,
                    Code = row.Code
                })
                .ToList();
            return dataList;
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDTO>> GetTeam(Guid id)
        {
            var dbRow = await _uow.Teams.FirstOrDefaultAsync(id);
            
            if (dbRow == null)
            {
                return NotFound();
            }
            
            var team = new TeamDTO()
            {
                Id = dbRow.Id,
                AppUserId = dbRow.AppUserId,
                Name = dbRow.Name,
                Code = dbRow.Code
            };

            return team;
        }

        // PUT: api/Team/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(Guid id, TeamDTO team)
        {
            if (id != team.Id)
            {
                return BadRequest();
            }

            var dbRow = await _uow.Teams.FirstOrDefaultAsync(id);
            
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
        public async Task<ActionResult<TeamDTO>> PostTeam(TeamDTO team)
        {
            var dbRow = new Team()
            {
                AppUserId = team.AppUserId,
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
            var team = await _uow.Teams.FirstOrDefaultAsync(id);
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

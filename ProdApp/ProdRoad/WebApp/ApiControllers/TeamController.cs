#nullable enable

using BLL.App.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Public.App.DTO.v1;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles="admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TeamController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public TeamController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            var dataList = (await _bll.Teams
                    .GetAllAsync(User.GetUserId())).Select(x => new Public.App.DTO.v1.Team()
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Code = x.Code,
                        }).ToList();
            return dataList;
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(Guid id)
        {
            var bllTeam = await _bll.Teams.FirstOrDefaultAsync(User.GetUserId(), id);
            
            if (bllTeam == null)
            {
                return NotFound();
            }
            
            var publicTeam = new Public.App.DTO.v1.Team()
            {
                Id = bllTeam.Id,
                AppUserId = User.GetUserId(),
                Name = bllTeam.Name,
                Code = bllTeam.Code
            };
            

            return publicTeam;
        }

        // PUT: api/Team/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(Guid id, Public.App.DTO.v1.Team team)
        {
            if (id != team.Id)
            {
                return BadRequest();
            }

            var bllTeam = await _bll.Teams.FirstOrDefaultAsync(User.GetUserId(),id);
            
            if (bllTeam == null) {return NotFound();}

            bllTeam.Name!.SetTranslation(team.Name!);
            bllTeam.Code!.SetTranslation(team.Code!);

            //_bll.Entry(bllTeam).State = EntityState.Modified;
            _bll.Teams.ModifyState(bllTeam);

            try
            {
                await _bll.SaveChangesAsync();
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
        public async Task<ActionResult<Team>> PostTeam(Public.App.DTO.v1.Team team)
        {
            var bllTeam = new BLL.App.DTO.Team()
            {
                AppUserId = User.GetUserId()
            };
            
            bllTeam.Name!.SetTranslation(team.Name!);
            bllTeam.Code!.SetTranslation(team.Code!);
            
            _bll.Teams.Add(bllTeam);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTeam", 
                new
                {
                    version = HttpContext.GetRequestedApiVersion()!.ToString(),
                    id = team.Id
                }, 
                team);
        }

        // DELETE: api/Team/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(Guid id)
        {
            var team = await _bll.Teams.FirstOrDefaultAsync(User.GetUserId(),id);
            if (team == null)
            {
                return NotFound();
            }

            _bll.Teams.Remove(team);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
        
        private bool TeamExists(Guid id)
        {
            return _bll.Teams.Exists(id);
        }
    }
}

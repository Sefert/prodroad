#nullable enable
using BLL.App.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Public.App.DTO.v1;
using Public.App.DTO.v1.Identity;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public TeamController(IAppBLL bll)
        {
            _bll = bll;
        }

        [HttpGet("[action]/{code}")]
        [Authorize(Roles="user,admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Public.App.DTO.v1.Team>> GetPublicTeam(string code)
        {
            var incomingTeam = new BLL.App.DTO.Team();
            incomingTeam.Code!.SetTranslation(code);
            //incomingTeam.Code!.SetTranslation(team.Code!);
            var bllTeam = await _bll.Teams.PublicTeamAsync(code,User.GetUserId());

            if (bllTeam == null)
            {
                return NotFound();
            }
            
            var publicUserTeams = bllTeam.UserTeams?.Select(x => new Public.App.DTO.v1.UserTeam()
            {
                Id = x.Id,
                AppUserId = x.AppUserId,
                TeamId = x.TeamId,
                Accepted = x.Accepted
            }).ToList();

            var publicTeam = new Public.App.DTO.v1.Team()
            {
                Id = bllTeam.Id,
                AppUserId = User.GetUserId(),
                Name = bllTeam.Name,
                Code = bllTeam.Code,
                IsPublic = bllTeam.IsPublic,
                UserTeams = publicUserTeams
            };
            
            return publicTeam;
        }

        
        // GET: api/Team
        [HttpGet]
        [Authorize(Roles="admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            var dataList = (await _bll.Teams
                    .GetAllAsync(User.GetUserId())).Select(x => new Public.App.DTO.v1.Team()
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Code = x.Code,
                            IsPublic = x.IsPublic,
                            UserTeams = x.UserTeams != null ? 
                                x.UserTeams.Select(ut => new Public.App.DTO.v1.UserTeam()
                            {
                                Id = ut.Id,
                                AppUserId = ut.AppUserId,
                                AppUser = new Public.App.DTO.v1.Identity.AppUser
                                {
                                    Id = ut.AppUserId,
                                    UserName = ut.AppUser?.UserName,
                                },
                                TeamId = ut.TeamId,
                                Accepted = ut.Accepted
                            }).ToList() : new List<UserTeam>()
                        }).ToList();
            return dataList;
        }
        
        

        // GET: api/Team/5
        [HttpGet("{id}")]
        [Authorize(Roles="admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                Code = bllTeam.Code,
                IsPublic = bllTeam.IsPublic
            };
            
            return publicTeam;
        }

        // PUT: api/Team/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles="admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            bllTeam.IsPublic = team.IsPublic;

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
        [Authorize(Roles="admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Team>> PostTeam(Public.App.DTO.v1.Team team)
        {
            var bllTeam = new BLL.App.DTO.Team()
            {
                Id = Guid.NewGuid(),
                AppUserId = User.GetUserId(),
                IsPublic = team.IsPublic
            };
            
            bllTeam.Name!.SetTranslation(team.Name!);
            bllTeam.Code!.SetTranslation(team.Code!);
            
            _bll.Teams.Add(bllTeam);
            await _bll.SaveChangesAsync();

            team.Id = bllTeam.Id;

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
        [Authorize(Roles="admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

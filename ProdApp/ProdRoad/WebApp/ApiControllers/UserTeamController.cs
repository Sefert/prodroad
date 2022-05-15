#nullable enable

using BLL.App.Contracts;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Public.App.DTO.v1;

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
        [Authorize(Roles="user,admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<UserTeam>>> GetUserTeams()
        {
            var dataList = (await _bll.UserTeams
                    .GetAllAsync(User.GetUserId())).Select(x => new Public.App.DTO.v1.UserTeam()
            {
                Id = x.Id,
                AppUserId = x.AppUserId,
                TeamId = x.TeamId,
                //Team = x.Team,
                Accepted = x.Accepted,
            }).ToList();
            return dataList;
        }

        // GET: api/UserTeam/5
        [HttpGet("{id}")]
        [Authorize(Roles="user,admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserTeam>> GetUserTeam(Guid id)
        {
            var userTeam  = await _bll.UserTeams.FirstOrDefaultAsync(id);

            var publicUserTeam = new Public.App.DTO.v1.UserTeam()
            {
                Id = userTeam.Id,
                AppUserId = userTeam.AppUserId,
                TeamId = userTeam.TeamId,
                //Team = x.Team,
                Accepted = userTeam.Accepted,
            };
            
            if (userTeam  == null)
            {
                return NotFound();
            }

            return publicUserTeam;
        }

        // PUT: api/UserTeam/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("[action]/{id}")]
        [HttpPut("{id}")]
        [Authorize(Roles="admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutUserTeam(Guid id, Public.App.DTO.v1.UserTeam userTeam)
        {
            if (id != userTeam.Id)
            {
                return BadRequest();
            }

            var bllUserTeam = await _bll.UserTeams.FirstOrDefaultAsync(id);

            if (bllUserTeam == null) {return NotFound();}

            bllUserTeam.Accepted = userTeam.Accepted;
            _bll.UserTeams.ModifyState(bllUserTeam);

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
        [HttpPost("[action]")]
        [Authorize(Roles="user,admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserTeam>> UserTeam(Public.App.DTO.v1.UserTeam userTeam)
        {
            var bllUserTeam = new BLL.App.DTO.UserTeam()
            {
                Id = userTeam.Id,
                AppUserId = User.GetUserId(),
                TeamId = userTeam.TeamId,
                Accepted = false,
            };
            
            _bll.UserTeams.Add(bllUserTeam);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserTeam", new
            {
                version = HttpContext.GetRequestedApiVersion()!.ToString(),
                id = userTeam.Id
            }, userTeam);
        }
        
        // POST: api/UserTeam
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("[action]")]
        [Authorize(Roles="admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserTeam>> ManagerTeam(Public.App.DTO.v1.UserTeam userTeam)
        {
            var bllUserTeam = new BLL.App.DTO.UserTeam()
            {
                Id = userTeam.Id,
                AppUserId = User.GetUserId(),
                TeamId = userTeam.TeamId,
                Accepted = userTeam.Accepted,
            };
            
            _bll.UserTeams.Add(bllUserTeam);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserTeam", new
            {
                version = HttpContext.GetRequestedApiVersion()!.ToString(),
                id = userTeam.Id
            }, userTeam);
        }

        // DELETE: api/UserTeam/5
        [HttpDelete("{id}")]
        [Authorize(Roles="admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteUserTeam(Guid id)
        {
            var userTeam = await _bll.UserTeams.FirstOrDefaultAsync(id);
            if (userTeam == null)
            {
                return NotFound();
            }

            await _bll.UserTeams.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool UserTeamExists(Guid id)
        {
            return _bll.UserTeams.Exists(id);
        }
    }
}

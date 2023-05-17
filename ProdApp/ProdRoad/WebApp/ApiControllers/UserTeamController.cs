#nullable enable

using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Public.App.Contracts.v1;
using Public.App.DTO.v1.UserTeamDTO;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// UserTeamController contains all v1 userTeam many to many access REST methods
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserTeamController : ControllerBase
    {
        private readonly IAppPublic _v1;
        
        /// <summary>
        /// Constructor for controller
        /// </summary>
        /// <param name="v1">Interface for accessing existing v1 layer service context</param>
        public UserTeamController(IAppPublic v1)
        {
            _v1 = v1;
        }

        /// <summary>
        /// GetUserTeams method is for accessing user allowed UserTeams
        /// </summary>
        /// <returns>List of all UserTeams</returns>
        // GET: api/UserTeam
        [Authorize(Roles="user,admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<UserTeam>>> GetUserTeams()
        {
            return (await _v1.UserTeams.GetAllAsync(User.GetUserId())).ToList();
            //return dataList;
        }
        
        /// <summary>
        /// GetUserTeam method is for accessing UserTeam
        /// </summary>
        /// <param name="id">specific UserTeam id</param>
        /// <returns>found UserTeam or NotFound 404</returns>
        // GET: api/UserTeam/5
        [HttpGet("{id}")]
        [Authorize(Roles="user,admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserTeam>> GetUserTeam(Guid id)
        {
            var userTeam = await _v1.UserTeams.FirstOrDefaultAsync(User.GetUserId(),id);
            
            return  userTeam != null ? userTeam : NotFound();
        }
        
        /// <summary>
        /// Method for accessing UserTeam by Team id which is connected to user
        /// Method meant for User to send join request to public Team
        /// </summary>
        /// <param name="id">Team id</param>
        /// <returns>UserTeam or Error Code 404</returns>
        // GET: api/UserTeam/Team/5
        [HttpGet("Team/{id}")]
        [Authorize(Roles="user,admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserTeam>> GetUserTeamByTeamId(Guid id)
        {
            var userTeam  = await _v1.UserTeams.FirstOrDefaultAsync(User.GetUserId(),id);
            
            return  userTeam != null ? userTeam : NotFound();
        }

        /// <summary>
        /// Method for modifying UserTeam and only manager can modify
        /// </summary>
        /// <param name="id">UserTeam id</param>
        /// <param name="userTeam">UserTeam object itself</param>
        /// <returns>Code 204</returns>
        // PUT: api/UserTeam/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("[action]/{id}")]
        [HttpPut("{id}")]
        [Authorize(Roles="admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutUserTeam(Guid id, Public.App.DTO.v1.UserTeam userTeam)
        {
            if (id != userTeam.Id) {return BadRequest();}

            var v1UserTeam = await _v1.UserTeams.FirstOrDefaultAsync(id);

            if (v1UserTeam == null) {return NotFound();}
            
            v1UserTeam.Accepted = userTeam.Accepted;
            
            _v1.UserTeams.ModifyState(v1UserTeam);

            try
            {
                await _v1.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_v1.UserTeams.Exists(id))
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

        /// <summary>
        /// Method for adding new UserTeam. Add connection to Team and User
        /// </summary>
        /// <param name="userTeam">UserTeam object</param>
        /// <returns>UserTeam with new generated and saved Guid key</returns>
        // POST: api/UserTeam
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles="user,admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserTeam>> PostUserTeam([FromBody] UserTeam userTeam)
        {
            _v1.UserTeams.Add(userTeam);
            await _v1.SaveChangesAsync();

            return CreatedAtAction("GetUserTeam", new
            {
                version = HttpContext.GetRequestedApiVersion()!.ToString(),
                id = userTeam.Id
            }, userTeam);
        }

        /// <summary>
        /// Delete userTeams and only manager can delete Team User Connections
        /// </summary>
        /// <param name="id">UserTeam id for finding UserTeam to delete</param>
        /// <returns>Code 204</returns>
        // DELETE: api/UserTeam/5
        [HttpDelete("{id}")]
        [Authorize(Roles="admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteUserTeam(Guid id)
        {
            await _v1.UserTeams.RemoveAsync(id);
            await _v1.SaveChangesAsync();

            return NoContent();
        }
    }
}

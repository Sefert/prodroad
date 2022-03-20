#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTeamController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserTeamController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserTeam
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTeamDTO>>> GetUserTeams()
        {
            var dataList = (await _context.UserTeams
                    .ToListAsync())
                .Select(row => new UserTeamDTO()
                {
                    AppUserId = row.AppUserId,
                    TeamId = row.TeamId
                })
                .ToList();
            return dataList;
        }

        // GET: api/UserTeam/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTeamDTO>> GetUserTeam(Guid id)
        {
            var dbRow = await _context.UserTeams.FindAsync(id);
            
            if (dbRow == null)
            {
                return NotFound();
            }
            
            var userTeam = new UserTeamDTO()
            {
                Id = dbRow.Id,
                AppUserId = dbRow.AppUserId,
                TeamId = dbRow.TeamId
            };

            return userTeam;
        }

        // PUT: api/UserTeam/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTeam(Guid id, UserTeamDTO userTeam)
        {
            if (id != userTeam.Id)
            {
                return BadRequest();
            }

            var dbRow = await _context.UserTeams.FindAsync(id);
            
            if (dbRow == null) {return NotFound();}

            _context.Entry(userTeam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        public async Task<ActionResult<UserTeamDTO>> PostUserTeam(UserTeamDTO userTeam)
        {
            var dbRow = new UserTeam()
            {
                AppUserId = userTeam.AppUserId,
                TeamId = userTeam.TeamId
            };
            _context.UserTeams.Add(dbRow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserTeam", new { id = userTeam.Id }, userTeam);
        }

        // DELETE: api/UserTeam/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTeam(Guid id)
        {
            var userTeam = await _context.UserTeams.FindAsync(id);
            if (userTeam == null)
            {
                return NotFound();
            }

            _context.UserTeams.Remove(userTeam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserTeamExists(Guid id)
        {
            return _context.UserTeams.Any(e => e.Id == id);
        }
    }
}

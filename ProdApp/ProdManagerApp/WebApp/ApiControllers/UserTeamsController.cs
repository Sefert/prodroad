using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTeamsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserTeamsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserTeams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTeam>>> GetUserTeams()
        {
            return await _context.UserTeams.ToListAsync();
        }

        // GET: api/UserTeams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTeam>> GetUserTeam(Guid id)
        {
            var userTeam = await _context.UserTeams.FindAsync(id);

            if (userTeam == null)
            {
                return NotFound();
            }

            return userTeam;
        }

        // PUT: api/UserTeams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTeam(Guid id, UserTeam userTeam)
        {
            if (id != userTeam.Id)
            {
                return BadRequest();
            }

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

        // POST: api/UserTeams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserTeam>> PostUserTeam(UserTeam userTeam)
        {
            _context.UserTeams.Add(userTeam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserTeam", new { id = userTeam.Id }, userTeam);
        }

        // DELETE: api/UserTeams/5
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

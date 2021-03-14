using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTeamsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUserTeamRepo _repo;

        public UserTeamsController(AppDbContext context)
        {
            _context = context;
            _repo = new UserTeamRepo(context);
        }

        // GET: api/UserTeams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTeam>>> GetUserTeams()
        {
            return Ok(await _repo.GetAllAsync(false));
        }

        // GET: api/UserTeams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTeam>> GetUserTeam(Guid id)
        {
            if (!await _repo.ExistsAsync(id))
            {
                return NotFound();
            }

            return Ok(await _repo.FirstOrDefaultAsync(id));
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
                if (!await _repo.ExistsAsync(id))
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
            _repo.Add(userTeam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserTeam", new { id = userTeam.Id }, userTeam);
        }

        // DELETE: api/UserTeams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTeam(Guid id)
        {
            if (!await _repo.ExistsAsync(id))
            {
                return NotFound();
            }
            
            var component = await _repo.FirstOrDefaultAsync(id);
            _repo.Remove(component);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

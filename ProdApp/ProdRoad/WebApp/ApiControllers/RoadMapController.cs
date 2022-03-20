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

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoadMapController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoadMapController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RoadMap
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoadMap>>> GetRoadMaps()
        {
            return await _context.RoadMaps.ToListAsync();
        }

        // GET: api/RoadMap/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoadMap>> GetRoadMap(Guid id)
        {
            var roadMap = await _context.RoadMaps.FindAsync(id);

            if (roadMap == null)
            {
                return NotFound();
            }

            return roadMap;
        }

        // PUT: api/RoadMap/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoadMap(Guid id, RoadMap roadMap)
        {
            if (id != roadMap.Id)
            {
                return BadRequest();
            }

            _context.Entry(roadMap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoadMapExists(id))
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

        // POST: api/RoadMap
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoadMap>> PostRoadMap(RoadMap roadMap)
        {
            _context.RoadMaps.Add(roadMap);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoadMap", new { id = roadMap.Id }, roadMap);
        }

        // DELETE: api/RoadMap/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoadMap(Guid id)
        {
            var roadMap = await _context.RoadMaps.FindAsync(id);
            if (roadMap == null)
            {
                return NotFound();
            }

            _context.RoadMaps.Remove(roadMap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoadMapExists(Guid id)
        {
            return _context.RoadMaps.Any(e => e.Id == id);
        }
    }
}

#nullable enable
using DAL.App.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="admin,user",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoadMapController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public RoadMapController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/RoadMap
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoadMapDTO>>> GetRoadMaps()
        {
            var dataList = (await _uow.RoadMaps
                    .GetAllAsync())
                .Select(row => new RoadMapDTO()
                {
                    Id = row.Id,
                    AppUserId = row.AppUserId,
                    Name = row.Name,
                    Position = row.Position,
                    Line = row.Line
                })
                .ToList();
            return dataList;
        }

        // GET: api/RoadMap/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoadMapDTO>> GetRoadMap(Guid id)
        {
            var dbRow = await _uow.RoadMaps.FirstOrDefaultAsync(id);
            
            if (dbRow == null)
            {
                return NotFound();
            }
            
            var roadMap = new RoadMapDTO()
            {
                Id = dbRow.Id,
                AppUserId = dbRow.AppUserId,
                Name = dbRow.Name,
                Position = dbRow.Position,
                Line = dbRow.Line
            };

            return roadMap;
        }

        // PUT: api/RoadMap/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoadMap(Guid id, RoadMapDTO roadMap)
        {
            if (id != roadMap.Id)
            {
                return BadRequest();
            }

            var dbRow = await _uow.RoadMaps.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            dbRow.Name!.SetTranslation(roadMap.Name!);
            dbRow.Position!.SetTranslation(roadMap.Position!);
            dbRow.Line!.SetTranslation(roadMap.Line!);

            _uow.RoadMaps.ModifyState(dbRow);

            try
            {
                await _uow.SaveChangesAsync();
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
        public async Task<ActionResult<RoadMapDTO>> PostRoadMap(RoadMapDTO roadMap)
        {
            var dbRow = new RoadMap()
            {
                AppUserId = roadMap.AppUserId,
            };
            
            dbRow.Name!.SetTranslation(roadMap.Name!);
            dbRow.Position!.SetTranslation(roadMap.Position!);
            dbRow.Line!.SetTranslation(roadMap.Line!);
            
            _uow.RoadMaps.Add(dbRow);
            
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetRoadMap", new { id = roadMap.Id }, roadMap);
        }

        // DELETE: api/RoadMap/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoadMap(Guid id)
        {
            var roadMap = await _uow.RoadMaps.FirstOrDefaultAsync(id);
            if (roadMap == null)
            {
                return NotFound();
            }

            _uow.RoadMaps.Remove(roadMap);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool RoadMapExists(Guid id)
        {
            return _uow.RoadMaps.Exists(id);
        }
    }
}

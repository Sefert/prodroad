#nullable enable
using DAL.App.Contracts;
using DTO.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="admin,manager",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoadMapController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public RoadMapController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/RoadMap
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoadMap>>> GetRoadMaps()
        {
            var dataList = (await _uow.RoadMaps
                    .GetAllAsync(User.GetUserId()))
                .ToList();
            return dataList;
        }

        // GET: api/RoadMap/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoadMap>> GetRoadMap(Guid id)
        {
            var roadMap = await _uow.RoadMaps.FirstOrDefaultAsync(User.GetUserId(),id);
            
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

            var dbRow = await _uow.RoadMaps.FirstOrDefaultAsync(User.GetUserId(),id);
            
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
        public async Task<ActionResult<RoadMap>> PostRoadMap(RoadMap roadMap)
        {
            var dbRow = new RoadMap()
            {
                AppUserId = User.GetUserId(),
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
            var roadMap = await _uow.RoadMaps.FirstOrDefaultAsync(User.GetUserId(),id);
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

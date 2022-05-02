#nullable enable
using BLL.App.Contracts;
using BLL.App.DTO;
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
        private readonly IAppBLL _bll;

        public RoadMapController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/RoadMap
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoadMap>>> GetRoadMaps()
        {
            var dataList = (await _bll.RoadMaps
                    .GetAllAsync(User.GetUserId()))
                .ToList();
            return dataList;
        }

        // GET: api/RoadMap/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoadMap>> GetRoadMap(Guid id)
        {
            var roadMap = await _bll.RoadMaps.FirstOrDefaultAsync(User.GetUserId(),id);
            
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

            var dbRow = await _bll.RoadMaps.FirstOrDefaultAsync(User.GetUserId(),id);
            
            if (dbRow == null) {return NotFound();}

            dbRow.Name!.SetTranslation(roadMap.Name!);
            dbRow.Position!.SetTranslation(roadMap.Position!);
            dbRow.Line!.SetTranslation(roadMap.Line!);

            _bll.RoadMaps.ModifyState(dbRow);

            try
            {
                await _bll.SaveChangesAsync();
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
            
            _bll.RoadMaps.Add(dbRow);
            
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetRoadMap", new { id = roadMap.Id }, roadMap);
        }

        // DELETE: api/RoadMap/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoadMap(Guid id)
        {
            var roadMap = await _bll.RoadMaps.FirstOrDefaultAsync(User.GetUserId(),id);
            if (roadMap == null)
            {
                return NotFound();
            }

            _bll.RoadMaps.Remove(roadMap);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool RoadMapExists(Guid id)
        {
            return _bll.RoadMaps.Exists(id);
        }
    }
}

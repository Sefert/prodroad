#nullable enable
using DAL.App.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.App;
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceGroupController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public PriceGroupController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/PriceGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriceGroupDTO>>> GetPriceGroups()
        {
            var dataList = (await _uow.PriceGroups
                    .GetAllAsync())
                .Select(row => new PriceGroupDTO()
                {
                    Id = row.Id,
                    AppUserId = row.AppUserId,
                    Name = row.Name,
                    PriceGroupId = row.PriceGroupId,
                    Tax = row.Tax,
                    Margin = row.Margin,
                    Discount = row.Discount
                })
                .ToList();
            return dataList;
        }

        // GET: api/PriceGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PriceGroupDTO>> GetPriceGroup(Guid id)
        {
            var dbRow = await _uow.PriceGroups.FirstOrDefaultAsync(id);
            
            if (dbRow == null)
            {
                return NotFound();
            }
            
            var priceGroup = new PriceGroupDTO()
            {
                Id = dbRow.Id,
                AppUserId = dbRow.AppUserId,
                Name = dbRow.Name,
                PriceGroupId = dbRow.PriceGroupId,
                Tax = dbRow.Tax,
                Margin = dbRow.Margin,
                Discount = dbRow.Discount
            };

            return priceGroup;
        }

        // PUT: api/PriceGroup/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPriceGroup(Guid id, PriceGroupDTO priceGroup)
        {
            if (id != priceGroup.Id)
            {
                return BadRequest();
            }

            var dbRow = await _uow.PriceGroups.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            dbRow.Name!.SetTranslation(priceGroup.Name!);

            _uow.PriceGroups.ModifyState(dbRow);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceGroupExists(id))
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

        // POST: api/PriceGroup
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PriceGroupDTO>> PostPriceGroup(PriceGroupDTO priceGroup)
        {
            var dbRow = new PriceGroup()
            {
                AppUserId = priceGroup.AppUserId,
                PriceGroupId = priceGroup.PriceGroupId,
                Tax = priceGroup.Tax,
                Margin = priceGroup.Margin,
                Discount = priceGroup.Discount
            };
            
            dbRow.Name!.SetTranslation(priceGroup.Name!);
            
            _uow.PriceGroups.Add(dbRow);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetPriceGroup", new { id = priceGroup.Id }, priceGroup);
        }

        // DELETE: api/PriceGroup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriceGroup(Guid id)
        {
            var priceGroup = await _uow.PriceGroups.FirstOrDefaultAsync(id);
            if (priceGroup == null)
            {
                return NotFound();
            }

            _uow.PriceGroups.Remove(priceGroup);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool PriceGroupExists(Guid id)
        {
            return _uow.PriceGroups.Exists(id);
        }
    }
}

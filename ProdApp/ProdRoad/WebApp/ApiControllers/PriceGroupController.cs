#nullable enable
using BLL.App.Contracts;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceGroupController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public PriceGroupController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/PriceGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriceGroup>>> GetPriceGroups()
        {
            var dataList = (await _bll.PriceGroups
                    .GetAllAsync())
                .ToList();
            return dataList;
        }

        // GET: api/PriceGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PriceGroup>> GetPriceGroup(Guid id)
        {
            var priceGroup = await _bll.PriceGroups.FirstOrDefaultAsync(id);
            
            if (priceGroup== null)
            {
                return NotFound();
            }

            return priceGroup;
        }

        // PUT: api/PriceGroup/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPriceGroup(Guid id, PriceGroup priceGroup)
        {
            if (id != priceGroup.Id)
            {
                return BadRequest();
            }

            var dbRow = await _bll.PriceGroups.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            dbRow.Name!.SetTranslation(priceGroup.Name!);

            _bll.PriceGroups.ModifyState(dbRow);

            try
            {
                await _bll.SaveChangesAsync();
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
        public async Task<ActionResult<PriceGroup>> PostPriceGroup(PriceGroup priceGroup)
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
            
            _bll.PriceGroups.Add(dbRow);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPriceGroup", new { id = priceGroup.Id }, priceGroup);
        }

        // DELETE: api/PriceGroup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriceGroup(Guid id)
        {
            var priceGroup = await _bll.PriceGroups.FirstOrDefaultAsync(id);
            if (priceGroup == null)
            {
                return NotFound();
            }

            _bll.PriceGroups.Remove(priceGroup);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool PriceGroupExists(Guid id)
        {
            return _bll.PriceGroups.Exists(id);
        }
    }
}

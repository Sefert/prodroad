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
    [Authorize(Roles="admin,manager,user",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ItemController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            var dataList = (await _bll.Items
                    .GetAllAsync(User.GetUserId()))
                .ToList();
            return dataList;
        }

        // GET: api/Item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(Guid id)
        {
            var item = await _bll.Items.FirstOrDefaultAsync(User.GetUserId(),id);
            
            if (item== null)
            {
                return NotFound();
            }
            
            return item;
        }

        // PUT: api/Item/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(Guid id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var dbRow = await _bll.Items.FirstOrDefaultAsync(User.GetUserId(),id);
            
            if (dbRow == null) {return NotFound();}

            dbRow.Name!.SetTranslation(item.Name!);
            dbRow.Type!.SetTranslation(item.Type!);
            dbRow.Unit!.SetTranslation(item.Unit!);

            _bll.Items.ModifyState(dbRow);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Item
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            var dbRow = new Item()
            {
                AppUserId = item.AppUserId,
                Quantity = item.Quantity
            };
            
            dbRow.Name!.SetTranslation(item.Name!);
            dbRow.Type!.SetTranslation(item.Type!);
            dbRow.Unit!.SetTranslation(item.Unit!);
            
            _bll.Items.Add(dbRow);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _bll.Items.FirstOrDefaultAsync(User.GetUserId(),id);
            if (item == null)
            {
                return NotFound();
            }

            _bll.Items.Remove(item);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(Guid id)
        {
            return _bll.Items.Exists(id);
        }
    }
}

#nullable enable
using BLL.App.Contracts;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedureController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ProcedureController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Procedure
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Procedure>>> GetProcedures()
        {
            var dataList = (await _bll.Procedures
                    .GetAllAsync())
                .ToList();
            return dataList;
        }

        // GET: api/Procedure/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Procedure>> GetProcedure(Guid id)
        {
            var procedure  = await _bll.Procedures.FirstOrDefaultAsync(id);
            
            if (procedure == null)
            {
                return NotFound();
            }

            return procedure;
        }

        // PUT: api/Procedure/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcedure(Guid id, Procedure procedure)
        {
            if (id != procedure.Id)
            {
                return BadRequest();
            }

            var dbRow = await _bll.Procedures.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            dbRow.Name!.SetTranslation(procedure.Name!);
            dbRow.Code!.SetTranslation(procedure.Code!);
            
            _bll.Procedures.ModifyState(dbRow);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcedureExists(id))
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

        // POST: api/Procedure
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Procedure>> PostProcedure(Procedure procedure)
        {
            var dbRow = new Procedure()
            {
                AppUserId = procedure.AppUserId,
            };
            
            dbRow.Name!.SetTranslation(procedure.Name!);
            dbRow.Code!.SetTranslation(procedure.Code!);
            
            _bll.Procedures.Add(dbRow);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetProcedure", new { id = procedure.Id }, procedure);
        }

        // DELETE: api/Procedure/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcedure(Guid id)
        {
            var procedure = await _bll.Procedures.FirstOrDefaultAsync(id);
            if (procedure == null)
            {
                return NotFound();
            }

            _bll.Procedures.Remove(procedure);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool ProcedureExists(Guid id)
        {
            return _bll.Procedures.Exists(id);
        }
    }
}

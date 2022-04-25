#nullable enable
using DAL.App.Contracts;
using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedureController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public ProcedureController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Procedure
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Procedure>>> GetProcedures()
        {
            var dataList = (await _uow.Procedures
                    .GetAllAsync())
                .ToList();
            return dataList;
        }

        // GET: api/Procedure/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Procedure>> GetProcedure(Guid id)
        {
            var procedure  = await _uow.Procedures.FirstOrDefaultAsync(id);
            
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

            var dbRow = await _uow.Procedures.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            dbRow.Name!.SetTranslation(procedure.Name!);
            dbRow.Code!.SetTranslation(procedure.Code!);
            
            _uow.Procedures.ModifyState(dbRow);

            try
            {
                await _uow.SaveChangesAsync();
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
            
            _uow.Procedures.Add(dbRow);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetProcedure", new { id = procedure.Id }, procedure);
        }

        // DELETE: api/Procedure/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcedure(Guid id)
        {
            var procedure = await _uow.Procedures.FirstOrDefaultAsync(id);
            if (procedure == null)
            {
                return NotFound();
            }

            _uow.Procedures.Remove(procedure);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool ProcedureExists(Guid id)
        {
            return _uow.Procedures.Exists(id);
        }
    }
}

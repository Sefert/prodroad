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
    public class ProcedureController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public ProcedureController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Procedure
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcedureDTO>>> GetProcedures()
        {
            var dataList = (await _uow.Procedures
                    .GetAllAsync())
                .Select(row => new ProcedureDTO()
                {
                    Id = row.Id,
                    AppUserId = row.AppUserId,
                    Name = row.Name,
                    Code = row.Code
                })
                .ToList();
            return dataList;
        }

        // GET: api/Procedure/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcedureDTO>> GetProcedure(Guid id)
        {
            var dbRow = await _uow.Procedures.FirstOrDefaultAsync(id);
            
            if (dbRow == null)
            {
                return NotFound();
            }
            
            var procedure = new ProcedureDTO()
            {
                Id = dbRow.Id,
                AppUserId = dbRow.AppUserId,
                Name = dbRow.Name,
                Code = dbRow.Code
            };

            return procedure;
        }

        // PUT: api/Procedure/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcedure(Guid id, ProcedureDTO procedure)
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
        public async Task<ActionResult<ProcedureDTO>> PostProcedure(ProcedureDTO procedure)
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

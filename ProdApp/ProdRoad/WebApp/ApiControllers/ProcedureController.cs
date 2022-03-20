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
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedureController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProcedureController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Procedure
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcedureDTO>>> GetProcedures()
        {
            var dataList = (await _context.Procedures
                    .ToListAsync())
                .Select(row => new ProcedureDTO()
                {
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
            var dbRow = await _context.Procedures.FindAsync(id);
            
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

            var dbRow = await _context.Procedures.FindAsync(id);
            
            if (dbRow == null) {return NotFound();}

            dbRow.Name!.SetTranslation(procedure.Name!);
            dbRow.Code!.SetTranslation(procedure.Code!);
            
            _context.Entry(procedure).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
            
            _context.Procedures.Add(dbRow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProcedure", new { id = procedure.Id }, procedure);
        }

        // DELETE: api/Procedure/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcedure(Guid id)
        {
            var procedure = await _context.Procedures.FindAsync(id);
            if (procedure == null)
            {
                return NotFound();
            }

            _context.Procedures.Remove(procedure);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProcedureExists(Guid id)
        {
            return _context.Procedures.Any(e => e.Id == id);
        }
    }
}

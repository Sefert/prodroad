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
    public class ProcessController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProcessController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Process
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcessDTO>>> GetProcesses()
        {
            var dataList = (await _context.Processes
                    .ToListAsync())
                .Select(row => new ProcessDTO()
                {
                    TeamId = row.TeamId,
                    RoadMapId = row.RoadMapId,
                    ProcedureId = row.ProcedureId,
                    CreatedAmount = row.CreatedAmount
                })
                .ToList();
            return dataList;
        }

        // GET: api/Process/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessDTO>> GetProcess(Guid id)
        {
            var dbRow = await _context.Processes.FindAsync(id);
            
            if (dbRow == null)
            {
                return NotFound();
            }
            
            var process = new ProcessDTO()
            {
                Id = dbRow.Id,
                TeamId = dbRow.TeamId,
                RoadMapId = dbRow.RoadMapId,
                ProcedureId = dbRow.ProcedureId,
                CreatedAmount = dbRow.CreatedAmount
            };

            return process;
        }

        // PUT: api/Process/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcess(Guid id, ProcessDTO process)
        {
            if (id != process.Id)
            {
                return BadRequest();
            }

            var dbRow = await _context.Processes.FindAsync(id);
            
            if (dbRow == null) {return NotFound();}

            _context.Entry(process).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcessExists(id))
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

        // POST: api/Process
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProcessDTO>> PostProcess(ProcessDTO process)
        {
            var dbRow = new Process()
            {
                TeamId = process.TeamId,
                RoadMapId = process.RoadMapId,
                ProcedureId = process.ProcedureId,
                CreatedAmount = process.CreatedAmount
            };
            
            _context.Processes.Add(dbRow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProcess", new { id = process.Id }, process);
        }

        // DELETE: api/Process/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcess(Guid id)
        {
            var process = await _context.Processes.FindAsync(id);
            if (process == null)
            {
                return NotFound();
            }

            _context.Processes.Remove(process);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProcessExists(Guid id)
        {
            return _context.Processes.Any(e => e.Id == id);
        }
    }
}

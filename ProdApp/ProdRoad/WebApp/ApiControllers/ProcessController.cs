#nullable enable
using DAL.App.Contracts;
using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public ProcessController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Process
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Process>>> GetProcesses()
        {
            var dataList = (await _uow.Processes
                    .GetAllAsync())
                .ToList();
            return dataList;
        }

        // GET: api/Process/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Process>> GetProcess(Guid id)
        {
            var process = await _uow.Processes.FirstOrDefaultAsync(id);
            
            if (process == null)
            {
                return NotFound();
            }

            return process;
        }

        // PUT: api/Process/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcess(Guid id, Process process)
        {
            if (id != process.Id)
            {
                return BadRequest();
            }

            var dbRow = await _uow.Processes.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            _uow.Processes.ModifyState(dbRow);

            try
            {
                await _uow.SaveChangesAsync();
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
        public async Task<ActionResult<Process>> PostProcess(Process process)
        {
            var dbRow = new Process()
            {
                TeamId = process.TeamId,
                RoadMapId = process.RoadMapId,
                ProcedureId = process.ProcedureId,
                CreatedAmount = process.CreatedAmount
            };
            
            _uow.Processes.Add(dbRow);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetProcess", new { id = process.Id }, process);
        }

        // DELETE: api/Process/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcess(Guid id)
        {
            var process = await _uow.Processes.FirstOrDefaultAsync(id);
            if (process == null)
            {
                return NotFound();
            }

            _uow.Processes.Remove(process);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool ProcessExists(Guid id)
        {
            return _uow.Procedures.Exists(id);
        }
    }
}

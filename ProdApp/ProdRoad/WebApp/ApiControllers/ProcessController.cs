#nullable enable

using BLL.App.Contracts;
using BLL.App.DTO;
using DAL.App.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ProcessController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Process
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Process>>> GetProcesses()
        {
            var dataList = (await _bll.Processes
                    .GetAllAsync())
                .ToList();
            return dataList;
        }

        // GET: api/Process/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Process>> GetProcess(Guid id)
        {
            var process = await _bll.Processes.FirstOrDefaultAsync(id);
            
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

            var dbRow = await _bll.Processes.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            _bll.Processes.ModifyState(dbRow);

            try
            {
                await _bll.SaveChangesAsync();
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
            
            _bll.Processes.Add(dbRow);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetProcess", new { id = process.Id }, process);
        }

        // DELETE: api/Process/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcess(Guid id)
        {
            var process = await _bll.Processes.FirstOrDefaultAsync(id);
            if (process == null)
            {
                return NotFound();
            }

            _bll.Processes.Remove(process);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool ProcessExists(Guid id)
        {
            return _bll.Procedures.Exists(id);
        }
    }
}

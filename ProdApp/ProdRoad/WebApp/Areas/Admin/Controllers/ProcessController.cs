#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App;
using Domain.App;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProcessController : Controller
    {
        private readonly AppDbContext _context;

        public ProcessController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Process
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Processes.Include(p => p.Procedure).Include(p => p.RoadMap).Include(p => p.Team);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Process/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var process = await _context.Processes
                .Include(p => p.Procedure)
                .Include(p => p.RoadMap)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (process == null)
            {
                return NotFound();
            }

            return View(process);
        }

        // GET: Admin/Process/Create
        public IActionResult Create()
        {
            ViewData["ProcedureId"] = new SelectList(_context.Procedures, "Id", "Code");
            ViewData["RoadMapId"] = new SelectList(_context.RoadMaps, "Id", "Name");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Code");
            return View();
        }

        // POST: Admin/Process/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,RoadMapId,ProcedureId,CreatedAmount,StartAt,EndAt,RealStartAt,RealEndAt,UpdatedAt,UpdatedId,Id")] Process process)
        {
            if (ModelState.IsValid)
            {
                process.Id = Guid.NewGuid();
                _context.Add(process);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProcedureId"] = new SelectList(_context.Procedures, "Id", "Code", process.ProcedureId);
            ViewData["RoadMapId"] = new SelectList(_context.RoadMaps, "Id", "Name", process.RoadMapId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Code", process.TeamId);
            return View(process);
        }

        // GET: Admin/Process/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var process = await _context.Processes.FindAsync(id);
            if (process == null)
            {
                return NotFound();
            }
            ViewData["ProcedureId"] = new SelectList(_context.Procedures, "Id", "Code", process.ProcedureId);
            ViewData["RoadMapId"] = new SelectList(_context.RoadMaps, "Id", "Name", process.RoadMapId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Code", process.TeamId);
            return View(process);
        }

        // POST: Admin/Process/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TeamId,RoadMapId,ProcedureId,CreatedAmount,StartAt,EndAt,RealStartAt,RealEndAt,UpdatedAt,UpdatedId,Id")] Process process)
        {
            if (id != process.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(process);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcessExists(process.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProcedureId"] = new SelectList(_context.Procedures, "Id", "Code", process.ProcedureId);
            ViewData["RoadMapId"] = new SelectList(_context.RoadMaps, "Id", "Name", process.RoadMapId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Code", process.TeamId);
            return View(process);
        }

        // GET: Admin/Process/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var process = await _context.Processes
                .Include(p => p.Procedure)
                .Include(p => p.RoadMap)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (process == null)
            {
                return NotFound();
            }

            return View(process);
        }

        // POST: Admin/Process/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var process = await _context.Processes.FindAsync(id);
            _context.Processes.Remove(process);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcessExists(Guid id)
        {
            return _context.Processes.Any(e => e.Id == id);
        }
    }
}

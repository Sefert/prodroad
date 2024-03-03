using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class ProcessController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProcessController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Process
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Processes.Include(p => p.RoadMap).Include(p => p.Team);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Process/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var process = await _context.Processes
                .Include(p => p.RoadMap)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (process == null)
            {
                return NotFound();
            }

            return View(process);
        }

        // GET: Process/Create
        public IActionResult Create()
        {
            ViewData["RoadMapId"] = new SelectList(_context.RoadMaps, "Id", "Id");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id");
            return View();
        }

        // POST: Process/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoadMapId,TeamId,StartAt,EndAt,RealStartAt,RealEndAt,UpdatedAt,Id")] Process process)
        {
            if (ModelState.IsValid)
            {
                process.Id = Guid.NewGuid();
                _context.Add(process);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoadMapId"] = new SelectList(_context.RoadMaps, "Id", "Id", process.RoadMapId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", process.TeamId);
            return View(process);
        }

        // GET: Process/Edit/5
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
            ViewData["RoadMapId"] = new SelectList(_context.RoadMaps, "Id", "Id", process.RoadMapId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", process.TeamId);
            return View(process);
        }

        // POST: Process/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RoadMapId,TeamId,StartAt,EndAt,RealStartAt,RealEndAt,UpdatedAt,Id")] Process process)
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
            ViewData["RoadMapId"] = new SelectList(_context.RoadMaps, "Id", "Id", process.RoadMapId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", process.TeamId);
            return View(process);
        }

        // GET: Process/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var process = await _context.Processes
                .Include(p => p.RoadMap)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (process == null)
            {
                return NotFound();
            }

            return View(process);
        }

        // POST: Process/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var process = await _context.Processes.FindAsync(id);
            if (process != null)
            {
                _context.Processes.Remove(process);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcessExists(Guid id)
        {
            return _context.Processes.Any(e => e.Id == id);
        }
    }
}

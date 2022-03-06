#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoadMapController : Controller
    {
        private readonly AppDbContext _context;

        public RoadMapController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/RoadMap
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.RoadMaps.Include(r => r.AppUser);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/RoadMap/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roadMap = await _context.RoadMaps
                .Include(r => r.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roadMap == null)
            {
                return NotFound();
            }

            return View(roadMap);
        }

        // GET: Admin/RoadMap/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/RoadMap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,Name,Position,Line,Id")] RoadMap roadMap)
        {
            if (ModelState.IsValid)
            {
                roadMap.Id = Guid.NewGuid();
                _context.Add(roadMap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", roadMap.AppUserId);
            return View(roadMap);
        }

        // GET: Admin/RoadMap/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roadMap = await _context.RoadMaps.FindAsync(id);
            if (roadMap == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", roadMap.AppUserId);
            return View(roadMap);
        }

        // POST: Admin/RoadMap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,Name,Position,Line,Id")] RoadMap roadMap)
        {
            if (id != roadMap.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roadMap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoadMapExists(roadMap.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", roadMap.AppUserId);
            return View(roadMap);
        }

        // GET: Admin/RoadMap/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roadMap = await _context.RoadMaps
                .Include(r => r.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roadMap == null)
            {
                return NotFound();
            }

            return View(roadMap);
        }

        // POST: Admin/RoadMap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var roadMap = await _context.RoadMaps.FindAsync(id);
            _context.RoadMaps.Remove(roadMap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoadMapExists(Guid id)
        {
            return _context.RoadMaps.Any(e => e.Id == id);
        }
    }
}

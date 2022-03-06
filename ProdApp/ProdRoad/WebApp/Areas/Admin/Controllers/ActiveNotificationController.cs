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
    public class ActiveNotificationController : Controller
    {
        private readonly AppDbContext _context;

        public ActiveNotificationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ActiveNotification
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ActiveNotifications.Include(a => a.Process).Include(a => a.Team).Include(a => a.UserNotification);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/ActiveNotification/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activeNotification = await _context.ActiveNotifications
                .Include(a => a.Process)
                .Include(a => a.Team)
                .Include(a => a.UserNotification)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activeNotification == null)
            {
                return NotFound();
            }

            return View(activeNotification);
        }

        // GET: Admin/ActiveNotification/Create
        public IActionResult Create()
        {
            ViewData["ProcessId"] = new SelectList(_context.Processes, "Id", "Id");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Code");
            ViewData["UserNotificationId"] = new SelectList(_context.UserNotifications, "Id", "Color");
            return View();
        }

        // POST: Admin/ActiveNotification/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProcessId,UserNotificationId,TeamId,Head,Info,Active,CreatedAt,CreatedId,UpdatedAt,UpdatedId,Id")] ActiveNotification activeNotification)
        {
            if (ModelState.IsValid)
            {
                activeNotification.Id = Guid.NewGuid();
                _context.Add(activeNotification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProcessId"] = new SelectList(_context.Processes, "Id", "Id", activeNotification.ProcessId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Code", activeNotification.TeamId);
            ViewData["UserNotificationId"] = new SelectList(_context.UserNotifications, "Id", "Color", activeNotification.UserNotificationId);
            return View(activeNotification);
        }

        // GET: Admin/ActiveNotification/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activeNotification = await _context.ActiveNotifications.FindAsync(id);
            if (activeNotification == null)
            {
                return NotFound();
            }
            ViewData["ProcessId"] = new SelectList(_context.Processes, "Id", "Id", activeNotification.ProcessId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Code", activeNotification.TeamId);
            ViewData["UserNotificationId"] = new SelectList(_context.UserNotifications, "Id", "Color", activeNotification.UserNotificationId);
            return View(activeNotification);
        }

        // POST: Admin/ActiveNotification/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProcessId,UserNotificationId,TeamId,Head,Info,Active,CreatedAt,CreatedId,UpdatedAt,UpdatedId,Id")] ActiveNotification activeNotification)
        {
            if (id != activeNotification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activeNotification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActiveNotificationExists(activeNotification.Id))
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
            ViewData["ProcessId"] = new SelectList(_context.Processes, "Id", "Id", activeNotification.ProcessId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Code", activeNotification.TeamId);
            ViewData["UserNotificationId"] = new SelectList(_context.UserNotifications, "Id", "Color", activeNotification.UserNotificationId);
            return View(activeNotification);
        }

        // GET: Admin/ActiveNotification/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activeNotification = await _context.ActiveNotifications
                .Include(a => a.Process)
                .Include(a => a.Team)
                .Include(a => a.UserNotification)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activeNotification == null)
            {
                return NotFound();
            }

            return View(activeNotification);
        }

        // POST: Admin/ActiveNotification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var activeNotification = await _context.ActiveNotifications.FindAsync(id);
            _context.ActiveNotifications.Remove(activeNotification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActiveNotificationExists(Guid id)
        {
            return _context.ActiveNotifications.Any(e => e.Id == id);
        }
    }
}

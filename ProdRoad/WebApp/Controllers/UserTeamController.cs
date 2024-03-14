using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class UserTeamController : Controller
    {
        private readonly AppDbContext _context;

        public UserTeamController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserTeam
        public async Task<IActionResult> Index()
        {
            var AppDbContext = _context.UserTeams.Include(u => u.AppUser).Include(u => u.Team);
            return View(await AppDbContext.ToListAsync());
        }

        // GET: UserTeam/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTeam = await _context.UserTeams
                .Include(u => u.AppUser)
                .Include(u => u.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTeam == null)
            {
                return NotFound();
            }

            return View(userTeam);
        }

        // GET: UserTeam/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id");
            return View();
        }

        // POST: UserTeam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,TeamId,Id")] UserTeam userTeam)
        {
            if (ModelState.IsValid)
            {
                userTeam.Id = Guid.NewGuid();
                _context.Add(userTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userTeam.AppUserId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", userTeam.TeamId);
            return View(userTeam);
        }

        // GET: UserTeam/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTeam = await _context.UserTeams.FindAsync(id);
            if (userTeam == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userTeam.AppUserId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", userTeam.TeamId);
            return View(userTeam);
        }

        // POST: UserTeam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,TeamId,Id")] UserTeam userTeam)
        {
            if (id != userTeam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTeamExists(userTeam.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userTeam.AppUserId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", userTeam.TeamId);
            return View(userTeam);
        }

        // GET: UserTeam/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTeam = await _context.UserTeams
                .Include(u => u.AppUser)
                .Include(u => u.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTeam == null)
            {
                return NotFound();
            }

            return View(userTeam);
        }

        // POST: UserTeam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userTeam = await _context.UserTeams.FindAsync(id);
            if (userTeam != null)
            {
                _context.UserTeams.Remove(userTeam);
            }

            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTeamExists(Guid id)
        {
            return _context.UserTeams.Any(e => e.Id == id);
        }
    }
}

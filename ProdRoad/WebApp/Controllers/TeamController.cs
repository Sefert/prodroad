using App.Contracts.DAL;
using App.DAL.EF;
using App.DAL.EF.Repositories;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ITeamRepository _repo;

        public TeamController(AppDbContext context)
        {
            _context = context;
            _repo = new TeamRepository(_context);
        }

        // GET: Team
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllAsync());
        }

        // GET: Team/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _repo.FirstOrDefaultAsync(id.Value);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Team/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList( _context.Users, "Id", "Id");
            return View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team)
        {
            if (ModelState.IsValid)
            {
                team.Id = Guid.NewGuid();
                _repo.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", team.AppUserId);
            return View(team);
        }

        // GET: Team/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _repo.FirstOrDefaultAsync(id.Value);
            if (team == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", team.AppUserId);
            return View(team);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TeamExists(team.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", team.AppUserId);
            return View(team);
        }

        // GET: Team/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _repo.FirstOrDefaultAsync(id.Value);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var team = await _repo.FirstOrDefaultAsync(id);
            if (team != null)
            {
                _repo.Remove(id);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TeamExists(Guid id)
        {
            return await _repo.ExistsAsync(id);
        }
    }
}

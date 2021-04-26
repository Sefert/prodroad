using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Extensions.Base;

namespace WebApp.Controllers
{
    [Authorize]
    public class TeamsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public TeamsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Teams.GetAllAsync(User.GetUserId()!.Value));
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var team = await _uow.Teams.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value, false);

            if (team == null) return NotFound();
            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team)
        {
            team.StartDate = DateTime.Now;
            if (!ModelState.IsValid) return View(team);
            //team.Id = Guid.NewGuid();
            _uow.Teams.Add(team);
            _uow.UserTeams.Add(new UserTeam
                {
                    AppUserId = User.GetUserId()!.Value,
                    TeamId = team.Id,
                    Team = team,
                    StartDate = DateTime.Now
                }
            );
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var team = await _uow.Teams.FirstOrDefaultAsync(id.Value,User.GetUserId()!.Value, false);

            if (team == null) return NotFound();
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, Team team)
        {
            if (id != team.Id) return NotFound();

            if (!ModelState.IsValid || !await _uow.Teams.ExistsAsync(team.Id))
                return View(team);
            
            _uow.Teams.Update(team);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var team = await _uow.Teams.FirstOrDefaultAsync(id.Value);

            if (team == null) return NotFound();

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Teams.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

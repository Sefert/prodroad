using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain.App;

namespace WebApp.Controllers
{
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
            return View(await _uow.Teams.GetAllAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var team = await _uow.Teams.FirstOrDefaultAsync(id.Value);

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
        public async Task<IActionResult> Create([Bind("Name,Code,StartDate,EndDate,Id")] Team team)
        {
            if (ModelState.IsValid)
            {
                team.Id = Guid.NewGuid();
                _uow.Teams.Add(team);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var team = await _uow.Teams.FirstOrDefaultAsync(id.Value);

            if (team == null) return NotFound();
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Code,StartDate,EndDate,Id")] Team team)
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
            var team = await _uow.Teams.FirstOrDefaultAsync(id);
            if (team == null) return NotFound();
            _uow.Teams.Remove(team);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

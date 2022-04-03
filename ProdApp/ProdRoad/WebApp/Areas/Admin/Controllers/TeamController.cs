#nullable enable
using DAL.App.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="admin,manager")]
    public class TeamController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public TeamController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Admin/Team
        public async Task<IActionResult> Index()
        {
            
            var dataList = await _uow.Teams.GetAllAsync(User.GetUserId());
            return View(dataList);
        }

        // GET: Admin/Team/Details/5
        public async Task<IActionResult> Details(Guid id)
        {

            var team = await _uow.Teams.FirstOrDefaultAsync(User.GetUserId(),id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Admin/Team/Create
        public IActionResult Create()
        {
            //ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/Team/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( /*[Bind("Name,Code")]*/ Team team)
        {
            if (ModelState.IsValid)
            {
                team.AppUserId = User.GetUserId();
                //team.Id = Guid.NewGuid();
                team.Name.SetTranslation(team.Name);
                team.Code.SetTranslation(team.Code);
                _uow.Teams.Add(team);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(team);
            //ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", team.AppUserId);
        }

        // GET: Admin/Team/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var team = await _uow.Teams.FirstOrDefaultAsync(User.GetUserId(),id);
            if (team == null)
            {
                return NotFound();
            }
            //ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", team.AppUserId);
            return View(team);
        }

        // POST: Admin/Team/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, /*[Bind("Name,Code")]*/ Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(team);
            try
            {
                team.AppUserId = User.GetUserId();
                _uow.Teams.Update(team);
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Teams.ExistsAsync(team.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            //ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", team.AppUserId);
        }

        // GET: Admin/Team/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var team = await _uow.Teams.FirstOrDefaultAsync(User.GetUserId(),id, false);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        //TODO:make more efficient
        // POST: Admin/Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var team = await _uow.Teams.FirstOrDefaultAsync(User.GetUserId(),id);
            if (team == null) return NotFound();
            await _uow.Teams.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

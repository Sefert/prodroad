using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using WebApp.Helpers;

namespace WebApp.Controllers
{
    [Authorize]
    public class UserTeamsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public UserTeamsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: UserTeams
        public async Task<IActionResult> Index()
        {
            return View(await _uow.UserTeams.GetAllAsync(User.GetUserId()!.Value));
        }

        // GET: UserTeams/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var userTeam = await _uow.UserTeams.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value, false);

            if (userTeam == null) return NotFound();
            return View(userTeam);
        }

        // GET: UserTeams/Create
        public async Task<IActionResult> Create()
        {
            ViewData["TeamId"] = new SelectList(await _uow.Teams.GetAllAsync(User.GetUserId()!.Value), "Id", "Code");
            return View();
        }

        // POST: UserTeams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserTeam userTeam)
        {
            var uId = User.GetUserId()!.Value;
            if (ModelState.IsValid)
            {
                userTeam.AppUserId = User.GetUserId()!.Value;
                userTeam.Id = Guid.NewGuid();
                _uow.UserTeams.Add(userTeam);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(await _uow.Teams.GetAllAsync(User.GetUserId()!.Value), "Id", "Code", userTeam.TeamId);
            return View(userTeam);
        }

        // GET: UserTeams/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var uId = User.GetUserId()!.Value;

            var userTeam = await _uow.UserTeams.FirstOrDefaultAsync(id.Value, uId, false);

            if (userTeam == null) return NotFound();

            ViewData["TeamId"] = new SelectList(await _uow.Teams.GetAllAsync(uId), "Id", "Code", userTeam.TeamId);
            return View(userTeam);
        }

        // POST: UserTeams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MasterTeam,Accepted,ApplicationUserId,TeamId,StartDate,EndDate,Id")] UserTeam userTeam)
        {
            if (id != userTeam.Id) return NotFound();
            var uId = User.GetUserId()!.Value;

            if (!ModelState.IsValid || !await _uow.UserTeams.ExistsAsync(userTeam.Id, uId))
                return View(userTeam);

            ViewData["TeamId"] = new SelectList(await _uow.Teams.GetAllAsync(uId), "Id", "Code", userTeam.TeamId);

            _uow.UserTeams.Update(userTeam);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: UserTeams/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var userTeam = await _uow.UserTeams.FirstOrDefaultAsync(id.Value,User.GetUserId()!.Value, false);

            if (userTeam == null) return NotFound();
            return View(userTeam);
        }

        // POST: UserTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var uId = User.GetUserId()!.Value;
            var userTeam = await _uow.UserTeams.FirstOrDefaultAsync(id, uId);
            if (userTeam == null) return NotFound();
            await _uow.UserTeams.RemoveAsync(id, uId);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

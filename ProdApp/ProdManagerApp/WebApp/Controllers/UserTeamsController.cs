using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;

namespace WebApp.Controllers
{
    public class UserTeamsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserTeamRepo _repo;

        public UserTeamsController(AppDbContext context)
        {
            _context = context;
            _repo = new UserTeamRepo(context);
        }

        // GET: UserTeams
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllAsync());
        }

        // GET: UserTeams/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _repo.FirstOrDefaultAsync((Guid) id));
        }

        // GET: UserTeams/Create
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Code");
            return View();
        }

        // POST: UserTeams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MasterTeam,Accepted,ApplicationUserId,TeamId,StartDate,EndDate,Id")] UserTeam userTeam)
        {
            if (ModelState.IsValid)
            {
                userTeam.Id = Guid.NewGuid();
                _repo.Add(userTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_repo.GetAllAsync().Result, "Id", "Code", userTeam.TeamId);
            return View(userTeam);
        }

        // GET: UserTeams/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTeam = await _repo.FirstOrDefaultAsync((Guid) id);

            ViewData["TeamId"] = new SelectList(_repo.GetAllAsync().Result, "Id", "Code", userTeam.TeamId);
            return View(userTeam);
        }

        // POST: UserTeams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MasterTeam,Accepted,ApplicationUserId,TeamId,StartDate,EndDate,Id")] UserTeam userTeam)
        {
            if (id != userTeam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(userTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repo.ExistsAsync(id).Result)
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
            ViewData["TeamId"] = new SelectList(_repo.GetAllAsync().Result, "Id", "Code", userTeam.TeamId);
            return View(userTeam);
        }

        // GET: UserTeams/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            return View(await _repo.FirstOrDefaultAsync((Guid) id));
        }

        // POST: UserTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userTeam = await _repo.FirstOrDefaultAsync(id);
            _repo.Remove(userTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

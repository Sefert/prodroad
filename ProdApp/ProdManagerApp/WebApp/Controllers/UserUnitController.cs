using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;
using Extensions.Base;

namespace WebApp.Controllers
{
    public class UserUnitController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public UserUnitController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: UserUnit
        public async Task<IActionResult> Index()
        {
            return View(await _uow.UserUnits.GetAllAsync(User.GetUserId()!.Value));
        }

        // GET: UserUnit/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var userUnit= await _uow.UserUnits.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value, false);

            if (userUnit == null) return NotFound();
            
            return View(userUnit);
        }

        // GET: UserUnit/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ComponentId"] = new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name");
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: UserUnit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserUnit userUnit)
        {
            if (ModelState.IsValid)
            {
                userUnit.Id = Guid.NewGuid();
                _uow.UserUnits.Add(userUnit);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name");
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name");
            return View(userUnit);
        }

        // GET: UserUnit/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var uId = User.GetUserId()!.Value;

            var userUnit  = await _uow.UserUnits.FirstOrDefaultAsync(id.Value, uId, false);

            if (userUnit == null) return NotFound();
            
            ViewData["ComponentId"] = new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name", userUnit.ComponentId);
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name", userUnit.ItemId);
            return View(userUnit);
        }

        // POST: UserUnit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserUnit userUnit)
        {
            if (id != userUnit.Id) return NotFound();
            var uId = User.GetUserId()!.Value;

            if (!ModelState.IsValid || !await _uow.UserTeams.ExistsAsync(userUnit.Id, uId))
            {
                ViewData["ComponentId"] = new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name", userUnit.ComponentId);
                ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name", userUnit.ItemId);
                return View(userUnit);
            }

            _uow.UserUnits.Update(userUnit);
            await _uow.SaveChangesAsync();

            return View(userUnit);
        }

        // GET: UserUnit/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var userUnit = await _uow.UserUnits.FirstOrDefaultAsync(id.Value,User.GetUserId()!.Value, false);

            if (userUnit == null) return NotFound();
            return View(userUnit);
        }

        // POST: UserUnit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var uId = User.GetUserId()!.Value;
            var userUnit = await _uow.UserUnits.FirstOrDefaultAsync(id, uId);
            if (userUnit== null) return NotFound();
            await _uow.UserUnits.RemoveAsync(id, uId);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

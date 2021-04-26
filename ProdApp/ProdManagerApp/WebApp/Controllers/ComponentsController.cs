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
    public class ComponentsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ComponentsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Components
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Components.GetAllAsync(User.GetUserId()!.Value));
        }

        // GET: Components/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var component = await _uow.Components.FirstOrDefaultAsync(id.Value,User.GetUserId()!.Value);
            
            if (component == null) return NotFound();

            return View(component);
        }

        // GET: Components/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Components/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Component component)
        {
            if (!ModelState.IsValid) return View(component);
            _uow.Components.Add(component);
            _uow.UserUnits.Add(new UserUnit
                {
                    AppUserId = User.GetUserId()!.Value,
                    ComponentId = component.Id,
                    Component = component,
                    StartDate = DateTime.Now
                }
            );
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Components/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var component = await _uow.Components
                .FirstOrDefaultAsync(id.Value,User.GetUserId()!.Value, false);

            if (component == null) return NotFound();

            return View(component);
        }

        // POST: Components/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,Component component)
        {
            if (id != component.Id) return NotFound();

            if (!ModelState.IsValid || !await _uow.Components.ExistsAsync(component.Id))
                return View(component);
            
            _uow.Components.Update(component);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Components/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var component = await _uow.Components.FirstOrDefaultAsync(id.Value,User.GetUserId()!.Value, false);

            if (component == null) return NotFound();
            
            return View(component);
        }

        // POST: Components/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var component = await _uow.Components.FirstOrDefaultAsync(id,User.GetUserId()!.Value);
            if (component == null) return NotFound();
            _uow.Components.Remove(component, User.GetUserId()!.Value);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

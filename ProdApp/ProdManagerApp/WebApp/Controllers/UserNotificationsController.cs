using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;

namespace WebApp.Controllers
{
    public class UserNotificationsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public UserNotificationsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: UserNotifications
        public async Task<IActionResult> Index()
        {
            return View(await _uow.UserNotifications.GetAllAsync());
        }

        // GET: UserNotifications/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var userNotification = await _uow.UserNotifications.FirstOrDefaultAsync(id.Value, false);

            if (userNotification == null) return NotFound();
            return View(userNotification);
        }

        // GET: UserNotifications/Create
        public async Task<IActionResult> Create()
        {
            ViewData["NotificationTypeId"] = new SelectList(await _uow.NotificationTypes.GetAllAsync(), "Id", "Type");
            return View();
        }

        // POST: UserNotifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Color,Active,NotificationTypeId,ApplicationUserId,Id")] UserNotification userNotification)
        {
            if (ModelState.IsValid)
            {
                userNotification.Id = Guid.NewGuid();
                _uow.UserNotifications.Add(userNotification);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NotificationTypeId"] = new SelectList(await _uow.NotificationTypes.GetAllAsync(), "Id", "Type", userNotification.NotificationTypeId);
            return View(userNotification);
        }

        // GET: UserNotifications/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var userNotification = await _uow.UserNotifications.FirstOrDefaultAsync(id.Value, false);

            if (userNotification == null) return NotFound();

            ViewData["NotificationTypeId"] = new SelectList(await _uow.NotificationTypes.GetAllAsync(), "Id", "Type",
                userNotification.NotificationTypeId);
            return View(userNotification);
        }

        // POST: UserNotifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Color,Active,NotificationTypeId,ApplicationUserId,Id")] UserNotification userNotification)
        {
            if (id != userNotification.Id) return NotFound();

            if (!ModelState.IsValid || !await _uow.UserNotifications.ExistsAsync(userNotification.Id))
                return View(userNotification);

            ViewData["NotificationTypeId"] = new SelectList(await _uow.NotificationTypes.GetAllAsync(), "Id", "Type",
                userNotification.NotificationTypeId);

            _uow.UserNotifications.Update(userNotification);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: UserNotifications/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            
            var userNotification = await _uow.UserNotifications.FirstOrDefaultAsync(id.Value, false);

            if (userNotification == null) return NotFound();
            return View(userNotification);
        }

        // POST: UserNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userNotification = await _uow.UserNotifications.FirstOrDefaultAsync(id);
            if (userNotification == null) return NotFound();
            _uow.UserNotifications.Remove(userNotification);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

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
    public class UserNotificationsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserNotificationRepo _repo;

        public UserNotificationsController(AppDbContext context)
        {
            _context = context;
            _repo = new UserNotificationRepo(context);
        }

        // GET: UserNotifications
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllAsync());
        }

        // GET: UserNotifications/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _repo.FirstOrDefaultAsync((Guid) id));
        }

        // GET: UserNotifications/Create
        public IActionResult Create()
        {
            ViewData["NotificationTypeId"] = new SelectList(_context.NotificationTypes, "Id", "Type");
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
                _repo.Add(userNotification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NotificationTypeId"] = new SelectList(_context.NotificationTypes, "Id", "Type", userNotification.NotificationTypeId);
            return View(userNotification);
        }

        // GET: UserNotifications/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userNotification = await _repo.FirstOrDefaultAsync((Guid) id);

            ViewData["NotificationTypeId"] = new SelectList(_context.NotificationTypes, "Id", "Type", userNotification.NotificationTypeId);
            return View(userNotification);
        }

        // POST: UserNotifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Color,Active,NotificationTypeId,ApplicationUserId,Id")] UserNotification userNotification)
        {
            if (id != userNotification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(userNotification);
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
            ViewData["NotificationTypeId"] = new SelectList(_context.NotificationTypes, "Id", "Type", userNotification.NotificationTypeId);
            return View(userNotification);
        }

        // GET: UserNotifications/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _repo.FirstOrDefaultAsync((Guid) id));
        }

        // POST: UserNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userNotification = await _repo.FirstOrDefaultAsync(id);
            _repo.Remove(userNotification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

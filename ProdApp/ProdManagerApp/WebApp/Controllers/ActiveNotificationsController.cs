using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Extensions.Base;

namespace WebApp.Controllers
{
    [Authorize]
    public class ActiveNotificationsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ActiveNotificationsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: ActiveNotifications
        public async Task<IActionResult> Index()
        {
            return View(await _uow.ActiveNotifications.GetAllAsync(User.GetUserId()!.Value));
        }

        // GET: ActiveNotifications/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var activeNotification = await _uow.ActiveNotifications.FirstOrDefaultAsync(id.Value,User.GetUserId()!.Value, false);
            
            if (activeNotification == null) return NotFound();

            return View(activeNotification);
        }

        // GET: ActiveNotifications/Create
        public async Task<IActionResult> Create()
        {
            var uId = User.GetUserId()!.Value;
            ViewData["MasterNotificationId"] = new SelectList(await _uow.ActiveNotifications.GetAllAsync(uId), "Id", "Head");
            ViewData["OrderId"] = new SelectList(await _uow.Orders.GetAllAsync(uId), "Id", "DeliveryAddress");
            ViewData["SupplyId"] = new SelectList(await _uow.Orders.GetAllAsync(uId), "Id", "Id");
            return View();
        }

        // POST: ActiveNotifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActiveNotification activeNotification)
        {
            var uId = User.GetUserId()!.Value;
            if (ModelState.IsValid)
            {
                activeNotification.AppUserId = uId;
                _uow.ActiveNotifications.Add(activeNotification);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MasterNotificationId"] = new SelectList(await _uow.ActiveNotifications.GetAllAsync(uId), "Id", "Head", activeNotification.MasterNotificationId);
            ViewData["OrderId"] = new SelectList(await _uow.Orders.GetAllAsync(uId), "Id", "DeliveryAddress", activeNotification.OrderId);
            ViewData["SupplyId"] = new SelectList(await _uow.Supplys.GetAllAsync(uId), "Id", "Id", activeNotification.SupplyId);
            return View(activeNotification);
        }

        // GET: ActiveNotifications/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var uId = User.GetUserId()!.Value;

            var activeNotification = await _uow.ActiveNotifications.FirstOrDefaultAsync(id.Value,uId);

            if (activeNotification == null) return NotFound();


            ViewData["MasterNotificationId"] = new SelectList(await _uow.ActiveNotifications.GetAllAsync(uId), "Id",
                "Head", activeNotification.MasterNotificationId);
            ViewData["OrderId"] = new SelectList(await _uow.Orders.GetAllAsync(uId), "Id", "DeliveryAddress",
                activeNotification.OrderId);
            ViewData["SupplyId"] =
                new SelectList(await _uow.Supplys.GetAllAsync(uId), "Id", "Id", activeNotification.SupplyId);
            return View(activeNotification);
        }

        // POST: ActiveNotifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Head,Info,OrderId,SupplyId,ApplicationUserId,MasterNotificationId,StartDate,EndDate,StartTime,EndTime,Id")] ActiveNotification activeNotification)
        {
            if (id != activeNotification.Id) return NotFound();
            var uId = User.GetUserId()!.Value;

            if (!ModelState.IsValid || !await _uow.ActiveNotifications.ExistsAsync(activeNotification.Id, uId))
                return View(activeNotification);

            ViewData["MasterNotificationId"] = new SelectList(await _uow.ActiveNotifications.GetAllAsync(uId), "Id",
                "Head", activeNotification.MasterNotificationId);
            ViewData["OrderId"] = new SelectList(await _uow.Orders.GetAllAsync(uId), "Id", "DeliveryAddress",
                activeNotification.OrderId);
            ViewData["SupplyId"] =
                new SelectList(await _uow.Supplys.GetAllAsync(uId), "Id", "Id", activeNotification.SupplyId);

            _uow.ActiveNotifications.Update(activeNotification);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ActiveNotifications/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

                var activeNotification = await _uow.ActiveNotifications.FirstOrDefaultAsync(id.Value,User.GetUserId()!.Value);
            
            if (activeNotification == null) return NotFound();
            
            return View(activeNotification);
        }

        // POST: ActiveNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var uId = User.GetUserId()!.Value;
            var activeNotification = await _uow.ActiveNotifications.RemoveAsync(id,uId);
            _uow.ActiveNotifications.Remove(activeNotification,uId);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

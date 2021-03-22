using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;
using Microsoft.AspNetCore.Authorization;

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
            return View(await _uow.ActiveNotifications.GetAllAsync());
        }

        // GET: ActiveNotifications/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var activeNotification = await _uow.ActiveNotifications.FirstOrDefaultAsync(id.Value, false);
            
            if (activeNotification == null) return NotFound();

            return View(activeNotification);
        }

        // GET: ActiveNotifications/Create
        public async Task<IActionResult> Create()
        {
            ViewData["MasterNotificationId"] = new SelectList(await _uow.ActiveNotifications.GetAllAsync(), "Id", "Head");
            ViewData["OrderId"] = new SelectList(await _uow.Orders.GetAllAsync(), "Id", "DeliveryAddress");
            ViewData["SupplyId"] = new SelectList(await _uow.Orders.GetAllAsync(), "Id", "Id");
            return View();
        }

        // POST: ActiveNotifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Head,Info,OrderId,SupplyId,ApplicationUserId,MasterNotificationId,StartDate,EndDate,StartTime,EndTime,Id")] ActiveNotification activeNotification)
        {
            if (ModelState.IsValid)
            {
                _uow.ActiveNotifications.Add(activeNotification);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MasterNotificationId"] = new SelectList(await _uow.ActiveNotifications.GetAllAsync(), "Id", "Head", activeNotification.MasterNotificationId);
            ViewData["OrderId"] = new SelectList(await _uow.Orders.GetAllAsync(), "Id", "DeliveryAddress", activeNotification.OrderId);
            ViewData["SupplyId"] = new SelectList(await _uow.Supplys.GetAllAsync(), "Id", "Id", activeNotification.SupplyId);
            return View(activeNotification);
        }

        // GET: ActiveNotifications/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var activeNotification = await _uow.ActiveNotifications.FirstOrDefaultAsync(id.Value);

            if (activeNotification == null) return NotFound();


            ViewData["MasterNotificationId"] = new SelectList(await _uow.ActiveNotifications.GetAllAsync(), "Id",
                "Head", activeNotification.MasterNotificationId);
            ViewData["OrderId"] = new SelectList(await _uow.Orders.GetAllAsync(), "Id", "DeliveryAddress",
                activeNotification.OrderId);
            ViewData["SupplyId"] =
                new SelectList(await _uow.Supplys.GetAllAsync(), "Id", "Id", activeNotification.SupplyId);
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

            if (!ModelState.IsValid || !await _uow.ActiveNotifications.ExistsAsync(activeNotification.Id))
                return View(activeNotification);

            ViewData["MasterNotificationId"] = new SelectList(await _uow.ActiveNotifications.GetAllAsync(), "Id",
                "Head", activeNotification.MasterNotificationId);
            ViewData["OrderId"] = new SelectList(await _uow.Orders.GetAllAsync(), "Id", "DeliveryAddress",
                activeNotification.OrderId);
            ViewData["SupplyId"] =
                new SelectList(await _uow.Supplys.GetAllAsync(), "Id", "Id", activeNotification.SupplyId);

            _uow.ActiveNotifications.Update(activeNotification);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ActiveNotifications/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

                var activeNotification = await _uow.ActiveNotifications.FirstOrDefaultAsync(id.Value);
            
            if (activeNotification == null) return NotFound();
            
            return View(activeNotification);
        }

        // POST: ActiveNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var activeNotification = await _uow.ActiveNotifications.Remove(id);
            _uow.ActiveNotifications.Remove(activeNotification);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

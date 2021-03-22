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
    public class OrdersController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public OrdersController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Orders.GetAllAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var order = await _uow.Orders.FirstOrDefaultAsync(id.Value, false);

            if (order == null) return NotFound();
            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CustomerId"] = new SelectList(await _uow.Customers.GetAllAsync(), "Id", "Address");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Number,Name,DeliveryAddress,DueDate,Info,CustomerId,ApplicationUserId,Id")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Id = Guid.NewGuid();
                _uow.Orders.Add(order);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(await _uow.Customers.GetAllAsync(), "Id", "Address", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var order = await _uow.Orders.FirstOrDefaultAsync(id.Value, false);

            if (order == null) return NotFound();

            ViewData["CustomerId"] =
                new SelectList(await _uow.Customers.GetAllAsync(), "Id", "Address", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Number,Name,DeliveryAddress,DueDate,Info,CustomerId,ApplicationUserId,Id")] Order order)
        {
            if (id != order.Id) return NotFound();

            if (!ModelState.IsValid || !await _uow.Orders.ExistsAsync(order.Id))
                return View(order);

            ViewData["CustomerId"] =
                new SelectList(await _uow.Customers.GetAllAsync(), "Id", "Address", order.CustomerId);
            _uow.Orders.Update(order);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            
            var order = await _uow.Orders.FirstOrDefaultAsync(id.Value, false);

            if (order == null) return NotFound();
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _uow.Orders.FirstOrDefaultAsync(id);
            if (order == null) return NotFound();
            _uow.Orders.Remove(order);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

using App.Contracts.DAL;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IAppUOW _uow;

        public OrderController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Orders.GetAllAsync());
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _uow.Orders
                .FirstOrDefaultAsync(id.Value);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_uow.Users.GetAll(), "Id", "Id");
            ViewData["CustomerId"] = new SelectList(_uow.Customers.GetAll(), "Id", "Id");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                order.Id = Guid.NewGuid();
                _uow.Orders.Add(order);
                await _uow.Orders.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_uow.Users.GetAll(), "Id", "Id", order.AppUserId);
            ViewData["CustomerId"] = new SelectList(_uow.Customers.GetAll(), "Id", "Id", order.CustomerId);
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _uow.Orders.FirstOrDefaultAsync(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_uow.Users.GetAll(), "Id", "Id", order.AppUserId);
            ViewData["CustomerId"] = new SelectList(_uow.Customers.GetAll(), "Id", "Id", order.CustomerId);
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Orders.Update(order);
                    await _uow.Orders.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["AppUserId"] = new SelectList(_uow.Users.GetAll(), "Id", "Id", order.AppUserId);
            ViewData["CustomerId"] = new SelectList(_uow.Customers.GetAll(), "Id", "Id", order.CustomerId);
            return View(order);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _uow.Orders
                .FirstOrDefaultAsync(id.Value);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _uow.Orders.FirstOrDefaultAsync(id);
            if (order != null)
            {
                _uow.Orders.Remove(order);
            }

            await _uow.Orders.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(Guid id)
        {
            return _uow.Orders.Exists(id);
        }
    }
}

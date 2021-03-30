using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain.App;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        
        public CustomersController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Customers.GetAllAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var customer = await _uow.Customers.FirstOrDefaultAsync(id.Value);

            if (customer == null) return NotFound();

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            _uow.Customers.Add(customer);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var customer = await _uow.Customers.FirstOrDefaultAsync(id.Value);

            if (customer == null) return NotFound();

            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Customer customer)
        {
            if (id != customer.Id) return NotFound();

            if (!ModelState.IsValid || !await _uow.Customers.ExistsAsync(customer.Id))
                return View(customer);

            _uow.Customers.Update(customer);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var customer = await _uow.Customers.FirstOrDefaultAsync(id.Value);

            if (customer == null) return NotFound();
            
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Customers.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

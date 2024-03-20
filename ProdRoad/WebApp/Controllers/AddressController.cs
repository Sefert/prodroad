using App.Contracts.DAL.Repositories;
using App.DAL.EF;
using App.DAL.EF.Repositories;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class AddressController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAddressRepository _repo;

        public AddressController(AppDbContext context)
        {
            _context = context;
            _repo = new AddressRepository(context);
        }

        // GET: Address
        public async Task<IActionResult> Index()
        {
            var response = await _repo.GetAllAsync();
            return View(response);
        }

        // GET: Address/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _repo.FirstOrDefaultAsync(id.Value);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Address/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            return View();
        }

        // POST: Address/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Address address)
        {
            if (ModelState.IsValid)
            {
                address.Id = Guid.NewGuid();
                _repo.Add(address);
                await _repo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", address.AppUserId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", address.CustomerId);
            return View(address);
        }

        // GET: Address/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _repo.FirstOrDefaultAsync(id.Value);
            if (address == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", address.AppUserId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", address.CustomerId);
            return View(address);
        }

        // POST: Address/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Address address)
        {
            if (id != address.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(address);
                    await _repo.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AddressExists(address.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", address.AppUserId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", address.CustomerId);
            return View(address);
        }

        // GET: Address/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _repo.FirstOrDefaultAsync(id.Value);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var address = await _repo.FirstOrDefaultAsync(id);
            if (address != null)
            {
                _repo.Remove(id);
            }

            await _repo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AddressExists(Guid id)
        {
            return await _repo.ExistsAsync(id);
        }
    }
}

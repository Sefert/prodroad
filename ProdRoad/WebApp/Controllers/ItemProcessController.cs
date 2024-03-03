using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class ItemProcessController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemProcessController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItemProcess
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ItemProcesses.Include(i => i.Item).Include(i => i.OrderRow).Include(i => i.Process);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ItemProcess/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemProcess = await _context.ItemProcesses
                .Include(i => i.Item)
                .Include(i => i.OrderRow)
                .Include(i => i.Process)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemProcess == null)
            {
                return NotFound();
            }

            return View(itemProcess);
        }

        // GET: ItemProcess/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Id");
            ViewData["OrderRowId"] = new SelectList(_context.OrderRows, "Id", "Id");
            ViewData["ProcessId"] = new SelectList(_context.Processes, "Id", "Id");
            return View();
        }

        // POST: ItemProcess/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderRowId,ItemId,ProcessId,Quantity,UsedCreated,Save,Id")] ItemProcess itemProcess)
        {
            if (ModelState.IsValid)
            {
                itemProcess.Id = Guid.NewGuid();
                _context.Add(itemProcess);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Id", itemProcess.ItemId);
            ViewData["OrderRowId"] = new SelectList(_context.OrderRows, "Id", "Id", itemProcess.OrderRowId);
            ViewData["ProcessId"] = new SelectList(_context.Processes, "Id", "Id", itemProcess.ProcessId);
            return View(itemProcess);
        }

        // GET: ItemProcess/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemProcess = await _context.ItemProcesses.FindAsync(id);
            if (itemProcess == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Id", itemProcess.ItemId);
            ViewData["OrderRowId"] = new SelectList(_context.OrderRows, "Id", "Id", itemProcess.OrderRowId);
            ViewData["ProcessId"] = new SelectList(_context.Processes, "Id", "Id", itemProcess.ProcessId);
            return View(itemProcess);
        }

        // POST: ItemProcess/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderRowId,ItemId,ProcessId,Quantity,UsedCreated,Save,Id")] ItemProcess itemProcess)
        {
            if (id != itemProcess.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemProcess);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemProcessExists(itemProcess.Id))
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
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Id", itemProcess.ItemId);
            ViewData["OrderRowId"] = new SelectList(_context.OrderRows, "Id", "Id", itemProcess.OrderRowId);
            ViewData["ProcessId"] = new SelectList(_context.Processes, "Id", "Id", itemProcess.ProcessId);
            return View(itemProcess);
        }

        // GET: ItemProcess/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemProcess = await _context.ItemProcesses
                .Include(i => i.Item)
                .Include(i => i.OrderRow)
                .Include(i => i.Process)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemProcess == null)
            {
                return NotFound();
            }

            return View(itemProcess);
        }

        // POST: ItemProcess/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var itemProcess = await _context.ItemProcesses.FindAsync(id);
            if (itemProcess != null)
            {
                _context.ItemProcesses.Remove(itemProcess);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemProcessExists(Guid id)
        {
            return _context.ItemProcesses.Any(e => e.Id == id);
        }
    }
}

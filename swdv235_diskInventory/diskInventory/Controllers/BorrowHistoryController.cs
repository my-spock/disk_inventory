using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using diskInventory.Data;
using diskInventory.Models;

namespace diskInventory.Controllers
{
    public class BorrowHistoryController : Controller
    {
        private readonly InventoryContext _context;

        public BorrowHistoryController(InventoryContext context)
        {
            _context = context;
        }

        // GET: BorrowHistory
        public async Task<IActionResult> Index()
        {
            var diskInventoryContext = _context.BorrowedItems.Include(b => b.Borrower).Include(b => b.Item);
            return View(await diskInventoryContext.ToListAsync());
        }

        // GET: BorrowHistory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowedItem = await _context.BorrowedItems
                .Include(b => b.Borrower)
                .Include(b => b.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowedItem == null)
            {
                return NotFound();
            }

            return View(borrowedItem);
        }

        // GET: BorrowHistory/Create
        public IActionResult Create()
        {
            ViewData["BorrowerId"] = new SelectList(_context.Set<Borrower>(), "Id", "FullName");
            ViewData["ItemId"] = new SelectList(_context.Set<Item>(), "Id", "Name");
            return View();
        }

        // POST: BorrowHistory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BorrowedDate,ReturnedDate,BorrowerId,ItemId")] BorrowedItem borrowedItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrowedItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BorrowerId"] = new SelectList(_context.Set<Borrower>(), "Id", "FullName", borrowedItem.BorrowerId);
            ViewData["ItemId"] = new SelectList(_context.Set<Item>(), "Id", "Name", borrowedItem.ItemId);
            return View(borrowedItem);
        }

        // GET: BorrowHistory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowedItem = await _context.BorrowedItems.FindAsync(id);
            if (borrowedItem == null)
            {
                return NotFound();
            }
            ViewData["BorrowerId"] = new SelectList(_context.Set<Borrower>(), "Id", "FullName", borrowedItem.BorrowerId);
            ViewData["ItemId"] = new SelectList(_context.Set<Item>(), "Id", "Name", borrowedItem.ItemId);
            return View(borrowedItem);
        }

        // POST: BorrowHistory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BorrowedDate,ReturnedDate,BorrowerId,ItemId")] BorrowedItem borrowedItem)
        {
            if (id != borrowedItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowedItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowedItemExists(borrowedItem.Id))
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
            ViewData["BorrowerId"] = new SelectList(_context.Set<Borrower>(), "Id", "FullName", borrowedItem.BorrowerId);
            ViewData["ItemId"] = new SelectList(_context.Set<Item>(), "Id", "Name", borrowedItem.ItemId);
            return View(borrowedItem);
        }

        // GET: BorrowHistory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowedItem = await _context.BorrowedItems
                .Include(b => b.Borrower)
                .Include(b => b.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowedItem == null)
            {
                return NotFound();
            }

            return View(borrowedItem);
        }

        // POST: BorrowHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrowedItem = await _context.BorrowedItems.FindAsync(id);
            _context.BorrowedItems.Remove(borrowedItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowedItemExists(int id)
        {
            return _context.BorrowedItems.Any(e => e.Id == id);
        }
    }
}

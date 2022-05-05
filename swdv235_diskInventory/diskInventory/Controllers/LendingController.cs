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
    public class LendingController : Controller
    {
        private readonly InventoryContext _context;

        public LendingController(InventoryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<BorrowedItem> borrowedItems = _context.BorrowedItems
                .Include(b => b.Borrower).Include(b => b.Item).OrderBy(bi => bi.BorrowedDate).ToList();
            return View(borrowedItems);
        }

        [HttpGet]
        public ViewResult Add() => GetItem(0, "Add");

        [HttpPost]
        public IActionResult Add(BorrowedItem borrowedItem)
        {
            //check that borrow date is before return date
            DateIsBefore(borrowedItem.BorrowedDate, borrowedItem.ReturnedDate);

            if (ModelState.IsValid)
            {
                _context.Database.ExecuteSqlRaw("exec sp_insertBorrowedItem @p0, @p1, @p2, @p3",
                   parameters: new[] { borrowedItem.BorrowerId.ToString(), borrowedItem.ItemId.ToString(), borrowedItem.BorrowedDate.ToString(), borrowedItem.ReturnedDate?.ToString() });
                StatusType status = _context.StatusTypes.Single(s => s.Name == "On Loan");
                _context.Database.ExecuteSqlRaw($"UPDATE [item] SET [status_id] = {status.Id} WHERE [item_id] = {borrowedItem.ItemId}");
                _context.SaveChanges();

                TempData["message"] = "Item has been checked out.";
                return RedirectToAction("Index");
            }
            else
            {
                LendingViewModel vm = new LendingViewModel(_context)
                {
                    BorrowedItem = borrowedItem,
                    action = "Add"
                };

                ModelState.AddModelError("", "There are errors in the form.");
                return View("AddOrEdit", vm);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id) => GetItem(id, "Edit");

        [HttpPost]
        public IActionResult Edit(BorrowedItem borrowedItem)
        {
            //check that borrow date is before return date
            DateIsBefore(borrowedItem.BorrowedDate, borrowedItem.ReturnedDate);

            if (ModelState.IsValid)
            {
                _context.Database.ExecuteSqlRaw("exec sp_updateBorrowedItem @p0, @p1, @p2, @p3, @p4",
                    parameters: new[] { borrowedItem.Id.ToString(), borrowedItem.BorrowerId.ToString(), borrowedItem.ItemId.ToString(), borrowedItem.BorrowedDate.ToString(), borrowedItem.ReturnedDate?.ToString()});
                //TODO: decide how to handle item.status update
                _context.SaveChanges();

                TempData["message"] = "Lending history has been updated.";
                return RedirectToAction("Index");
            }
            else
            {
                borrowedItem.Borrower = _context.Borrowers.Single(b => b.Id == borrowedItem.BorrowerId);
                borrowedItem.Item = _context.Items.Single(i => i.Id == borrowedItem.ItemId);

                LendingViewModel vm = new LendingViewModel(_context)
                {
                    BorrowedItem = borrowedItem,
                    action = "Edit"
                };

                ModelState.AddModelError("", "There are errors in the form.");
                return View("AddOrEdit", vm);
            }
        }

        // private helper methods
        private ViewResult GetItem(int id, string operation)
        {
            LendingViewModel vm = new LendingViewModel(_context);
            Load(vm, operation, id);
            return View("AddOrEdit", vm);
        }
        private void Load(LendingViewModel vm, string op, int? id = null)
        {
            vm.action = op;

            if (Operation.IsAdd(op))
            {
                vm.BorrowedItem = new BorrowedItem
                {
                    BorrowedDate = DateTime.Today
                };
            }
            else
            {
                vm.BorrowedItem = _context.BorrowedItems.Include(bi => bi.Borrower).Include(bi => bi.Item).Single(bi => bi.Id == id);
            }
        }
        private void DateIsBefore(DateTime borrowDate, DateTime? returnDate)
        {
            if (returnDate == null)
            {
                return;
            }
            else if (borrowDate > returnDate)
            {
                ModelState.AddModelError("", "The borrowed date must be before the returned date.");
            }
        }
    }
}

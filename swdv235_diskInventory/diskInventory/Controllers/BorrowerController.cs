using diskInventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diskInventory.Controllers
{
    public class BorrowerController : Controller
    {
        private InventoryContext context;

        public BorrowerController(InventoryContext ctx)
        {
            context = ctx;
        }

        [Route("[controller]s")]
        public IActionResult Index()
        {
            //get a complete list of borrowers
            List<Borrower> borrowers = context.Borrowers.OrderBy(b => b.FullName).ToList();

            return View(borrowers);
        }

        //add
        [HttpGet]
        public ViewResult Add(int id) => GetBorrower(id, "Add");
        [HttpPost]
        public IActionResult Add(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                context.Database.ExecuteSqlRaw("exec sp_insertBorrower @p0, @p1",
                    parameters: new[] { borrower.FullName.ToString(), borrower.Phone.ToString()});
                context.SaveChanges();

                TempData["message"] = $"{borrower.FullName} updated.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else
            {
                BorrowerViewModel vm = new BorrowerViewModel(context);
                vm.Borrower = borrower;
                vm.action = "Add";
                ModelState.AddModelError("", "There are errors in the form.");
                return View("Edit", vm);
            }
        }

        //edit
        [HttpGet]
        public ViewResult Edit(int id) => GetBorrower(id, "Edit");
        [HttpPost]
        public IActionResult Edit(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                context.Database.ExecuteSqlRaw("exec sp_updateBorrower @p0, @p1, @p2",
                    parameters: new[] { borrower.Id.ToString(), borrower.FullName.ToString(), borrower.Phone.ToString()});
                context.SaveChanges();

                TempData["message"] = $"{borrower.FullName} updated.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else
            {
                BorrowerViewModel vm = new BorrowerViewModel(context);
                vm.Borrower = borrower;
                vm.action = "Edit";
                ModelState.AddModelError("", "There are errors in the form.");
                return View("Edit", vm);
            }
        }

        // delete
        [HttpGet]
        public ViewResult Delete(int id) => GetBorrower(id, "Delete");

        [HttpPost]
        public IActionResult Delete(Borrower borrower)
        {
            // delete Borrower and any references to Borrower in BorrowedItems
            context.Database.ExecuteSqlRaw("exec sp_deleteBorrower @p0",
                parameters: new[] { borrower.Id.ToString()});
            context.SaveChanges();
            TempData["message"] = $"{borrower.FullName} removed from borrowers.";
            return RedirectToAction("Index"); 
        }

        // private helper methods
        private ViewResult GetBorrower(int id, string operation)
        {
            var vm = new BorrowerViewModel(context);
            Load(vm, operation, id);
            return View("Edit", vm);
        }
        private void Load(BorrowerViewModel vm, string op, int? id = null)
        {
            vm.action = op;

            if (Operation.IsAdd(op))
            {
                vm.Borrower = new Borrower();
            }
            else
            {
                vm.Borrower = context.Borrowers.Find(id);
            }
        }
    }
}

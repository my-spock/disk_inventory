using diskInventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diskInventory.Controllers
{
    public class ItemController : Controller
    {
        private InventoryContext context;

        public ItemController(InventoryContext ctx)
        {
            context = ctx;
        }

        [Route("[controller]s")]
        public IActionResult Index()
        {
            var model = new ItemListViewModel(context);
            return View(model);
        }

        //add
        [HttpGet]
        public ViewResult Add(int id) => GetItem(id, "Add");
        [HttpPost]
        public IActionResult Add(Item item)
        {
            if (ModelState.IsValid)
            {
                context.Database.ExecuteSqlRaw("exec sp_insertItem @p0, @p1, @p2, @p3, @p4",
                    parameters: new[] {item.Name.ToString(), item.ReleaseDate.ToString(), item.StatusId.ToString(), item.TypeId.ToString(), item.GenreId.ToString() });
                context.SaveChanges();

                TempData["message"] = $"{item.Name} updated.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else
            {
                ItemViewModel vm = new ItemViewModel(context);
                vm.Item = item;
                vm.action = "Add";
                ModelState.AddModelError("", "There are errors in the form.");
                return View("Edit", vm);
            }
        }

        //edit
        [HttpGet]
        public ViewResult Edit(int id) => GetItem(id, "Edit");
        [HttpPost]
        public IActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                context.Database.ExecuteSqlRaw("exec sp_updateItem @p0, @p1, @p2, @p3, @p4, @p5",
                    parameters: new[] { item.Id.ToString(), item.Name.ToString(), item.ReleaseDate.ToString(), item.StatusId.ToString(), item.TypeId.ToString(), item.GenreId.ToString() });
                context.SaveChanges();

                TempData["message"] = $"{item.Name} updated.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else
            {
                ItemViewModel vm = new ItemViewModel(context);
                vm.Item = item;
                vm.action = "Edit";
                ModelState.AddModelError("", "There are errors in the form.");
                return View("Edit", vm);
            }
        }

        // delete
        [HttpGet]
        public ViewResult Delete(int id) => GetItem(id, "Delete");

        [HttpPost]
        public IActionResult Delete(Item item)
        {
            // delete Item and references in BorrowedItems 
            context.Database.ExecuteSqlRaw("exec sp_deleteItem @p0",
                parameters: new[] {item.Id.ToString() });
            context.SaveChanges();
            TempData["message"] = $"{item.Name} removed from inventory.";
            return RedirectToAction("Index");  // PRG pattern
        }

        // private helper methods
        private ViewResult GetItem(int id, string operation)
        {
            var vm = new ItemViewModel(context);
            Load(vm, operation, id);
            return View("Edit", vm);
        }
        private void Load(ItemViewModel vm, string op, int? id = null)
        {
            vm.action = op;

            if (Operation.IsAdd(op))
            {
                vm.Item = new Item();
                vm.Item.ReleaseDate = DateTime.Today;
            }
            else
            {
                vm.Item = context.Items.Find(id);
            }
        }
    }
}

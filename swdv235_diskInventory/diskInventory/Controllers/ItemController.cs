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
        public ViewResult Index()
        {
            var model = new ItemListViewModel(context);
            return View(model);
        }

        public IActionResult View(int id)
        {
            if(id == 0)
                return RedirectToAction("Index");

            ItemViewModel vm = new ItemViewModel(context);
            var item = context.Items.Find(id);
            vm.Item = item;
            return View(vm);
        }

        //add
        [HttpGet]
        public ViewResult Add(int id) => GetItem(id, "Add");
        [HttpPost]
        public IActionResult Add(Item item)
        {
            //TODO: validate empty item.ReleaseDate field
            if (ModelState.IsValid)
            {
                context.Items.Add(item);
                context.SaveChanges();

                TempData["message"] = $"{item.Name} updated.";
                return RedirectToAction("View", item.Id);  // PRG pattern
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
            //TODO: validate empty item.ReleaseDate field
            if (ModelState.IsValid)
            {
                context.Items.Update(item);
                context.SaveChanges();

                TempData["message"] = $"{item.Name} updated.";
                return RedirectToAction("View", item.Id);  // PRG pattern
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
            // remove any references to Item in BorrowedItems 
            DeleteBorrowedItems(item.Id);
            // delete Item
            context.Items.Remove(item);
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
        private void DeleteBorrowedItems(int itemId)
        {
            //get list of borrowed item references
            var borrowedInstances = context.BorrowedItems.Where(bi => bi.ItemId == itemId).ToList();

            foreach (BorrowedItem bi in borrowedInstances)
            {
                context.BorrowedItems.Remove(bi);
            }
        }
    }
}

using diskInventory.Models;
using Microsoft.AspNetCore.Mvc;
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
            //get a list of item types
            //List<ItemType> itemTypes = context.ItemTypes.OrderBy(i => i.Name).ToList();
            //ViewData["types"] = itemTypes;

            //get items by type
            //foreach (ItemType type in itemTypes)
            //{
            //    ViewData[type.Name] = context.Items.ToList();
            //}
            //ViewData["items"] = context.Items.ToList();

            var model = new ItemListViewModel
            {
                Types = context.ItemTypes.ToList(),
                Statuses = context.StatusTypes.ToList(),
                Genres = context.Genres.ToList(),
                Items = context.Items.OrderBy(i => i.Name).ToList()
            };

            return View(model);
        }
    }
}

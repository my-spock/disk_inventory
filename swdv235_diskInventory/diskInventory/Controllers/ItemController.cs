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
            var model = new ItemListViewModel
            {
                //Types = context.ItemTypes.ToList(),
                //Statuses = context.StatusTypes.ToList(),
                Genres = context.Genres
                .Include("Items.Status").Include("Items.Type")
                .ToList(),
                Items = context.Items.OrderBy(i => i.Name).ToList()
            };

            return View(model);
        }
    }
}

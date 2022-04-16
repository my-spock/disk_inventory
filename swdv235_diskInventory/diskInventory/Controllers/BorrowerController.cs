using diskInventory.Models;
using Microsoft.AspNetCore.Mvc;
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
            ViewData["borrowers"] = context.Borrowers.OrderBy(b => b.FullName).ToList();

            return View();
        }
    }
}

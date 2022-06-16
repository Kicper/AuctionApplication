using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Category()
        {
            return RedirectToAction("Index", "Category");
        }

        public IActionResult Auction()
        {
            return RedirectToAction("Index", "Auction");
        }

        public IActionResult Home()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}

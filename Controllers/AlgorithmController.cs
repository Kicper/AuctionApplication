using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Controllers
{
    public class AlgorithmController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Main()
        {
            return RedirectToAction("Index", "Main");
        }
    }
}

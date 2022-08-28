using AuctionApplication.Models;
using AuctionApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Controllers
{
    public class AlgorithmController : Controller
    {
        public IActionResult Index(AlgorithmModel algorithmModel)
        {
            int userId = (int)TempData["userId"];
            TempData.Keep("userId");
            List<(int ItemId, string ItemName, double Result)> finalResult;
            finalResult = algorithmModel.GetBestAuction(userId);
            algorithmModel.Scalarization(userId);
            ViewData["result"] = finalResult;
            return View();
        }

        public IActionResult Main()
        {
            return RedirectToAction("Index", "Main");
        }
    }
}

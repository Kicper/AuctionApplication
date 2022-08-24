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
            AlgorithmDAO algorithm = new AlgorithmDAO();
            List<(int ItemId, int Frequency)> itemsFrequency = algorithm.ItemsFrequency();
            List<(int ItemId, int MinPrice, int MaxPrice, string MinTime, string MaxTime)> minMaxPriceAndDate = algorithm.MinMaxPriceAndDate();
            return View();
        }

        public IActionResult Main()
        {
            return RedirectToAction("Index", "Main");
        }
    }
}

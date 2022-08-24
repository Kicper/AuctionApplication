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
            AlgorithmDAO algorithm = new AlgorithmDAO();
            List<(int ItemId, int Frequency)> itemsFrequency = algorithm.ItemsFrequency();
            List<(int ItemId, int MinPrice, int MaxPrice, string MinTime, string MaxTime)> minMaxPriceAndDate = algorithm.MinMaxPriceAndDate();
            List<(int ItemId, int Rating)> itemPreferences = algorithm.ItemPreferences(userId);
            List<(int ItemId, int Rating)> categoryPreferences = algorithm.CategoryPreferences(userId);
            List<(int ItemId, int AvgPrice, string StartTime, string EndTime)> averagePriceStartEndDate = algorithm.AveragePriceStartEndDate();
            return View();
        }

        public IActionResult Main()
        {
            return RedirectToAction("Index", "Main");
        }
    }
}

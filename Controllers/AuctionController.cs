using AuctionApplication.Models;
using AuctionApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Controllers
{
    public class AuctionController : Controller
    {
        public IActionResult Index()
        {
            AuctionDAO auction = new AuctionDAO();
            return View(auction.GetAllAvailableAuctions());
        }

        public IActionResult SearchResults(string searchTerm)
        {
            AuctionDAO auction = new AuctionDAO();
            List<ItemModel> foundAuctions = auction.SearchAuctions(searchTerm);
            return View("Index", foundAuctions);
        }

        public IActionResult SearchForm()
        {
            return View();
        }

        public IActionResult Offer(int auctionId)
        {
            TempData["auctionId"] = auctionId;
            return RedirectToAction("Index", "Offer");
        }
    }
}

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

        public IActionResult Offer(int auctionId)
        {
            TempData["auctionId"] = auctionId;
            return RedirectToAction("Index", "Offer");
        }
    }
}

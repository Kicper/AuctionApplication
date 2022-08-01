using AuctionApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Controllers
{
    public class OfferController : Controller
    {
        public IActionResult Index()
        {
            int auctionId = (int)TempData["auctionId"];
            //int userId = (int)TempData["userId"];
            //TempData.Keep("userId");
            OfferDAO offer = new OfferDAO();
            return View(offer.GetAllOffersInAuction(auctionId));
        }

        public IActionResult Auction()
        {
            return RedirectToAction("Index", "Auction");
        }
    }
}

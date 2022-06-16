using AuctionApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            int categoryId = (int)TempData["categoryId"];
            int userId = (int)TempData["userId"];
            TempData.Keep("userId");
            ItemDAO item = new ItemDAO();
            return View(item.GetAllItemsInCategory(userId, categoryId));
        }
    }
}

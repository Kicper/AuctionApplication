using AuctionApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            int userId = (int)TempData["userId"];
            TempData.Keep("userId");
            CategoryDAO category = new CategoryDAO();
            return View(category.GetAllCategories(userId));
        }

        public IActionResult Item(int categoryId)
        {
            TempData["categoryId"] = categoryId;
            return RedirectToAction("Index", "Item");
        }
    }
}

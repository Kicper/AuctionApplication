﻿using AuctionApplication.Models;
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
            TempData.Keep("categoryId");
            int userId = (int)TempData["userId"];
            TempData.Keep("userId");
            ItemDAO item = new ItemDAO();
            return View(item.GetAllItemsInCategory(userId, categoryId));
        }

        public IActionResult SearchResults(string searchTerm)
        {
            int categoryId = (int)TempData["categoryId"];
            TempData.Keep("categoryId");
            int userId = (int)TempData["userId"];
            TempData.Keep("userId");
            ItemDAO item = new ItemDAO();
            List<ItemModel> foundItems= item.SearchItems(userId, categoryId, searchTerm);
            return View("Index", foundItems);
        }

        public IActionResult Category()
        {
            return RedirectToAction("Index", "Category");
        }

        public IActionResult SearchForm()
        {
            return View();
        }
    }
}

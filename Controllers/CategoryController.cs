using AuctionApplication.Models;
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

        public IActionResult SearchResults(string searchTerm)
        {
            int userId = (int)TempData["userId"];
            TempData.Keep("userId");
            CategoryDAO category = new CategoryDAO();
            List<CategoryModel> foundCategories = category.SearchCategories(userId, searchTerm);
            return View("Index", foundCategories);
        }

        public IActionResult Main()
        {
            return RedirectToAction("Index", "Main");
        }

        public IActionResult SearchForm()
        {
            return View();
        }

        public IActionResult Edit(CategoryModel submittedCategory)
        {
            CategoryDAO category = new CategoryDAO();
            int userId = (int)TempData["userId"];
            TempData.Keep("userId");
            category.UpdatePreference(userId, submittedCategory);
            return View("Index", category.GetAllCategories(userId));
        }

        public IActionResult EditRating(int categoryId, string categoryName, int categoryRating)
        {
            CategoryModel category = new CategoryModel();
            category.Id = categoryId;
            category.Name = categoryName;
            category.Rating = categoryRating;
            return View("Edit", category);
        }

        public IActionResult Item(int categoryId)
        {
            TempData["categoryId"] = categoryId;
            return RedirectToAction("Index", "Item");
        }
    }
}

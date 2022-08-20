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

        public IActionResult Item(int categoryId)
        {
            TempData["categoryId"] = categoryId;
            return RedirectToAction("Index", "Item");
        }

        /*public IActionResult IndexSubmit(IEnumerable<CategoryModel> categoryModels)
        {
            //List<CategoryModel> submittedCategories = ViewData.{ r => r.Id };

            //var viewmodel = new CategoryModel
            return RedirectToAction("Index", "Item");
        }*/
    }
}

using AuctionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Services
{
    interface ICategoryDataService
    {
        List<CategoryModel> GetAllCategories(int userId);

        List<CategoryModel> SearchCategories(int userId, string searchTerm);

        CategoryModel GetCategoryById(int id);

        int Update(CategoryModel category);
    }
}

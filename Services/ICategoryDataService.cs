﻿using AuctionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Services
{
    interface ICategoryDataService
    {
        List<CategoryModel> GetAllCategories(int userId);

        CategoryModel GetCategoryById(int id);

        int Update(CategoryModel category);
    }
}
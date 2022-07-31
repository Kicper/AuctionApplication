using AuctionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Services
{
    interface IItemDataService
    {
        List<ItemModel> GetAllItemsInCategory(int userId, int categoryId);

        List<ItemModel> SearchItems(int userId, int categoryId, string searchTerm);

        ItemModel GetItemById(int id);

        int Update(ItemModel item);
    }
}

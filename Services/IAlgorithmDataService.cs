using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Services
{
    interface IAlgorithmDataService
    {
        List<(int ItemId, string ItemName, int CategoryId, int Frequency)> GetItemsFrequency();

        List<(int ItemId, int MinPrice, int MaxPrice, string MinTime, string MaxTime)> GetMinMaxPriceAndDate();

        List<(int ItemId, int Rating)> GetItemPreferences(int userId);

        List<(int CategoyId, int Rating)> GetCategoryPreferences(int userId);

        List<(int ItemId, int AvgPrice, int CategoryId, string StartTime, string EndTime)> GetAveragePriceCategoryStartEndDate();
    }
}

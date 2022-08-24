using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Services
{
    interface IAlgorithmDataService
    {
        List<(int ItemId, int Frequency)> ItemsFrequency();

        List<(int ItemId, int MinPrice, int MaxPrice, string MinTime, string MaxTime)> MinMaxPriceAndDate();

        List<(int ItemId, int Rating)> ItemPreferences(int userId);

        List<(int CategoyId, int Rating)> CategoryPreferences(int userId);

        List<(int ItemId, int AvgPrice, string StartTime, string EndTime)> AveragePriceStartEndDate();
    }
}

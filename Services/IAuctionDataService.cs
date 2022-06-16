using AuctionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Services
{
    interface IAuctionDataService
    {
        List<ItemModel> GetAllAvailableAuctions();
    }
}

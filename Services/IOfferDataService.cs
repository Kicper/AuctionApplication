using AuctionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Services
{
    interface IOfferDataService
    {
        List<OfferModel> GetAllOffersInAuction(int auctionId);
    }
}

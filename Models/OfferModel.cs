using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Models
{
    public class OfferModel
    {
        public int Id { get; set; }

        public int AuctionId { get; set; }

        public string Date { get; set; }

        public int Price { get; set; }

        public int Person { get; set; }
    }
}

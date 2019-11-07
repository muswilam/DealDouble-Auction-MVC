using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DealDouble.Entities;

namespace DealDouble.Web.ViewModels
{
    public class AuctionsListingViewModel : PageViewModel
    {
        public List<Auction> AllAuctions { get; set; }
    }

    public class AuctionsViewModel : PageViewModel
    {
        public List<Auction> AllAuctions { get; set; }

        public List<Auction> PromotedAuctions { get; set; }
    }
}
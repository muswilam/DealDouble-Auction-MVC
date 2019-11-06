using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DealDouble.Entities;

namespace DealDouble.Web.ViewModels
{
    public class AuctionsViewModel
    {
        public List<Auction> AllAuctions { get; set; }

        public List<Auction> PromotedAuctions { get; set; }
    }
}
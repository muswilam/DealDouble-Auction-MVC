using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DealDouble.Entities;

namespace DealDouble.Web.ViewModels
{
    public class AuctionViewModel : PageViewModel
    {
        public Auction Auction { get; set; }
    }

    public class AuctionsListingViewModel : PageViewModel
    {
        public List<Auction> AllAuctions { get; set; }
    }

    public class AuctionsViewModel : PageViewModel
    {
        public List<Auction> AllAuctions { get; set; }

        public List<Auction> PromotedAuctions { get; set; }
    }

    public class CreateAuctionViewModel : PageViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal ActualPrice { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string AuctionPictures { get; set; }

        public List<Category> Categories { get; set; }
    }

    public class EditAuctionViewModel : PageViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal ActualPrice { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string AuctionPictures { get; set; }

        public Auction Auction { get; set; }

        public List<Category> Categories { get; set; }
    }
}
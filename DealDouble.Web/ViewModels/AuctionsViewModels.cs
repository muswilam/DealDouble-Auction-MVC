using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DealDouble.Entities;
using System.ComponentModel.DataAnnotations;

namespace DealDouble.Web.ViewModels
{
    public class AuctionDetailsViewModel : PageViewModel
    {
        public Auction Auction { get; set; }
    }

    public class AuctionsListingViewModel : PageViewModel
    {
        public List<Auction> AllAuctions { get; set; }

        public Pager Pager { get; set; }

        public int? CategoryId { get; set; }
        public string SearchTerm { get; set; }
        public int? PageNo { get; set; }

        public List<Category> Categories { get; set; }
    }

    public class AuctionsViewModel : PageViewModel
    {
        public List<Auction> AllAuctions { get; set; }

        public List<Auction> PromotedAuctions { get; set; }
    }

    public class AuctionViewModel : PageViewModel
    {
        public int Id { get; set; }

        [Required, MinLength(15), MaxLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }

        [Range(1, 1000000)]
        public decimal ActualPrice { get; set; }

        public DateTime? StartingTime { get; set; }
        public DateTime? EndingTime { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string AuctionPictures { get; set; }

        public Auction Auction { get; set; }

        public List<Category> Categories { get; set; }
    }
}
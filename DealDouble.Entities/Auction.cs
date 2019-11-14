using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DealDouble.Entities
{
    public class Auction : BaseEntity
    {
        [Required, MinLength(15), MaxLength(150)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required, Range(1, 1000000)]
        public decimal ActualPrice { get; set; }

        public DateTime? StartingTime { get; set; }
        public DateTime? EndingTime { get; set; }

        //nav props
        public List<AuctionPicture> AuctionPictures { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Bid> Bids { get; set; }
    }
}

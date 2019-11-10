using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Entities
{
    public class Auction : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal ActualPrice { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }

        //nav props
        public List<AuctionPicture> AuctionPictures { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Entities
{
    public class Auction
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal ActualPrice { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }
    }
}

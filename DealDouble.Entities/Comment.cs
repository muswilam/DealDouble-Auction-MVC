using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Entities
{
    public class Comment : BaseEntity
    {
        [Required, MaxLength(400)]
        public string Body { get; set; }

        public DateTime Timestamp { get; set; }

        //nav props 
        [Required]
        public string UserId { get; set; }
        public DealDoubleUser User { get; set; }

        public int EntityId { get; set; } //Auctions || Blogs tables 
        public int RecordId { get; set; } //auctionId, postId
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DealDouble.Entities;

namespace DealDouble.Web.ViewModels
{
    public class CommentViewModel
    {
        public string Body { get; set; }

        public int EntityId { get; set; } //Auctions || Blogs tables 
        public int RecordId { get; set; } //auctionId, postId
    }

    public class CommentsViewModel : PageViewModel
    {
        public List<Comment> Comments { get; set; }
    }
}
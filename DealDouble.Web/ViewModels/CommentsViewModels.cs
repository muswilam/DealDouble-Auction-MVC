using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DealDouble.Web.ViewModels
{
    public class CommentViewModel
    {
        [Required, MaxLength(400)]
        public string Body { get; set; }

        [Range(1, 5)]
        public byte Rating { get; set; }

        public int EntityId { get; set; } //Auctions || Blogs tables 
        public int RecordId { get; set; } //auctionId, postId
    }

    public class CommentsViewModel : PageViewModel
    {
        public List<Comment> Comments { get; set; }
    }
}
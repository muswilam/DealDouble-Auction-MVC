using DealDouble.Data;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DealDouble.Services
{
    public class CommentsServices
    {
        //create a comment
        public bool LeaveComment(Comment comment)
        {
            var context = new DealDoubleContext();

            context.Comments.Add(comment);
            return context.SaveChanges() > 0;
        }

        //get all comments by entityId, and recordId
        public List<Comment> GetComments(int entityId, int recordId)
        {
            var context = new DealDoubleContext();

            return context.Comments.Include(c => c.User).Where(c => c.EntityId == entityId && c.RecordId == recordId).ToList();
        }

        //get average rate 
        public int? GetAverageRate(int entityId, int recordId)
        {
            var context = new DealDoubleContext();

            var comments = context.Comments.Where(c => c.EntityId == entityId && c.RecordId == recordId);

            if (comments != null && comments.Count() > 0)
            {
                var rates = comments.Select(c => c.Rating);

                var multipliedRatesCount = (rates.Where(r => r == 5).Count() * 5) +
                        (rates.Where(r => r == 4).Count() * 4) +
                        (rates.Where(r => r == 3).Count() * 3) +
                        (rates.Where(r => r == 2).Count() * 2) +
                        (rates.Where(r => r == 1).Count() * 1);

                decimal average = decimal.Divide(multipliedRatesCount, rates.Count());

                return (Int32)Math.Round(average, MidpointRounding.AwayFromZero);
            }

            return null;
        }
        
        //delete comments by entityId, and recordId
        public void DeleteEntityComments(int entityId, int recordId)
        {
            var context = new DealDoubleContext();

            var auctionComments = context.Comments.Where(c => c.EntityId == entityId && c.RecordId == recordId);

            foreach (var auctionComment in auctionComments)
            {
                context.Entry(auctionComment).State = EntityState.Deleted;
            }

            context.SaveChanges();
        }

        //delete a comment by entityId, and recordId
        public bool DeleteEntityComment(int entityId, int recordId)
        {
            var context = new DealDoubleContext();

            var auctionComment = context.Comments.Where(c => c.EntityId == entityId && c.RecordId == recordId).First();

            context.Entry(auctionComment).State = EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}

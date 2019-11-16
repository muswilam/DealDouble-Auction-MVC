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
    public class SharedService
    {
        public int SavePicture(Picture picture)
        {
            var context = new DealDoubleContext();

            context.Pictures.Add(picture);
            context.SaveChanges();

            return picture.Id;
        }

        public bool LeaveComment(Comment comment)
        {
            var context = new DealDoubleContext();

            context.Comments.Add(comment);
            return context.SaveChanges() > 0;
        }

        public List<Comment> GetComments(int entityId, int recordId)
        {
            var context = new DealDoubleContext();

            return context.Comments.Include(c => c.User).Where(c => c.EntityId == entityId && c.RecordId == recordId).ToList();
        }
    }
}

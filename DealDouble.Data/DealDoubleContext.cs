using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealDouble.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DealDouble.Data
{
    public class DealDoubleContext : IdentityDbContext<DealDoubleUser>
    {
        public DealDoubleContext()
            : base("name=DealDoubleCS")
        {
        }

        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<AuctionPicture> AuctionPictures { get; set; }
        public DbSet<Category> Categories { get; set; }

        public static DealDoubleContext Create()
        {
            return new DealDoubleContext();
        }
    }
}

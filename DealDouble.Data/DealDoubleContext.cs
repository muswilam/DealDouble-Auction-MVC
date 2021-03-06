﻿using System;
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
            Database.SetInitializer<DealDoubleContext>(new DealDoubleDbInitializer());
        }

        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<AuctionPicture> AuctionPictures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public static DealDoubleContext Create()
        {
            return new DealDoubleContext();
        }

        //CreateDatabaseIfNotExists -- Default
        //DropCreateDatabaseIfModelChanges
        //DropCreateDatabaseAlways
    }
}

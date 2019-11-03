﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealDouble.Entities;

namespace DealDouble.Data
{
    public class DealDoubleContext : DbContext
    {
        public DealDoubleContext()
            : base("name=DealDoubleCS")
        {
        }

        public DbSet<Auction> Auctions { get; set; }
    }
}

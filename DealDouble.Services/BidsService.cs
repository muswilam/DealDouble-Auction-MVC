﻿using DealDouble.Data;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Services
{
    public class BidsService
    {
        #region Singleton Non-Thread Safety
        public static BidsService Instance
        {
            get
            {
                if (instance == null) instance = new BidsService();
                return instance;
            }
        }

        private static BidsService instance { get; set; }

        private BidsService()
        {

        }
        #endregion

        //create new bid
        public bool AddBid(Bid bid)
        {
            DealDoubleContext context = new DealDoubleContext();

            context.Bids.Add(bid);
            return context.SaveChanges() > 0;
        }

        //get count of all bids that been created
        public int GetBidsCount()
        {
            var context = new DealDoubleContext();

            return context.Bids.Count();
        }
    }
}

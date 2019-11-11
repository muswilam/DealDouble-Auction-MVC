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
    public class AuctionsService
    {
        public List<Auction> GetAuctions()
        {
            var context = new DealDoubleContext();

            return context.Auctions.Include(a => a.Category)
                .Include(a => a.AuctionPictures)
                .Include("AuctionPictures.Picture").ToList();
        }

        public List<Auction> GetPromotedAuctions()
        {
            var context = new DealDoubleContext();

            return context.Auctions.Take(4).ToList();
        }

        public Auction GetAuction(int id)
        {
            var context = new DealDoubleContext();

            return context.Auctions.Include(a => a.AuctionPictures).Include("AuctionPictures.Picture").Where(a => a.Id == id).First();
        }

        public void SaveAuction(Auction auction)
        {
            var context = new DealDoubleContext();

            context.Auctions.Add(auction);
            context.SaveChanges();
        }

        public void UpdateAuction(Auction auction)
        {
            var context = new DealDoubleContext();

            context.Entry(auction).State = EntityState.Modified;

            context.SaveChanges();
        }

        public void DeleteAuction(Auction auctionModel)
        {
            var context = new DealDoubleContext();

            context.Entry(auctionModel).State = EntityState.Deleted;

            context.SaveChanges();
        }
    }
}

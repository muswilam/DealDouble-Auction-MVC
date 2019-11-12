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

        //get auctions by filtering them using search, category, and page no
        public List<Auction> FilterAuctions(int? categoryId, string searchTerm, int pageNo, int pageSize)
        {
            var context = new DealDoubleContext();

            var auction = context.Auctions.Include(a => a.Category).AsQueryable();

            if(categoryId.HasValue && categoryId.Value > 0)
            {
                auction = auction.Where(x => x.CategoryId == categoryId.Value);
            }

            if(!string.IsNullOrEmpty(searchTerm))
            {
                auction = auction.Where(x => x.Title.ToLower().Contains(searchTerm.ToLower()));
            }

            int skipCount = pageSize * ( pageNo - 1);

            return auction.OrderByDescending(a => a.Id).Skip(skipCount).Take(pageSize).ToList();
        }

        public int GetAuctionsCount(int? categoryId, string searchTerm)
        {
            var context = new DealDoubleContext();

            var auction = context.Auctions.Include(a => a.Category).AsQueryable();

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                auction = auction.Where(x => x.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                auction = auction.Where(x => x.Title.ToLower().Contains(searchTerm.ToLower()));
            }

            return auction.Count();
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

            var existingAuction = context.Auctions.Where(a => a.Id == auction.Id).Include(a => a.AuctionPictures).First();

            context.AuctionPictures.RemoveRange(existingAuction.AuctionPictures); //delete existing pics 

            context.Entry(existingAuction).CurrentValues.SetValues(auction); //update all old values to newest except : navigation objects like pics

            context.AuctionPictures.AddRange(auction.AuctionPictures); //add new pics

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

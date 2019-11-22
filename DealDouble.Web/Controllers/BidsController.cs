using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DealDouble.Entities;
using Microsoft.AspNet.Identity;
using DealDouble.Services;

namespace DealDouble.Web.Controllers
{
    public class BidsController : Controller
    {
        [HttpPost]
        public JsonResult Bid(int auctionId)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if(User.Identity.IsAuthenticated)
            {
                Bid bid = new Bid();

                bid.AuctionId = auctionId;
                bid.UserId = User.Identity.GetUserId();
                bid.Timestamp = DateTime.Now;
                bid.BidAmount = 10;

                var bidResult = BidsService.Instance.AddBid(bid);

                if(bidResult)
                    result.Data = new { success = true };
                else
                    result.Data = new { success = false, message = "Unable to add bid." };
            }
            else
            {
                result.Data = new { success = false, message = "You have to login first." };
            }

            return result;
            
        }
    }
}
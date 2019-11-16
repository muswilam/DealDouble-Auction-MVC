using DealDouble.Entities;
using DealDouble.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DealDouble.Web.ViewModels;

namespace DealDouble.Web.Controllers
{
    public class AuctionsController : Controller
    {
        AuctionsService auctionService = new AuctionsService();
        CategoriesService catService = new CategoriesService();
        SharedService sharedService = new SharedService();

        public ActionResult Index(int? categoryId, string search, int? pageNo)
        {
            var auctionsModel = new AuctionsListingViewModel();

            auctionsModel.PageTitle = "Auctions";
            auctionsModel.PageDescription = "Auctions listing page.";

            auctionsModel.CategoryId = categoryId;
            auctionsModel.SearchTerm = search;
            auctionsModel.PageNo = pageNo;
            auctionsModel.EntityId = (int) EntitiesEnum.Auction;

            auctionsModel.Categories = catService.GetCategories();

            return View(auctionsModel);
        }

        public PartialViewResult Listing(int? categoryId, string search, int? pageNo)
        {
            var pageSize = 5;
            pageNo = pageNo ?? 1;

            var auctionsModel = new AuctionsListingViewModel();

            auctionsModel.AllAuctions = auctionService.FilterAuctions(categoryId, search, pageNo.Value, pageSize);

            var totalAuctions = auctionService.GetAuctionsCount(categoryId, search);

            auctionsModel.Pager = new Pager(totalAuctions,pageNo, pageSize);

            auctionsModel.CategoryId = categoryId;
            auctionsModel.SearchTerm = search;

            return PartialView(auctionsModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new AuctionViewModel();

            model.Categories = catService.GetCategories();

            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Create(AuctionViewModel auctionModel)
        {
            JsonResult result = new JsonResult();

            if (ModelState.IsValid)
            {

                Auction newAuction = new Auction();

                newAuction.Title = auctionModel.Title;
                newAuction.Description = auctionModel.Description;
                newAuction.ActualPrice = auctionModel.ActualPrice;
                newAuction.StartingTime = auctionModel.StartingTime;
                newAuction.EndingTime = auctionModel.EndingTime;
                newAuction.CategoryId = auctionModel.CategoryId;

                //check if we have aictionpictureIds back from form 
                if (!String.IsNullOrEmpty(auctionModel.AuctionPictures))
                {
                    var pictureIds = auctionModel.AuctionPictures.Split(',').Select(int.Parse);

                    newAuction.AuctionPictures = new List<AuctionPicture>();
                    newAuction.AuctionPictures.AddRange(pictureIds.Select(pi => new AuctionPicture() { PictureId = pi }));

                    #region Same functionality for adding auction pictures
                    /*foreach (var picId in pictureIds)
            {
                var auctionPicture = new AuctionPicture();
                auctionPicture.PictureId = picId;
                newAuction.AuctionPictures.Add(auctionPicture);
            } */
                    #endregion

                }

                auctionService.SaveAuction(newAuction);

                result.Data = new { success = true };
            }
            else
            {
                result.Data = new { success = false, message = "Invalid inputs." };
            }

            return result;
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new AuctionViewModel();

            model.Auction = auctionService.GetAuction(id);
            model.Categories = catService.GetCategories();

            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Edit(AuctionViewModel auctionModel)
        {
            JsonResult result = new JsonResult();

            if (ModelState.IsValid)
            {
                var auctionFromDb = auctionService.GetAuction(auctionModel.Id);

                auctionFromDb.Title = auctionModel.Title;
                auctionFromDb.Description = auctionModel.Description;
                auctionFromDb.ActualPrice = auctionModel.ActualPrice;
                auctionFromDb.StartingTime = auctionModel.StartingTime;
                auctionFromDb.EndingTime = auctionModel.EndingTime;
                auctionFromDb.CategoryId = auctionModel.CategoryId;

                //check if we have auctionPictureIds back from form  
                if (!String.IsNullOrEmpty(auctionModel.AuctionPictures))
                {
                    var pictureIds = auctionModel.AuctionPictures.Split(',').Select(int.Parse);

                    auctionFromDb.AuctionPictures = new List<AuctionPicture>(); //empty auctionPicture object before modified
                    auctionFromDb.AuctionPictures.AddRange(pictureIds.Select(pi => new AuctionPicture()
                    {
                        PictureId = pi,
                        AuctionId = auctionFromDb.Id //prevent create new auction
                    }).ToList());
                }

                auctionService.UpdateAuction(auctionFromDb);

                result.Data = new { success = true};
            }
            else
            {
                result.Data = new { success = false, message = "Invalid Inputs." };
            }

            return result;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = new AuctionDetailsViewModel();

            model.Auction = auctionService.GetAuction(id);

            model.EntityId = Convert.ToInt32(EntitiesEnum.Auction);

            model.BidsAmount = model.Auction.ActualPrice + model.Auction.Bids.Sum(b => b.BidAmount);

            var latestBidder = model.Auction.Bids.OrderByDescending(b => b.Timestamp).FirstOrDefault();

            model.LatestBidder = latestBidder != null ? latestBidder.User : null;

            model.PageTitle = model.Auction.Title;
            model.PageDescription = model.Auction.Description != null ? (model.Auction.Description.Length > 10 ? model.Auction.Description.Substring(0, 10) : model.Auction.Description) : "Auction Details.";

            model.Comments = sharedService.GetComments(model.EntityId, id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int entityId, Auction auction)
        {
            auctionService.DeleteAuction(auction);
            sharedService.DeleteEntityComments(entityId, auction.Id); //delete comments that depend on this auction 

            return RedirectToAction("Listing");
        }
    }
}
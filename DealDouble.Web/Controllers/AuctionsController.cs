﻿using DealDouble.Entities;
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
        public ActionResult Index(int? categoryId, string search, int? pageNo)
        {
            var auctionsModel = new AuctionsListingViewModel();

            auctionsModel.PageTitle = "Auctions";
            auctionsModel.PageDescription = "Auctions listing page.";

            auctionsModel.CategoryId = categoryId;
            auctionsModel.SearchTerm = search;
            auctionsModel.PageNo = pageNo;
            auctionsModel.EntityId = (int) EntitiesEnum.Auction;

            auctionsModel.Categories = CategoriesService.Instance.GetCategories();

            return View(auctionsModel);
        }

        public PartialViewResult Listing(int? categoryId, string search, int? pageNo)
        {
            var pageSize = 5;
            pageNo = pageNo ?? 1;

            var auctionsModel = new AuctionsListingViewModel();

            auctionsModel.AllAuctions = AuctionsService.Instance.FilterAuctions(categoryId, search, pageNo.Value, pageSize);

            var totalAuctions = AuctionsService.Instance.GetAuctionsCount(categoryId, search);

            auctionsModel.Pager = new Pager(totalAuctions,pageNo, pageSize);

            auctionsModel.CategoryId = categoryId;
            auctionsModel.SearchTerm = search;

            return PartialView(auctionsModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new AuctionViewModel();

            model.Categories = CategoriesService.Instance.GetCategories();

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

                AuctionsService.Instance.SaveAuction(newAuction);

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

            model.Auction = AuctionsService.Instance.GetAuction(id);
            model.Categories = CategoriesService.Instance.GetCategories();

            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Edit(AuctionViewModel auctionModel)
        {
            JsonResult result = new JsonResult();

            if (ModelState.IsValid)
            {
                var auctionFromDb = AuctionsService.Instance.GetAuction(auctionModel.Id);

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

                AuctionsService.Instance.UpdateAuction(auctionFromDb);

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

            model.Auction = AuctionsService.Instance.GetAuction(id);

            model.EntityId = Convert.ToInt32(EntitiesEnum.Auction);

            model.BidsAmount = model.Auction.ActualPrice + model.Auction.Bids.Sum(b => b.BidAmount);

            var latestBidder = model.Auction.Bids.OrderByDescending(b => b.Timestamp).FirstOrDefault();

            model.LatestBidder = latestBidder != null ? latestBidder.User : null;

            model.PageTitle = model.Auction.Title;
            model.PageDescription = model.Auction.Description != null ? (model.Auction.Description.Length > 10 ? model.Auction.Description.Substring(0, 10) : model.Auction.Description) : "Auction Details.";

            model.Comments = CommentsServices.Instance.GetComments(model.EntityId, id);

            model.AverageRate = CommentsServices.Instance.GetAverageRate(model.EntityId, id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int entityId, Auction auction)
        {
            AuctionsService.Instance.DeleteAuction(auction);
            CommentsServices.Instance.DeleteEntityComments(entityId, auction.Id); //delete comments that depend on this auction 

            return RedirectToAction("Listing");
        }
    }
}
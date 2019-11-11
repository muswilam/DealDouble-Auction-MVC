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

        public ActionResult Index(int? categoryId, string searchTerm, int? pageNo)
        {
            var auctionsModel = new AuctionsListingViewModel();

            auctionsModel.PageTitle = "Auctions";
            auctionsModel.PageDescription = "Auctions listing page.";

            auctionsModel.CategoryId = categoryId;
            auctionsModel.SearchTerm = searchTerm;
            auctionsModel.PageNo = pageNo;

            auctionsModel.Categories = catService.GetCategories();

            return View(auctionsModel);
        }

        public PartialViewResult Listing(int? categoryId, string searchTerm, int? pageNo)
        {
            var pageSize = 5;

            var auctionsModel = new AuctionsListingViewModel();

            auctionsModel.AllAuctions = auctionService.FilterAuctions(categoryId, searchTerm, pageNo, pageSize);

            var totalAuctions = auctionService.GetAuctionsCount();

            auctionsModel.Pager = new Pager(totalAuctions,pageNo, pageSize);

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
        public ActionResult Edit(AuctionViewModel auctionModel)
        {
            var auctionFromDb = auctionService.GetAuction(auctionModel.Id);

            auctionFromDb.Title = auctionModel.Title;
            auctionFromDb.Description = auctionModel.Description;
            auctionFromDb.ActualPrice = auctionModel.ActualPrice;
            auctionFromDb.StartingTime = auctionModel.StartingTime;
            auctionFromDb.EndingTime = auctionModel.EndingTime;
            auctionFromDb.CategoryId = auctionModel.CategoryId;

            //there's a BUG here (update auction pictures)
            //check if we have aictionpictureIds back from form  
            //if(!String.IsNullOrEmpty(auctionModel.AuctionPictures))
            //{
            //    var pictureIds = auctionModel.AuctionPictures.Split(',').Select(int.Parse);

            //    auctionFromDb.AuctionPictures = new List<AuctionPicture>();
            //    auctionFromDb.AuctionPictures.AddRange(pictureIds.Select(pi => new AuctionPicture()
            //    {
            //        PictureId = pi,
            //    }));
            //}
           
            auctionService.UpdateAuction(auctionFromDb);

            return RedirectToAction("Listing");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = new AuctionDetailsViewModel();

            model.Auction = auctionService.GetAuction(id);

            model.PageTitle = model.Auction.Title;
            model.PageDescription = model.Auction.Description.Length > 10 ? model.Auction.Description.Substring(0, 10) : model.Auction.Description;

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Auction auction)
        {
            auctionService.DeleteAuction(auction);

            return RedirectToAction("Listing");
        }
    }
}
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

        public ActionResult Index()
        {
            var auctionsModel = new AuctionsListingViewModel();

            auctionsModel.PageTitle = "Auctions";
            auctionsModel.PageDescription = "Auctions listing page.";

            return View(auctionsModel);
        }

        public PartialViewResult Listing()
        {
            var auctionsModel = new AuctionsListingViewModel();

            auctionsModel.PageTitle = "Auctions";
            auctionsModel.PageDescription = "Auctions listing page.";

            auctionsModel.AllAuctions = auctionService.GetAuctions();

            return PartialView(auctionsModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateAuctionViewModel();

            model.Categories = catService.GetCategories();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(CreateAuctionViewModel auctionModel)
        {
            Auction newAuction = new Auction();

            newAuction.Title = auctionModel.Title;
            newAuction.Description = auctionModel.Description;
            newAuction.ActualPrice = auctionModel.ActualPrice;
            newAuction.StartingTime = auctionModel.StartingTime;
            newAuction.EndingTime = auctionModel.EndingTime;
            newAuction.CategoryId = auctionModel.CategoryId;

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

            auctionService.SaveAuction(newAuction);

            return RedirectToAction("Listing");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            var auction = auctionService.GetAuction(id);
            return PartialView(auction);
        }

        [HttpPost]
        public ActionResult Edit(Auction auction)
        {

            auctionService.UpdateAuction(auction);

            return RedirectToAction("Listing");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var auction = auctionService.GetAuction(id);

            return View(auction);
        }

        [HttpPost]
        public ActionResult Delete(Auction auction)
        {
            auctionService.DeleteAuction(auction);

            return RedirectToAction("Listing");
        }
    }
}
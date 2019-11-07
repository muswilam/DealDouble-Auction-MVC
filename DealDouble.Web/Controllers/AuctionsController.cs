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
        AuctionsService service = new AuctionsService();
        
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

            auctionsModel.AllAuctions = service.GetAuctions();

            return PartialView(auctionsModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(Auction auction)
        {

            service.SaveAuction(auction);

            return RedirectToAction("Listing");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            var auction = service.GetAuction(id);
            return PartialView(auction);
        }

        [HttpPost]
        public ActionResult Edit(Auction auction)
        {

            service.UpdateAuction(auction);

            return RedirectToAction("Listing");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var auction = service.GetAuction(id);

            return View(auction);
        }

        [HttpPost]
        public ActionResult Delete(Auction auction)
        {
            service.DeleteAuction(auction);

            return RedirectToAction("Listing");
        }
    }
}
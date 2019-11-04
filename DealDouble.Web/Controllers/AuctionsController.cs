using DealDouble.Entities;
using DealDouble.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class AuctionsController : Controller
    {
        public ActionResult Index()
        {
            var service = new AuctionsService();

            var auctions  = service.GetAuctions();
            
            return View(auctions);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Auction auction)
        {
            var service = new AuctionsService();

            service.SaveAuction(auction);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var service = new AuctionsService();

            var auction = service.GetAuction(id);
            return View(auction);
        }

        [HttpPost]
        public ActionResult Edit(Auction auction)
        {
            var service = new AuctionsService();

            service.UpdateAuction(auction);

            return View(auction);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var service = new AuctionsService();

            var auction = service.GetAuction(id);
            return View(auction);
        }

        [HttpPost]
        public ActionResult Delete(Auction auction)
        {
            var service = new AuctionsService();

            service.DeleteAuction(auction);

            return RedirectToAction("Index");
        }
    }
}
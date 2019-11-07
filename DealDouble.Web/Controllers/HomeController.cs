using DealDouble.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DealDouble.Web.ViewModels;

namespace DealDouble.Web.Controllers
{
    public class HomeController : Controller
    {
        AuctionsService service = new AuctionsService();

        public ActionResult Index()
        {
            AuctionsViewModel auctionModel = new AuctionsViewModel();

            auctionModel.PageTitle = "Home Page";
            auctionModel.PageDescription = "This is home page.";

            auctionModel.PromotedAuctions = service.GetPromotedAuctions();
            auctionModel.AllAuctions = service.GetAuctions();

            return View(auctionModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
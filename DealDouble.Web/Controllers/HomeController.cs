﻿using DealDouble.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DealDouble.Web.ViewModels;
using DealDouble.Entities;

namespace DealDouble.Web.Controllers
{
    public class HomeController : Controller
    {
        AuctionsService auctionService = new AuctionsService();
        CommentsServices commentService = new CommentsServices();

        public ActionResult Index()
        {
            AuctionsViewModel auctionModel = new AuctionsViewModel();

            auctionModel.PageTitle = "Home Page";
            auctionModel.PageDescription = "This is home page.";

            auctionModel.EntityId = (int)EntitiesEnum.Auction;

            auctionModel.PromotedAuctions = auctionService.GetPromotedAuctions();
            auctionModel.AllAuctions = auctionService.GetAuctions();

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DealDouble.Services;
using DealDouble.Web.ViewModels;

namespace DealDouble.Web.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            var model = new DashboardViewModel();

            model.UsersCount = DealDoubleUserManager.GetUsersCount();
            model.AuctionsCount = AuctionsService.Instance.GetAuctionsCount();
            model.BidsCount = BidsService.Instance.GetBidsCount();
            
            return View(model);
        }
    }
}
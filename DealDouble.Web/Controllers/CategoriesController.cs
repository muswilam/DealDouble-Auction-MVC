using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DealDouble.Web.ViewModels;
using DealDouble.Services;

namespace DealDouble.Web.Controllers
{
    public class CategoriesController : Controller
    {
        CategoriesService catService = new CategoriesService();

        public ActionResult Index()
        {
            var catsModel = new CategoriesListingViewModel();

            catsModel.PageTitle = "Categories";
            catsModel.PageDescription = "Categories listing page.";

            return View(catsModel);
        }

        public PartialViewResult Listing()
        {
            var catsModel = new CategoriesListingViewModel();

            catsModel.AllCategories = catService.GetCategories();

            return PartialView(catsModel);
        }

    }
}
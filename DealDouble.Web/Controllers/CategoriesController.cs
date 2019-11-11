using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DealDouble.Web.ViewModels;
using DealDouble.Services;
using DealDouble.Entities;

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

       [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel catModel)
        {
            Category newCategory = new Category();

            newCategory.Name = catModel.Name;
            newCategory.Description = catModel.Description;

            catService.SaveCategory(newCategory);

            return RedirectToAction("Listing");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new CategoryViewModel();

            model.Category = catService.GetCategory(id);

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel categoryModel)
        {
            var categoryFromDb = catService.GetCategory(categoryModel.Id);

            categoryFromDb.Name = categoryModel.Name;
            categoryFromDb.Description = categoryModel.Description;

            catService.UpdateCategory(categoryFromDb);

            return RedirectToAction("Listing");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = new CategoryDetailsViewModel();

            model.Category = catService.GetCategory(id);

            model.PageTitle = model.Category.Name;
            model.PageDescription = model.Category.Description.Length > 10 ? model.Category.Description.Substring(0, 10) : model.Category.Description;

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            catService.DeleteCategory(category);

            return RedirectToAction("Listing");
        }
    }
}
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
        public ActionResult Index(string search)
        {
            var catsModel = new CategoriesListingViewModel();

            catsModel.PageTitle = "Categories";
            catsModel.PageDescription = "Categories listing page.";

            catsModel.SearchTerm = search;

            return View(catsModel);
        }

        public PartialViewResult Listing(string search, int? pageNo)
        {
            int pageSize = 5;
            pageNo = pageNo ?? 1;

            var catsModel = new CategoriesListingViewModel();

            catsModel.AllCategories = CategoriesService.Instance.FilterCategories(search, pageNo.Value, pageSize);

            var totalCount = CategoriesService.Instance.GetCategoriesCount(search);
            catsModel.Pager = new Pager(totalCount, pageNo.Value, pageSize);

            catsModel.SearchTerm = search;

            return PartialView(catsModel);
        }

       [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult Create(CategoryViewModel catModel)
        {
            JsonResult result = new JsonResult();

            if (ModelState.IsValid)
            {
                Category newCategory = new Category();

                newCategory.Name = catModel.Name;
                newCategory.Description = catModel.Description;

                CategoriesService.Instance.SaveCategory(newCategory);

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
            var model = new CategoryViewModel();

            model.Category = CategoriesService.Instance.GetCategory(id);

            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Edit(CategoryViewModel categoryModel)
        {
            JsonResult result = new JsonResult();

            if (ModelState.IsValid)
            {
                var categoryFromDb = CategoriesService.Instance.GetCategory(categoryModel.Id);

                categoryFromDb.Name = categoryModel.Name;
                categoryFromDb.Description = categoryModel.Description;

                CategoriesService.Instance.UpdateCategory(categoryFromDb);

                result.Data = new { success = true };
            }
            else
            {
                result.Data = new { success = false, message = "Invalid inputs." };
            }

            return result;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = new CategoryDetailsViewModel();

            model.Category = CategoriesService.Instance.GetCategory(id);

            model.PageTitle = model.Category.Name;
            model.PageDescription = model.Category.Description != null ? (model.Category.Description.Length > 10 ? model.Category.Description.Substring(0, 10) : model.Category.Description) : "Category details.";

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            CategoriesService.Instance.DeleteCategory(category);

            return RedirectToAction("Listing");
        }
    }
}
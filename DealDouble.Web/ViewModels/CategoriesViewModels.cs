using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealDouble.Web.ViewModels
{
 
    public class CategoriesListingViewModel : PageViewModel
    {
        public List<Category> AllCategories { get; set; }
    }

    public class CateroriesViewModel : PageViewModel
    {
        public List<Category> AllCategories { get; set; }
    }

    public class CategoryDetailsViewModel : PageViewModel
    {
        public Category Category { get; set; }
    }

    public class CategoryViewModel : PageViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<Auction> Auctions { get; set; }

        public Category Category { get; set; }
    }

}
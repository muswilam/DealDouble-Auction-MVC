using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DealDouble.Web.ViewModels
{
 
    public class CategoriesListingViewModel : PageViewModel
    {
        public List<Category> AllCategories { get; set; }

        public Pager Pager { get; set; }
        public string SearchTerm { get; set; }
        public int EntityId { get; set; }
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

        [Required]
        [MinLength(10), MaxLength(125)]
        public string Name { get; set; }

        [MaxLength(400)]
        public string Description { get; set; }

        public List<Auction> Auctions { get; set; }

        public Category Category { get; set; }
    }

}
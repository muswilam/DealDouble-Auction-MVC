using DealDouble.Data;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DealDouble.Services
{
    public class CategoriesService
    {
        public List<Category> GetCategories()
        {
            var context = new DealDoubleContext();

            return context.Categories.Include(c => c.Auctions).ToList();
        }

    }
}

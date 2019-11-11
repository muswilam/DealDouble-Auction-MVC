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
        //read list of categories
        public List<Category> GetCategories()
        {
            var context = new DealDoubleContext();

            return context.Categories.Include(c => c.Auctions).ToList();
        }

        //read cattegory by Id
        public Category GetCategory(int id)
        {
            var context = new DealDoubleContext();

            return context.Categories.Include(c => c.Auctions).Where(c => c.Id == id).First();
        }

        //create new category 
        public void SaveCategory(Category category)
        {
            var context = new DealDoubleContext();

            context.Categories.Add(category);
            context.SaveChanges();
        }

        //update existing category
        public void UpdateCategory(Category category)
        {
            var context = new DealDoubleContext();

            context.Entry(category).State = EntityState.Modified;

            context.SaveChanges();
        }
   
    }
}

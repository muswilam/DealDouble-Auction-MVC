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
        #region Singleton Non-Thread Safety
        public static CategoriesService Instance
        {
            get
            {
                if (instance == null) instance = new CategoriesService();
                return instance;
            }
        }

        private static CategoriesService instance { get; set; }

        private CategoriesService()
        {

        }
        #endregion

        //read list of categories
        public List<Category> GetCategories()
        {
            var context = new DealDoubleContext();

            return context.Categories.Include(c => c.Auctions).ToList();
        }

        //filter categories 
        public List<Category> FilterCategories(string search, int pageNo, int pageSize)
        {
            var context = new DealDoubleContext();

            var categories = context.Categories.Include(c => c.Auctions).AsQueryable();

            if(!string.IsNullOrEmpty(search))
            {
                categories = categories.Where(c => c.Name.ToLower().Contains(search.ToLower()));
            }

            var skipCount = (pageNo - 1) * pageSize;

            return categories.OrderByDescending(c => c.Id).Skip(skipCount).Take(pageSize).ToList();
        }

        public int GetCategoriesCount(string search)
        {
            var context = new DealDoubleContext();

            var categories = context.Categories.AsQueryable();

            if(!string.IsNullOrEmpty(search))
            {
                categories = categories.Where(c => c.Name.ToLower().Contains(search.ToLower()));
            }

            return categories.Count();
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
   
        //delete category
        public void DeleteCategory(Category category)
        {
            var context = new DealDoubleContext();

            context.Entry(category).State = EntityState.Deleted;

            context.SaveChanges();
        }
    }
}

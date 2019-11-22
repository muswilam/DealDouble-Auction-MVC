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
    public class SharedService
    {
        #region Singleton Non-Thread Safety
        public static SharedService Instance
        {
            get
            {
                if (instance == null) instance = new SharedService();
                return instance;
            }
        }

        private static SharedService instance { get; set; }

        private SharedService()
        {

        }
        #endregion

        public int SavePicture(Picture picture)
        {
            var context = new DealDoubleContext();

            context.Pictures.Add(picture);
            context.SaveChanges();

            return picture.Id;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealDouble.Web.ViewModels
{
    public class DashboardViewModel : PageViewModel
    {
        public int AuctionsCount { get; set; }
        public int UsersCount { get; set; }
        public int BidsCount { get; set; }
    }
}
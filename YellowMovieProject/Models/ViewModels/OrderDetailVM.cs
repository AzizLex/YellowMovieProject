using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YellowMovieProject.Models;

namespace YellowMovieProject.Models.ViewModels
{
    public class OrderDetailVM
    {
        public Order Order { get; set; }
        public double Total { get; set; }
        public OrderDetailVM()
        {

        }
    }
}
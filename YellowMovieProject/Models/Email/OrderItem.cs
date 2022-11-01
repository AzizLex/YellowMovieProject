using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YellowMovieProject.Models.Email
{
    public class OrderItem
    {
        public string Title { get; set; }

        public string Director { get; set; }

        public int  ReleaseYear { get; set; }
        public int Quantity { get; set; }

        public double Price { get; set; }

    }
}
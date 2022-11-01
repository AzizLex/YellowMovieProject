using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YellowMovieProject.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer{ get; set; } // Don't name the variable the same as the class, Unnecessary confusion.
        public virtual List<OrderRow> OrderRows { get; set; }
        //public double total { get; set; }

        public Order()
        {
            OrderRows = new List<OrderRow>();
            OrderDate= DateTime.Now;

        }
    }
}